using System.Collections.Generic;

namespace ModIO.UI
{
    [System.Serializable]
    public struct ModDisplayData
    {
        public ModProfileDisplayData    profile;
        public UserProfileDisplayData   submittorProfile;
        public ImageDisplayData         submittorAvatar;
        public ModfileDisplayData       currentBuild;
        public ImageDisplayData         logo;
        public ImageDisplayData[]       youTubeThumbnails;
        public ImageDisplayData[]       galleryImages;
        public ModTagDisplayData[]      tags;

        public ModStatisticsDisplayData statistics;

        public DownloadDisplayData      binaryDownload;

        public bool isSubscribed;
        public bool isModEnabled;
    }
}
