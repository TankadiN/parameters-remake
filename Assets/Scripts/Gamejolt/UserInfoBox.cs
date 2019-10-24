using System.Collections;
using GameJolt.API;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UserInfoBox : MonoBehaviour {
    public Sprite GuestAvatar;
    public Image Avatar;
	public TMP_Text Name;
	public TMP_Text UserType;

    private void Start() {
			StartCoroutine(UpdateRoutine());
	}

	private IEnumerator UpdateRoutine() {
		var wait = new WaitForSeconds(1f);
		while(enabled) {
			UpdateInfos();
			yield return wait;
		}
	}

    private void UpdateInfos() {
		var user = GameJoltAPI.Instance.CurrentUser;
        Avatar.sprite = user != null ? user.Avatar : GuestAvatar;
		Name.text = user != null ? user.Name : "Guest";
		UserType.text = user != null ? user.Type.ToString() : "None";
    }
}
