using UnityEngine;
using System;

namespace Trello
{
	public class TrelloCard
	{
		private const string TimestampFormat = "yyyy-MM-dd_HH.mm.ss";

		private string name;
		private string playerName;
        private string desc;
        private string pos;
		private string idList;
		private string idLabels;
		private byte[] screenshot;
        private bool computerSpecs;

		public TrelloCard(string name, string playerName, string desc, string pos, string idList, string idLabels, byte[] screenshot, bool computerSpecs)
		{
			this.name = name;
            this.playerName = playerName;
			this.desc = desc;
			this.pos = pos;
			this.idList = idList;
			this.idLabels = idLabels;
			this.screenshot = screenshot;
            this.computerSpecs = computerSpecs;
		}

		public WWWForm GetPostBody()
		{
			WWWForm postBody = new WWWForm();
			postBody.AddField("name", name);
			postBody.AddField("desc", FormattedDescription());
			postBody.AddField("pos", pos);
			postBody.AddField("idList", idList);
			if (!string.IsNullOrEmpty(idLabels))
			{
				postBody.AddField("idLabels", idLabels);
			}
			if (screenshot != null)
			{
				postBody.AddBinaryData("fileSource", screenshot, "screenshot_" + DateTime.Now.ToString(TimestampFormat) + ".png");
			}
			return postBody;
		}

		private string FormattedDescription()
		{
			return "###Reported by\n" + playerName + "\n###Summary\n" + desc + "\n###Game State\n" + GetGameState() + "\n###Settings\n" + GetSettings() + "\n###System Info\n" + GetSystemInfo();
		}

		private string GetGameState()
		{
			return "Add specific game state info here. Things like the players position or the current scene name.";
		}

		private string GetSettings()
		{
			return "Screen Resolution: " + Screen.currentResolution + " \nFull Screen: " + Screen.fullScreen + "\nQuality Level: " + QualitySettings.GetQualityLevel();
		}

		private string GetSystemInfo()
		{
            if (computerSpecs)
            {
                return "OS: " + SystemInfo.operatingSystem + "\nProcessor: " + SystemInfo.processorType + "\nMemory: " + SystemInfo.systemMemorySize + "\nGraphics API: " + SystemInfo.graphicsDeviceType + "\nGraphics Processor: " + SystemInfo.graphicsDeviceName + "\nGraphics Memory: " + SystemInfo.graphicsMemorySize + "\nGraphics Vendor: " + SystemInfo.graphicsDeviceVendor;
            }
            else
            {
                return "OS: [REDACTED]\nProcessor: [REDACTED]\nMemory: [REDACTED]\nGraphics API: [REDACTED]\nGraphics Processor: [REDACTED]\nGraphics Memory: [REDACTED]\nGraphics Vendor: [REDACTED]";
            }
		}
	}
}
