using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControlsHUD : MonoBehaviour {

	GameObject Canvas;
	bool show = false;
	void Start()
	{
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		Invoke("ShowHide",0.5f);
	}
	public void ShowHide()
	{
		show = !show;
		Canvas.transform.FindChild("Movement").gameObject.active = show;
		if(show == true)
			Canvas.transform.FindChild("Hide-Show/Text").GetComponent<Text>().text = "Hide";
		else
			Canvas.transform.FindChild("Hide-Show/Text").GetComponent<Text>().text = "Show";
	}
}
