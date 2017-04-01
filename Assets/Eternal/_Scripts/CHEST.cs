using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CHEST : Interactable 
{

	ITEMS item;
	Classes cls;
	PASSIVEITEMS passive;
	bool Open = false;
	[SerializeField] bool lockedBoss = false;
	[SerializeField] int index;

	void Start () 
	{
		text = "Open Chest 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
		if(this.GetComponent<PublicItems>())
			item = this.GetComponent<PublicItems>().item;
		else if(this.GetComponent<PublicPassive>())
			passive = this.GetComponent<PublicPassive>().item;
		else if(this.GetComponent<publicClass>())
			cls = this.GetComponent<publicClass>().cls;
	}

	public override void Interact()
	{
		
			Open = true;
			this.GetComponent<Animator> ().SetBool ("Open", true);
			this.GetComponent<CHEST> ().enabled = false;
			if (item != null) {
				itemDesc.transform.FindChild ("Text").GetComponent<Text> ().text = item.itemName + " x " + item.curStack;
				player.GetComponent<Equip> ().FindEmpty (item, true);
			} else if (cls != null) {
				itemDesc.transform.FindChild ("Text").GetComponent<Text> ().text = cls.getName () + " Mask x 1";
				player.GetComponent<Equip> ().FindEmpty (cls, false);
			} else if (passive != null) {
				itemDesc.transform.FindChild ("Text").GetComponent<Text> ().text = passive.itemName + " x 1";
				player.GetComponent<Equip> ().FindEmpty (passive, false);
			}


		itemDesc.active = true;
		pickUp.active = false;
	}
	public override void WalkAway(){}
	public override void Loop(){}
	public override void Click(){}
	public void AlreadyOpen()
	{
		Open = true;
		this.GetComponent<Animator>().SetBool("Open",true);
		this.GetComponent<CHEST>().enabled = false;
	}
	public bool isOpen()
	{
		return Open;
	}
}
