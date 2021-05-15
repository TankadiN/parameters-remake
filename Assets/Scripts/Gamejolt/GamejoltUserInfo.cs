using System.Collections;
using GameJolt.API;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameJolt.Demo.UI {
	public class GamejoltUserInfo : MonoBehaviour {
		public Image Avatar;
        public Sprite GuestImage;
		public TMP_Text Name;
		public TMP_Text Id;
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
            Avatar.sprite = user != null ? user.Avatar : GuestImage;
			Name.text = user != null ? user.Name : "Guest";
			Id.text = user != null ? "ID: " + user.ID.ToString() : "ID: null";
			UserType.text = user != null ? user.Type.ToString() : "None";
		}
	}
}
