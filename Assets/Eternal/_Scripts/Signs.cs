using UnityEngine;
using System.Collections;

public class Signs : Interactable {

	[SerializeField] string signName;
	void Start () 
	{
		text = "Read Sign 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	

	public override void Interact()
	{
		Canvas.transform.FindChild (signName).gameObject.active = true;
		pickUp.active = false;
	}

	public override void WalkAway()
	{
		Destroy(pickUp);
		Canvas.transform.FindChild (signName).gameObject.active = false;
	}
}
