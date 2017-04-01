using UnityEngine;
using System.Collections;

public class Thanks : MonoBehaviour {

	void Start()
	{
		Cursor.visible = true;
	}
	public void returnMainMenu()
	{
		Application.LoadLevel("MainMenu");
	}
}
