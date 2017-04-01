using UnityEngine;
using System.Collections;

public class LevelSelect : Interactable 
{
	public bool GraveOn = false;
	public bool[] ShrinesOn;
	public string graveName;

	void Start () 
	{
		text = "Examine Grave 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	public override void Interact()
	{
		Canvas.transform.FindChild(graveName).gameObject.active = true;
	}
	public override void WalkAway()
	{
		Canvas.transform.FindChild(graveName).gameObject.active = false;
	}
	public override void Loop(){}
	public override void Click(){}

}
