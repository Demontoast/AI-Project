using UnityEngine;
using System.Collections;

public class SwitchLever : Interactable {

	// Use this for initialization
	public string textOnScreen;
	void Start () 
	{
		text = textOnScreen+" 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
	}

	public override void Interact()
	{
		this.GetComponent<Animator>().SetBool("Action",true);
		Destroy (pickUp);
		Destroy(itemDesc);
		Destroy (this);
	}
	public override void WalkAway(){}
	public override void Loop(){}
	public override void Click(){}
}
