﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModIO.UI
{
    public enum PageTransitionDirection
    {
        FromLeft,
        FromRight,
    }

    public class ExplorerView : MonoBehaviour
    {
        // ---------[ FIELDS ]---------
        public event Action<ModView> inspectRequested;
        public event Action<ModView> subscribeRequested;
        public event Action<ModView> unsubscribeRequested;
        public event Action<ModView> enableModRequested;
        public event Action<ModView> disableModRequested;
        public event Action onFilterTagsChanged;

        [Header("Settings")]
        public GameObject itemPrefab = null;
        public float pageTransitionTimeSeconds = 0.4f;
        public RectTransform pageTemplate = null;

        [Header("UI Components")]
        public RectTransform contentPane;
        public InputField nameSearchField;
        public ModTagFilterView tagFilterView;
        public ModTagContainer tagFilterBar;
        public Dropdown sortByDropdown;
        public Text pageNumberText;
        public Text pageCountText;
        public Text resultCountText;
        [Tooltip("Object to display when there are no subscribed mods")]
        public GameObject noResultsDisplay;

        [Header("Display Data")]
        public GridLayoutGroup gridLayout = null;
        public RequestPage<ModProfile> currentPage = null;
        public RequestPage<ModProfile> targetPage = null;
        public List<string> filterTags = new List<string>();

        [Header("Runtime Data")]
        public bool isTransitioning = false;
        public RectTransform currentPageContainer = null;
        public RectTransform targetPageContainer = null;

        // --- RUNTIME DATA ---
        private IEnumerable<ModTagCategory> m_tagCategories = null;
        private List<ModView> m_modViews = new List<ModView>();

        // --- ACCESSORS ---
        public int itemsPerPage
        {
            get
            {
                if(this.gridLayout == null) { return 0; }

                return UIUtilities.CountVisibleGridCells(this.gridLayout);
            }
        }
        public IEnumerable<ModTagCategory> tagCategories
        {
            get { return m_tagCategories; }
            set
            {
                if(m_tagCategories == value)
                {
                    return;
                }

                m_tagCategories = value;
                if(value == null) { return; }

                if(tagFilterView != null)
                {
                    tagFilterView.tagCategories = value;
                }
                if(tagFilterBar != null)
                {
                    tagFilterBar.DisplayTags(filterTags, value);
                    tagFilterBar.gameObject.SetActive(filterTags.Count > 0);
                }
            }
        }
        public IEnumerable<ModView> modViews
        {
            get
            {
                return this.m_modViews;
            }
        }

        // ---[ CALCULATED VARS ]----
        public int CurrentPageNumber
        {
            get
            {
                int pageNumber = 0;

                if(currentPage != null
                   && currentPage.size > 0
                   && currentPage.resultTotal > 0)
                {
                    pageNumber = (int)Mathf.Floor((float)currentPage.resultOffset / (float)currentPage.size) + 1;
                }

                return pageNumber;
            }
        }
        public int CurrentPageCount
        {
            get
            {
                int pageCount = 0;

                if(currentPage != null
                   && currentPage.size > 0
                   && currentPage.resultTotal > 0)
                {
                    pageCount = (int)Mathf.Ceil((float)currentPage.resultTotal / (float)currentPage.size);
                }

                return pageCount;
            }
        }

        // ---------[ INITIALIZATION ]---------
        private void OnEnable()
        {
            // asserts
            if(pageTemplate == null)
            {
                Debug.LogWarning("[mod.io] Page Template variable needs to be set in order for the"
                                 + " Explorer View to function", this.gameObject);
                this.enabled = false;
                return;
            }

            this.gridLayout = pageTemplate.GetComponent<GridLayoutGroup>();
            if(this.gridLayout == null)
            {
                Debug.LogWarning("[mod.io] Page Template needs a grid layout component in order for the"
                                 + " Explorer View to function", this.gameObject);
                this.enabled = false;
                return;
            }

            // create pages
            pageTemplate.gameObject.SetActive(false);

            GameObject pageGO;

            pageGO = (GameObject)GameObject.Instantiate(pageTemplate.gameObject, pageTemplate.parent);
            pageGO.name = "Mod Page A";
            currentPageContainer = pageGO.GetComponent<RectTransform>();
            currentPageContainer.gameObject.SetActive(true);

            pageGO = (GameObject)GameObject.Instantiate(pageTemplate.gameObject, pageTemplate.parent);
            pageGO.name = "Mod Page B";
            targetPageContainer = pageGO.GetComponent<RectTransform>();
            targetPageContainer.gameObject.SetActive(false);

            // update view
            UpdateCurrentPageDisplay();

            // TODO(@jackson): handle transition?
        }

        private void OnDisable()
        {
            // TODO(@jackson): handle transition?

            if(currentPageContainer != null)
            {
                GameObject.Destroy(currentPageContainer.gameObject);
            }
            if(targetPageContainer != null)
            {
                GameObject.Destroy(targetPageContainer.gameObject);
            }

            m_modViews.Clear();
        }

        public void Initialize()
        {
            Debug.Assert(itemPrefab != null);

            RectTransform prefabTransform = itemPrefab.GetComponent<RectTransform>();
            ModView prefabView = itemPrefab.GetComponent<ModView>();

            Debug.Assert(prefabTransform != null
                         && prefabView != null,
                         "[mod.io] The ExplorerView.itemPrefab does not have the required "
                         + "ModBrowserItem, ModView, and RectTransform components.\n"
                         + "Please ensure these are all present.");


            // - nested views -
            if(tagFilterView != null)
            {
                tagFilterView.Initialize();

                tagFilterView.tagFilterAdded += (tag) =>
                {
                    filterTags.Add(tag);

                    if(tagFilterBar != null)
                    {
                        tagFilterBar.gameObject.SetActive(true);
                        tagFilterBar.DisplayTags(filterTags, m_tagCategories);
                    }

                    if(onFilterTagsChanged != null)
                    {
                        onFilterTagsChanged();
                    }
                };
                tagFilterView.tagFilterRemoved += RemoveTag;
            }

            if(tagFilterBar != null)
            {
                tagFilterBar.Initialize();
                tagFilterBar.gameObject.SetActive(filterTags.Count > 0);

                tagFilterBar.tagClicked += (display) =>
                {
                    RemoveTag(display.data.tagName);
                };
            }
        }

        private void RemoveTag(string tag)
        {
            filterTags.Remove(tag);

            if(tagFilterBar != null)
            {
                tagFilterBar.DisplayTags(filterTags, m_tagCategories);
                tagFilterBar.gameObject.SetActive(filterTags.Count > 0);
            }

            if(onFilterTagsChanged != null)
            {
                onFilterTagsChanged();
            }
        }

        // ----------[ PAGE DISPLAY ]---------
        public void UpdateCurrentPageDisplay()
        {
            if(currentPageContainer == null) { return; }

            #if DEBUG
            if(isTransitioning)
            {
                Debug.LogWarning("[mod.io] Explorer View is currently transitioning between pages. It"
                                 + " is recommended to not update page displays at this time.");
            }
            #endif

            if(noResultsDisplay != null)
            {
                noResultsDisplay.SetActive(currentPage == null
                                           || currentPage.items == null
                                           || currentPage.items.Length == 0);
            }

            IEnumerable<ModProfile> profiles = null;
            if(this.currentPage != null)
            {
                profiles = this.currentPage.items;
            }

            UpdatePageNumberDisplay();
            DisplayProfiles(profiles, this.currentPageContainer);
        }

        public void UpdateTargetPageDisplay()
        {
            if(targetPageContainer == null) { return; }

            #if DEBUG
            if(isTransitioning)
            {
                Debug.LogWarning("[mod.io] Explorer View is currently transitioning between pages. It"
                                 + " is recommended to not update page displays at this time.");
            }
            #endif

            DisplayProfiles(this.targetPage.items, this.targetPageContainer);
        }

        public void UpdateSubscriptionsDisplay()
        {
            IList<int> subscribedModIds = ModManager.GetSubscribedModIds();

            foreach(ModView view in m_modViews)
            {
                ModDisplayData modData = view.data;
                bool isSubscribed = subscribedModIds.Contains(modData.profile.modId);

                if(modData.isSubscribed != isSubscribed)
                {
                    modData.isSubscribed = isSubscribed;
                    view.data = modData;
                }
            }
        }

        private void DisplayProfiles(IEnumerable<ModProfile> profileCollection, RectTransform pageTransform)
        {
            #if DEBUG
            if(!Application.isPlaying) { return; }
            #endif

            foreach(Transform t in pageTransform)
            {
                ModView view = t.GetComponent<ModView>();
                if(view != null)
                {
                    m_modViews.Remove(view);
                }
                GameObject.Destroy(t.gameObject);
            }

            List<ModView> pageModViews = new List<ModView>();
            if(profileCollection != null)
            {
                IList<int> subscribedModIds = ModManager.GetSubscribedModIds();
                IList<int> enabledModIds = ModManager.GetEnabledModIds();

                foreach(ModProfile profile in profileCollection)
                {
                    if(pageModViews.Count >= itemsPerPage)
                    {
                        Debug.LogWarning("[mod.io] ProfileCollection contained more profiles than "
                                         + "can be displayed per page", this.gameObject);
                        break;
                    }

                    GameObject itemGO = GameObject.Instantiate(itemPrefab,
                                                               new Vector3(),
                                                               Quaternion.identity,
                                                               pageTransform);
                    itemGO.name = "Mod Tile [" + pageModViews.Count.ToString() + "]";

                    // initialize item
                    ModView view = itemGO.GetComponent<ModView>();
                    view.onClick +=                 NotifyInspectRequested;
                    view.subscribeRequested +=      NotifySubscribeRequested;
                    view.unsubscribeRequested +=    NotifyUnsubscribeRequested;
                    view.enableModRequested +=      NotifyEnableRequested;
                    view.disableModRequested +=     NotifyDisableRequested;
                    view.Initialize();

                    if(profile == null)
                    {
                        view.DisplayLoading();
                    }
                    else
                    {
                        bool isModSubscribed = subscribedModIds.Contains(profile.id);
                        bool isModEnabled = enabledModIds.Contains(profile.id);

                        view.DisplayMod(profile,
                                        null,
                                        m_tagCategories,
                                        isModSubscribed,
                                        isModEnabled);

                        ModManager.GetModStatistics(profile.id,
                                                    (s) =>
                                                    {
                                                        ModDisplayData data = view.data;
                                                        data.statistics = ModStatisticsDisplayData.CreateFromStatistics(s);
                                                        view.data = data;
                                                    },
                                                    null);
                    }

                    pageModViews.Add(view);
                }

                if(pageModViews.Count > 0)
                {
                    for(int i = pageModViews.Count; i < itemsPerPage; ++i)
                    {
                        GameObject spacer = new GameObject("Spacing Tile [" + i.ToString("00") + "]",
                                                           typeof(RectTransform));
                        spacer.transform.SetParent(pageTransform);
                    }
                }
            }
            m_modViews.AddRange(pageModViews);

            // fix layouting
            if(this.isActiveAndEnabled)
            {
                LayoutRebuilder.MarkLayoutForRebuild(pageTransform);
            }
        }

        private void UpdatePageNumberDisplay()
        {
            if(currentPage == null) { return; }

            if(pageNumberText != null)
            {
                pageNumberText.text = CurrentPageNumber.ToString();
            }
            if(pageCountText != null)
            {
                pageCountText.text = CurrentPageCount.ToString();
            }
            if(resultCountText != null)
            {
                resultCountText.text = UIUtilities.ValueToDisplayString(currentPage.resultTotal);
            }
        }

        // ----------[ PAGE TRANSITIONS ]---------
        public void InitiateTargetPageTransition(PageTransitionDirection direction, Action onTransitionCompleted)
        {
            if(!isTransitioning)
            {
                float mainPaneTargetX = contentPane.rect.width * (direction == PageTransitionDirection.FromLeft ? 1f : -1f);
                float transPaneStartX = mainPaneTargetX * -1f;

                currentPageContainer.anchoredPosition = Vector2.zero;
                targetPageContainer.anchoredPosition = new Vector2(transPaneStartX, 0f);

                StartCoroutine(TransitionPageCoroutine(mainPaneTargetX, transPaneStartX,
                                                       this.pageTransitionTimeSeconds, onTransitionCompleted));
            }
            #if DEBUG
            else
            {
                Debug.LogWarning("[mod.io] ModPages are already transitioning.");
            }
            #endif
        }

        private IEnumerator TransitionPageCoroutine(float mainPaneTargetX, float transitionPaneStartX,
                                                    float transitionLength, Action onTransitionCompleted)
        {
            isTransitioning = true;

            targetPageContainer.gameObject.SetActive(true);

            float transitionTime = 0f;

            // transition
            while(transitionTime < transitionLength)
            {
                float transPos = Mathf.Lerp(0f, mainPaneTargetX, transitionTime / transitionLength);

                currentPageContainer.anchoredPosition = new Vector2(transPos, 0f);
                targetPageContainer.anchoredPosition = new Vector2(transPos + transitionPaneStartX, 0f);

                transitionTime += Time.deltaTime;

                yield return null;
            }

            // flip
            var tempContainer = currentPageContainer;
            currentPageContainer = targetPageContainer;
            targetPageContainer = tempContainer;

            var tempPage = currentPage;
            currentPage = targetPage;
            targetPage = tempPage;

            // finalize
            currentPageContainer.anchoredPosition = Vector2.zero;
            targetPageContainer.gameObject.SetActive(false);

            UpdatePageNumberDisplay();

            isTransitioning = false;

            if(onTransitionCompleted != null)
            {
                onTransitionCompleted();
            }
        }

        // ---------[ FILTER MANAGEMENT ]---------
        public void ClearFilters()
        {
            if(nameSearchField != null)
            {
                nameSearchField.text = string.Empty;
            }

            filterTags.Clear();

            if(tagFilterView != null)
            {
                tagFilterView.selectedTags = filterTags;
            }
            if(tagFilterBar != null)
            {
                tagFilterBar.DisplayTags(filterTags, m_tagCategories);
                tagFilterBar.gameObject.SetActive(false);
            }

            if(onFilterTagsChanged != null)
            {
                onFilterTagsChanged();
            }
        }

        // ---------[ EVENTS ]---------
        public void NotifyInspectRequested(ModView view)
        {
            if(inspectRequested != null)
            {
                inspectRequested(view);
            }
        }
        public void NotifySubscribeRequested(ModView view)
        {
            if(subscribeRequested != null)
            {
                subscribeRequested(view);
            }
        }
        public void NotifyUnsubscribeRequested(ModView view)
        {
            if(unsubscribeRequested != null)
            {
                unsubscribeRequested(view);
            }
        }
        public void NotifyEnableRequested(ModView view)
        {
            if(enableModRequested != null)
            {
                enableModRequested(view);
            }
        }
        public void NotifyDisableRequested(ModView view)
        {
            if(disableModRequested != null)
            {
                disableModRequested(view);
            }
        }
    }
}
