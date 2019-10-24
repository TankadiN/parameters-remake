using System.Collections.Generic;
using UnityEngine;

namespace ModIO.UI
{
    public abstract class ModMediaCollectionDisplayComponent : MonoBehaviour
    {
        public abstract ImageDisplayData logoData { get; set; }
        public abstract IEnumerable<ImageDisplayData> youTubeData { get; set; }
        public abstract IEnumerable<ImageDisplayData> galleryData { get; set; }

        public abstract void Initialize();
        public abstract void DisplayMedia(ModProfile profile);
        public abstract void DisplayMedia(int modId,
                                          LogoImageLocator logoLocator,
                                          IEnumerable<GalleryImageLocator> galleryImageLocators,
                                          IEnumerable<string> youTubeURLs);
        public abstract void DisplayLoading();
    }
}
