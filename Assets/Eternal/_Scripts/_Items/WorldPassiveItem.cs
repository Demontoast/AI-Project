using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class WorldPassiveItem : Interactable {

	PASSIVEITEMS item;
	public string name;
	public string description;
	public int buyPrice;

	public bool extraHeart = false;
	public bool extraThreeHearts = false;
	public bool extraMana = false;
	public bool fireDefense25 = false;
	public bool fireDefense50 = false;
	public bool fireDefense100 = false;
	public bool magicDefense25 = false;
	public bool oneHeartDoubleDamage = false;
	public bool oneHeartCursedDoubleDamage = false;
	public bool halfDamageHalfDealt = false;
	public bool  lessManaCost = false;
	public bool extraEssence = false;
	public bool  noDeathPenalty = false;
	
	void Awake () 
	{
		item = new PASSIVEITEMS(extraHeart,extraThreeHearts,extraMana,fireDefense25,magicDefense25,oneHeartDoubleDamage,oneHeartCursedDoubleDamage,halfDamageHalfDealt,lessManaCost,extraEssence,noDeathPenalty,name,description,buyPrice);
	}

	void Start () 
	{
		text = "Pick Up Item 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
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
