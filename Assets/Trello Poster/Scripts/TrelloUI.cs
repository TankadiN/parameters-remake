using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Trello;

public class TrelloUI : MonoBehaviour
{
	private static readonly string[] TrelloCardPositions = { "top", "bottom" };

	[SerializeField]
	private TrelloPoster trelloPoster;
	[SerializeField]
	private GameObject trelloCanvas;
	[SerializeField]
	private GameObject reportPanel;
	[SerializeField]
	private TMP_InputField cardName;
    [SerializeField]
    private Toggle useDiscord;
    [SerializeField]
    private TMP_InputField playerName;
    [SerializeField]
	private TMP_InputField cardDesc;
	[SerializeField]
	private TMP_Dropdown cardPosition;
	[SerializeField]
	private TMP_Dropdown cardList;
	[SerializeField]
	private TMP_Dropdown cardLabel;
	[SerializeField]
	private Toggle includeScreenshot;
    [SerializeField]
    private Toggle sendComputerSpecs;
    [SerializeField]
    private RawImage screenshotDisplay;
    [SerializeField]
    private Texture2D screenshot;
	private bool noLabels = false;

    [Header("Discord Objects")]
    [SerializeField]
    private RawImage avatarImage;
    [SerializeField]
    private TMP_Text usernameText;

    private void Start()
	{
		cardList.AddOptions(GetDropdownOptions(trelloPoster.TrelloCardListOptions));
		TrelloCardOption[] cardLabels = trelloPoster.TrelloCardLabelOptions;
		if (cardLabels == null || cardLabels.Length == 0)
		{
			noLabels = true;
			cardLabel.gameObject.SetActive(false);
		}
		else
		{
			cardLabel.AddOptions(GetDropdownOptions(cardLabels));
		}
	}

	public void StartPostCard()
	{
        StartCoroutine(trelloPoster.PostCard(new TrelloCard(cardName.text, playerName.text, cardDesc.text, TrelloCardPositions[cardPosition.value], trelloPoster.TrelloCardListOptions[cardList.value].Id, noLabels ? null : trelloPoster.TrelloCardLabelOptions[cardLabel.value].Id, includeScreenshot.isOn ? screenshot.EncodeToPNG() : null, sendComputerSpecs.isOn ? true : false)));
	}

	private List<TMP_Dropdown.OptionData> GetDropdownOptions(TrelloCardOption[] cardOptions)
	{
		List<TMP_Dropdown.OptionData> dropdownOptions = new List<TMP_Dropdown.OptionData>();
		for (int i = 0; i < cardOptions.Length; i++)
		{
			dropdownOptions.Add(new TMP_Dropdown.OptionData(cardOptions[i].Name));
		}
		return dropdownOptions;
	}

	public void ToggleCanvas()
	{
		trelloCanvas.SetActive(!trelloCanvas.activeSelf);
	}

	public void ToggleCanvas(bool isEnabled)
	{
		trelloCanvas.SetActive(isEnabled);
	}

	public void TogglePanel()
	{
        StartCoroutine(TakeScreenShotAndOpenPanel());
	}

    public void TogglePanelInGame()
    {
        PauseMenu.inst.PausePanel.SetActive(false);
        StartCoroutine(TakeScreenShotAndOpenPanelInGame());
    }

    IEnumerator TakeScreenShotAndOpenPanel()
    {
        yield return new WaitForEndOfFrame();
        screenshot = ScreenCapture.CaptureScreenshotAsTexture();
        //yield return new WaitForSeconds(1f);
        reportPanel.SetActive(!reportPanel.activeSelf);
    }

    IEnumerator TakeScreenShotAndOpenPanelInGame()
    {
        yield return new WaitForEndOfFrame();
        screenshot = ScreenCapture.CaptureScreenshotAsTexture();;
        PauseMenu.inst.PausePanel.SetActive(true);
        reportPanel.SetActive(!reportPanel.activeSelf);
    }

    private void Update()
    {
        screenshotDisplay.texture = screenshot;
    }

    public void GetUser()
    {
        avatarImage.texture = DiscordManager.current.CurrentUser.avatar;
        usernameText.text = DiscordManager.current.CurrentUser.username + DiscordManager.current.CurrentUser.discrim;
    }

    public void UseDiscordUsername()
    {
        if (DiscordManager.current.CurrentUser != null)
        {
            GetUser();
            playerName.gameObject.SetActive(useDiscord.isOn ? false : true);
            playerName.text = useDiscord.isOn ? DiscordManager.current.CurrentUser.username + DiscordManager.current.CurrentUser.discrim : "";
        }
        else
        {
            Debug.LogError("You aren't logged in on Discord!");
            useDiscord.isOn = false;
        }
    }

    public void ResetUI()
	{
		cardName.text = "";
        playerName.text = "";
		cardDesc.text = "";
        screenshot = null;

    }
}
