using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InfiniteItems : Interactable {

	ITEMS item;
	void Start () 
	{	
		text = "Restock Items";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	

	public override void Interact()
	{
		this.GetComponent<PublicItems>().ReCreate ();
		item = this.GetComponent<PublicItems>().item;
		itemDesc.transform.FindChild ("Text").GetComponent<Text> ().text = item.itemName + " x " + item.curStack;
		player.GetComponent<Equip> ().FindEmpty (item, true);

		itemDesc.active = true;
		pickUp.active = false;
	}
}
