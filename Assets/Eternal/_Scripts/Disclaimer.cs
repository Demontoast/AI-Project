using UnityEngine;
using System.Collections;

public class Disclaimer : MonoBehaviour {

	void Start()
	{
		Invoke ("Fade",5f);
		Invoke ("MainMneu",7f);
	}
	void Fade()
	{
		GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("FadeIn").gameObject.active = true;
	}
	void MainMneu()
	{
		Application.LoadLevel("MainMenu");
	}
}
