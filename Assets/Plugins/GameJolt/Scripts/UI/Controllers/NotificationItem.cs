using GameJolt.UI.Objects;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GameJolt.UI.Controllers {
	public class NotificationItem : MonoBehaviour {
		public Image Image;
		public TMP_Text Text;

		public void Init(Notification notification) {
			Text.text = notification.Text;

			if(notification.Image != null) {
				Image.sprite = notification.Image;
				Image.enabled = true;
			} else {
				Image.enabled = false;
			}
		}
	}
}