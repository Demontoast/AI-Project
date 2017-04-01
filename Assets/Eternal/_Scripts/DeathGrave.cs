using UnityEngine;
using System.Collections;

public class DeathGrave : Interactable {

	public int Souls; 
	public int limit; 
	public int limitGem; 

	void Start()
	{
		text = "Retrieve Lost Essance";
		player = GameObject.FindGameObjectWithTag("Player");
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
	}

	public override void Interact()
	{
		GameObject.FindGameObjectWithTag ("MasterSave").GetComponent<UltraSave> ().removeGrave ();
		Destroy(pickUp);
		Canvas.transform.FindChild("Retrieved").gameObject.active = true;
		Invoke ("Off",.7f);
		this.gameObject.active = false;
	}
	
	public override void WalkAway(){}
	void Off()
	{
		Canvas.transform.FindChild("Retrieved").gameObject.active = false;
		Destroy(this.gameObject);
	}
}
