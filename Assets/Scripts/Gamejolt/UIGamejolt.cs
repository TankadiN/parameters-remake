using System.Collections;
using GameJolt.API;
using GameJolt.UI;
using UnityEngine;
using UnityEngine.UI;

public class UIGamejolt : MonoBehaviour {
	public Button ShowTrophiesButton;
	private int notificationQueued;

	public void AutoLoginCallback(AutoLoginResult result) {
		Debug.Log(string.Format("Auto login result: {0}", result));
        if(result == AutoLoginResult.Success)
        {
            StartCoroutine(CallDownload());
        }
        else
        {

        }
    }

    private IEnumerator CallDownload()
    {
        yield return new WaitForSeconds(1f);
        GameJoltAPI.Instance.CurrentUser.DownloadAvatar(success =>
            Debug.LogFormat("Downloading avatar {0}", success ? "succeeded" : "failed"));
    }

	public void SignInButtonClicked() {
		GameJoltUI.Instance.ShowSignIn(signInSuccess => 
           {
			if(signInSuccess)
               {
				ShowTrophiesButton.interactable = true;
				Debug.Log("Logged In");
			}
            else
            {
				Debug.Log("Dismissed or Failed");
			}
		},
        userFetchSuccess => 
        {
			Debug.Log(string.Format("User's Information Fetch {0}.", userFetchSuccess ? "Successful" : "Failed"));
            DownloadAvatar();
        });
    }

	public void SignOutButtonClicked() {
		if(GameJoltAPI.Instance.HasUser) {
			ShowTrophiesButton.interactable = false;
			GameJoltAPI.Instance.CurrentUser.SignOut();
		}
	}

	public void DownloadAvatar() {
        GameJoltAPI.Instance.CurrentUser.DownloadAvatar(success =>
	    	Debug.LogFormat("Downloading avatar {0}", success ? "succeeded" : "failed"));
	}

	public void QueueNotification() {
		GameJoltUI.Instance.QueueNotification(
			string.Format("Notification <b>#{0}</b>", ++notificationQueued));
	}

	public void ShowLeaderboards() {
		GameJoltUI.Instance.ShowLeaderboards();
		// if you only want to show certain tables, you can provide them as additional arguments:
		// GameJolt.UI.Manager.Instance.ShowLeaderboards(null, null, 123, 456, 789, ...);
	}

    public void ShowTrophies()
    {
        GameJoltUI.Instance.ShowTrophies();
    }
}