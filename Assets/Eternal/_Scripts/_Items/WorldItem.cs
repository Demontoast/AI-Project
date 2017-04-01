using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldItem : Interactable {

	public ITEMS item;

	void Start () 
	{
		text = "Pick Up Item 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
		item = this.GetComponent<PublicItems>().item;
	}
	

	public override void Interact()
	{
		if(player.GetComponent<Equip>().FindEmpty(item,true) == true)
		{
			itemDesc.transform.FindChild("Text").GetComponent<Text>().text = item.itemName + " x " + item.curStack;
			itemDesc.active = true;
			Destroy(pickUp);
			Destroy(this.gameObject);
		}
	}
	public override void Click(){}
}
