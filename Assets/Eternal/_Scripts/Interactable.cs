using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnitySampleAssets.CrossPlatformInput;

public class Interactable : MonoBehaviour 
{	
	protected GameObject player;
	protected GameObject Canvas;
	protected string text;
	float distance = 1f;
	protected GameObject pickUp = null;
	protected GameObject itemDesc = null;
	bool NextTo = false;
	protected bool turnOff = false;
	protected bool shortcut;
	bool check = false;
	protected bool hasInteracted;

	void Update () 
	{
		float h = CrossPlatformInputManager.GetAxis ("Vertical");
		if (h == 0)
			check = false;
		if(!turnOff)
		{
		if(player==null)
			player = GameObject.FindGameObjectWithTag("Player");
		float dist = Vector2.Distance(player.transform.position,this.transform.position);
		if(dist < distance&&!player.GetComponent<Platformer2DUserControl>().isDead())
		{
			NextTo = true;
			if(pickUp == null)
			{
				pickUp = Instantiate(Canvas.transform.FindChild("PickUp").gameObject, Canvas.transform.FindChild("PickUp").transform.position,Canvas.transform.FindChild("PickUp").transform.rotation)as GameObject;
				itemDesc = Instantiate(Canvas.transform.FindChild("ItemDesc").gameObject, Canvas.transform.FindChild("ItemDesc").transform.position,Canvas.transform.FindChild("ItemDesc").transform.rotation)as GameObject;
				pickUp.active = true;
				pickUp.transform.FindChild("Text").GetComponent<Text>().text = text;
				pickUp.transform.parent = Canvas.transform;
				itemDesc.transform.parent = Canvas.transform;
			}
				if(h<0f&&!check)
			{
				check = true;
				//Destroy(pickUp);
				Destroy(itemDesc,1f);
				Interact();
			}
		}
		else if(NextTo == true)
		{
			NextTo = false;
			Destroy(pickUp);
			WalkAway();
		}
		Loop ();
		Click();
		}
	}
	public virtual void Interact(){}
	public virtual void WalkAway(){}
	public virtual void Loop(){}
	public virtual void Click(){}
	public bool isShortCutOn()
	{
		return shortcut;
	}

	public void ShortCutON()
	{
		shortcut = true;
	}
	public virtual void saveOpen(){}
}
