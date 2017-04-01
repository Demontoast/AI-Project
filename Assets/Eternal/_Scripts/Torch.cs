using UnityEngine;
using System.Collections;

public class Torch : Interactable {

	bool remainOn = false;
	bool isOn = false;

	// Use this for initialization
	void Start () {
		text = "Light Torch 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public override void Interact()
	{
		this.transform.FindChild ("Fire").gameObject.active = true;
		isOn = true;
		Invoke ("TurnOFF", 6.5f);
		Destroy(pickUp);
		turnOff = true;
	}

	void TurnOFF()
	{
		if (!remainOn) {
			this.transform.FindChild ("Fire").gameObject.active = false;
			isOn = false;
			turnOff = false;
		}
	}

	public void Remain()
	{
		remainOn = true;
	}

	public bool IsON()
	{
		return isOn;
	}


}
