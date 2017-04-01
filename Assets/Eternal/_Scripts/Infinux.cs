using UnityEngine;
using System.Collections;

public class Infinux : MonoBehaviour {

	void Start()
	{
		Invoke ("Fade",3f);
		Invoke ("MainMneu",5f);
	}
	void Fade()
	{
		GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("FadeIn").gameObject.active = true;
	}
	void MainMneu()
	{
		Application.LoadLevel("Disclaimer");
	}
}
