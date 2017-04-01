using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ScrollSnapTo : MonoBehaviour {

	[SerializeField] ScrollRect scrollRect;
	[SerializeField] RectTransform contentPanel;
	[SerializeField] EventSystem ES;
	[SerializeField] GameObject[] buttons;

	public void SnapTo(RectTransform target)
	{
		Canvas.ForceUpdateCanvases();

		Vector2 v = (Vector2)scrollRect.transform.InverseTransformPoint(contentPanel.position)
			- (Vector2)scrollRect.transform.InverseTransformPoint(target.position);
		contentPanel.anchoredPosition = new Vector2(0,v.y-40f);

	}

	void Start () 
	{
		if (Input.GetJoystickNames ().Length == 0)
			this.enabled = false;
	}
	void Update () 
	{
		for (int i = 0; i < buttons.Length; i++) {
			if (ES.currentSelectedGameObject!=null&&ES.currentSelectedGameObject.gameObject.Equals (buttons [i])) {
				SnapTo (ES.currentSelectedGameObject.GetComponent<RectTransform> ());
			}
		}
	}
}
