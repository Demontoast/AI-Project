using UnityEngine;
using System.Collections;

public class Equip : MonoBehaviour 
{
	private CharacterStats character;
	public Classes mask;
	public Classes curMask;

	public ITEMS curItem;
		public int curPos;
	public SPELLS curSpell;
		public int curSpellPos;
	public bool twoHand;
	public ITEMS[] items = new ITEMS[4];
	public PASSIVEITEMS[] passiveItems = new PASSIVEITEMS[4];
	public SPELLS[] spellSlots = new SPELLS[5];

	public ArrayList<ITEMS> inventory;
	public ArrayList<PASSIVEITEMS> passiveInventory;
	public ArrayList<ITEMS> keys;

	public ArrayList<Classes> Mask;
	public ArrayList<SPELLS> spells;

	public int abilityNo = 0; // -1 = nothing 

	void Awake()
	{
		character = GetComponent<CharacterStats>();
		inventory = new ArrayList<ITEMS>();
		passiveInventory = new ArrayList<PASSIVEITEMS>();
		keys = new ArrayList<ITEMS>();
		spells = new ArrayList<SPELLS>();
		Mask = new ArrayList<Classes>();
	}

	public void EquipMask(int index)
	{
		curMask = Mask.get (index);
	}
	public void DequipMask()
	{
		curMask = null;
	}
		
	public void EquipItem(int index, int number)
	{
		CheckForActiveDup (inventory.get (index).itemName);
		items[number] = inventory.get (index);
		if(curItem == null)
		{
			curItem = items[number];
			curPos = number;
		}
	}

	public void EquipSpell(int index, int number)
	{
		
		spellSlots[number] = spells.get (index);
		spells.remove(index);
		if(curSpell == null)
		{
			curSpell = spellSlots[number];
			curSpellPos = number;
		}

	}
		

	public void EquipPassiveItem(int index, int number)
	{
		CheckForPassiveDups (passiveInventory.get (index).itemName);
		if(passiveItems[number] == null)
		{
			passiveItems[number] = passiveInventory.get (index);
		}
		else
		{
			DeEquipPassiveItem(number);
			passiveItems[number] = passiveInventory.get (index);
		}
	}

	public void DeEquipPassiveItem(int index)
	{
		passiveItems[index] = null;

	}

	public void DeEquipPassiveAll()
	{
		if (passiveItems [0] != null) {
			DeEquipPassiveItem (0);
		}
		if (passiveItems [1] != null) {
			DeEquipPassiveItem (1);
		}
		if (passiveItems [2] != null) {
			DeEquipPassiveItem (2);
		}
		if (passiveItems [3] != null) {
			DeEquipPassiveItem (3);
		}
	}

	void CheckForPassiveDup()
	{

	}
	void CheckForActiveDup(string name)
	{
		for (int i = 0; i < items.Length; i++) {
			if (items[i]!=null&&items [i].itemName.Equals (name)) {
				deEquipItem (i);
			}
		}
	}
	void CheckForPassiveDups(string name)
	{
		for (int i = 0; i < items.Length; i++) {
			if (passiveItems[i]!=null&&passiveItems [i].itemName.Equals (name)) {
				DeEquipPassiveItem (i);
			}
		}
	}

	public void RingBreak()
	{
		bool stop = false;
		string name = "";
		for (int i = 0; i < passiveItems.Length&&!stop; i++) {
			if (passiveItems [i].noDeathPenalty) {
				stop = true;
				name = passiveItems [i].itemName;
				passiveItems[i] = null;
			}
		}
		stop = false;
		for (int i = 0; i < passiveInventory.size()&&!stop; i++) {
			if (passiveInventory.get (i).itemName.Equals (name)) {
				stop = true;
				passiveInventory.remove (i);
			}
		}
	}


	public void deEquipItem(int pos) // 
	{
		items [pos] = null;
		if (curPos == pos) {
			curItem = null;
			for(int i = 0;i<4&&curItem==null;i++)
			{
				if (items [i] != null) {
					curPos = i;
					curItem = items [curPos];

				}
			}
		}
	}

	public void deleteCurItem() // 
	{
		items[curPos] = null;
		curItem = null;
		for(int i = 0;i<4&&curItem==null;i++)
		{
			if (items [i] != null) {
				curPos = i;
				curItem = items [curPos];
		
			}
		}
	}

	public bool FindEmpty(Classes classes, bool equipIt)
	{	
		bool duplicate = false;
		for (int i = 0; i < Mask.size (); i++) {
			if (classes.getName ().Equals (Mask.get (i).getName ()))
				duplicate = true;
		}
		if(!duplicate)
			Mask.add (classes);
		return true;
	}

//	public bool FindEmpty(ITEMS item, bool equipIt)
//	{
//		bool result = false;
//
//		if(item.itemType.Equals(ITEMS.Type.Passive))
//		{
//			PASSIVEITEMS x = (PASSIVEITEMS)item;
//			passiveInventory.add(x);
//			result = true;
//		}
//		else if(item.itemType.Equals(ITEMS.Type.Item))
//		{
//			bool max = false;
//			ArrayList<ITEMS> X = null;
//			if (item.typeofItem.Equals (ITEMS.typeOfItem.Consumable)
//			    || item.typeofItem.Equals (ITEMS.typeOfItem.Buff)
//				|| item.typeofItem.Equals (ITEMS.typeOfItem.Class)
//			    || item.typeofItem.Equals (ITEMS.typeOfItem.Throwable)) {
//				X = inventory;
//
//				if(item.isStackable)
//				{
//					for(int i = 0;i<items.Length&&!result;i++)
//					{
//						if(items[i]!=null&&!(items[i].Equals(item))&&items[i].itemName.Equals(item.itemName))
//						{
//							result = true;
//							if(items[i].curStack < items[i].stackLimit)
//							{
//								items[i].curStack += item.curStack;
//								if(items[i].curStack > items[i].stackLimit)
//								{
//									items[i].curStack = items[i].stackLimit;
//								}
//								max = true;
//							}
//						}
//					}
//					for(int i = 0;i<X.size()&&!result;i++)
//					{
//						if(X.get (i)!=null&&X.get(i).itemName.Equals(item.itemName))
//						{
//							result = true;
//							if(X.get (i).curStack < X.get (i).stackLimit)
//							{
//								X.get (i).curStack += item.curStack;
//								if(X.get (i).curStack > X.get (i).stackLimit)
//								{
//									X.get (i).curStack = X.get (i).stackLimit;
//								}
//								max = true;
//							}
//						}
//					}
//					if(item.curStack == 0&&!item.itemName.Equals("Empty Old Relic"))
//					{
//						result = true;
//					}
//				}
//				if (equipIt&&!result) {
//					result = true;
//					if (items [0] == null) {
//						items [0] = item;
//						if (curPos == 0)
//							curItem = items [0];
//					
//					} else if (items [1] == null) {
//						items [1] = item;
//						if (curPos == 1)
//							curItem = items [1];
//					
//					} else if (items [2] == null) {
//						items [2] = item;
//						if (curPos == 2)
//							curItem = items [2];
//					
//					} else if (items [3] == null) {
//						items [3] = item;
//						if (curPos == 3)
//							curItem = items [3];
//					
//					} else {
//						result = false;
//					}
//				}
//			} else if (item.typeofItem.Equals (ITEMS.typeOfItem.Key)) {
//				X = keys; 
//			}
//			if(result==false&&max==false)
//			{
//				X.add(item);
//				result = true;
//			}
//		}
//		return result;
//	}

	public bool FindEmpty(ITEMS item, bool equipIt)
	{
		bool result = false;
		int pos = -1;
		bool key = false;
		if(item.itemType.Equals(ITEMS.Type.Passive))
		{
			PASSIVEITEMS x = (PASSIVEITEMS)item;
			passiveInventory.add(x);
			result = true;
		}
		else if(item.itemType.Equals(ITEMS.Type.Item))
		{
			bool max = false;
			ArrayList<ITEMS> X = null;
			if (item.typeofItem.Equals (ITEMS.typeOfItem.Consumable)
				|| item.typeofItem.Equals (ITEMS.typeOfItem.Buff)
				|| item.typeofItem.Equals (ITEMS.typeOfItem.Class)
				|| item.typeofItem.Equals (ITEMS.typeOfItem.Throwable)) {
				X = inventory;

				if(item.isStackable)
				{
					for(int i = 0;i<items.Length&&!result;i++)
					{
						if(items[i]!=null&&!(items[i].Equals(item))&&items[i].itemName.Equals(item.itemName))
						{
							result = true;
							if(items[i].curStack < items[i].stackLimit)
							{
								items[i].curStack += item.curStack;
								if(items[i].curStack > items[i].stackLimit)
								{
									items[i].curStack = items[i].stackLimit;
								}
								max = true;
							}
						}
					}
					for(int i = 0;i<X.size()&&!result;i++)
					{
						if(X.get (i)!=null&&X.get(i).itemName.Equals(item.itemName))
						{
							result = true;
							if(X.get (i).curStack < X.get (i).stackLimit)
							{
								X.get (i).curStack += item.curStack;
								if(X.get (i).curStack > X.get (i).stackLimit)
								{
									X.get (i).curStack = X.get (i).stackLimit;
								}
								max = true;
							}
						}
					}
					if(item.curStack == 0&&!item.itemName.Equals("Empty Old Relic"))
					{
						result = true;
					}
				}
				if (!result && !max) {
					equipIt = true;
				}

			} else if (item.typeofItem.Equals (ITEMS.typeOfItem.Key)) {
				X = keys; 
				key = true;
			}
			if(result==false&&max==false)
			{
				pos = X.add(item);
				result = true;
			}
			if (equipIt&&!max&&!key) {
				result = true;
				if (items [0] == null) {
					items [0] = X.get(pos);
					if (curPos == 0)
						curItem = items [0];

				} else if (items [1] == null) {
					items [1] = X.get(pos);
					if (curPos == 1)
						curItem = items [1];

				} else if (items [2] == null) {
					items [2] = X.get(pos);
					if (curPos == 2)
						curItem = items [2];

				} else if (items [3] == null) {
					items [3] = X.get(pos);
					if (curPos == 3)
						curItem = items [3];

				} else {
					result = false;
				}
			}

		}

		return result;
	}


	void removeSpells()
	{
		for(int i = 0; i< spellSlots.Length;i++)
		{
			if(spellSlots[i] != null)
			{
				FindEmpty(spellSlots[i],false);
				spellSlots[i] = null;
			}
		}
	}

	public void restoreRelic(int number)
	{
		bool results = false;
		bool noRelic = true;
		for(int i = 0;i<items.Length&&results==false;i++)
		{
			if(items[i] != null &&(items[i].itemName.Equals("Old Relic") || items[i].itemName.Equals("Empty Old Relic")))
			{
				if(items[i].curStack < number)
				{
					items[i].curStack = number;
					items[i].itemName = "Old Relic";
					results = true;
					noRelic = false;
				}
			}
		}
		for(int i = 0;i<inventory.size()&&results==false;i++)
		{
			if(inventory.get(i) != null && (inventory.get(i).itemName.Equals("Old Relic") || inventory.get(i).itemName.Equals("Empty Old Relic")))
			{
				if(inventory.get(i).curStack < number)
				{
					inventory.get(i).curStack = number;
					inventory.get(i).itemName = "Old Relic";
					results = true;
					noRelic = false;
				}
			}
		}
		if (noRelic) {

		}
	}
	
	public void ChangeClass() // Add Stuff Here
	{
		string curClass = mask.getName();
		GameObject sprite = null;
		//this.curClass = curClass;
		switch(curClass)
		{
		case "Novice":
			Destroy (this.GetComponent<Platformer2DUserControl> ().curSprite);
			sprite = Resources.Load ("Sword_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl> ().curSprite = Instantiate (sprite, sprite.transform.position, sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl> ().curSprite.transform.parent = this.transform.FindChild ("Sprites");
			this.GetComponent<Platformer2DUserControl> ().curSprite.transform.localPosition = sprite.transform.position;	
			abilityNo = 0;
			break;
		case "Soldier":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Sword_Sheild_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Knight":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("GreatSword_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent =this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Hunter":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Sword_Gun_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;	
			abilityNo = 0;
			break;
		case "Mage":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Wooden_Staff_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;	
			abilityNo = 0;
			break;
		case "Fencer":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Rapier_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Lumber Jack":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Hatchet_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Archer":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Bow_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Barbarian":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Barbarian_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Theif":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Theif_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Priest":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Priest_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		}
		if(this.GetComponent<PlatformerCharacter2D>().facingRight==false)
		{
			Vector3 theScale = this.GetComponent<Platformer2DUserControl>().curSprite.transform.localScale;
			theScale.x *= -1;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localScale = theScale;
		}
	}


	public bool ChangeClass(string name) // Add Stuff Here
	{
		GameObject sprite = null;
		mask = curMask;

		//this.curClass = curClass;
		switch(name)
		{
		case "NoMask":
			string[] abl_N = {"Slash","","","","",""};
			WEAPONS fallenSword = new WEAPONS("Fallen Sword",10,5,0,0,0,0,0,0,false,false,0,1.7f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,0,0.45f,.5f);
			Classes noMask = new Classes("No Mask","One who is maskless, is like one without a home.",fallenSword,null,0,1,3,abl_N);
			mask = noMask;
			Destroy (this.GetComponent<Platformer2DUserControl> ().curSprite);
			sprite = Resources.Load ("NoMask_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl> ().curSprite = Instantiate (sprite, sprite.transform.position, sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl> ().curSprite.transform.parent = this.transform.FindChild ("Sprites");
			this.GetComponent<Platformer2DUserControl> ().curSprite.transform.localPosition = sprite.transform.position;	
			abilityNo = 0;
			break;
		case "Novice":
			Destroy (this.GetComponent<Platformer2DUserControl> ().curSprite);
			sprite = Resources.Load ("Sword_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl> ().curSprite = Instantiate (sprite, sprite.transform.position, sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl> ().curSprite.transform.parent = this.transform.FindChild ("Sprites");
			this.GetComponent<Platformer2DUserControl> ().curSprite.transform.localPosition = sprite.transform.position;	
			abilityNo = 0;
			break;
		case "Soldier":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Sword_Sheild_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Knight":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("GreatSword_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent =this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Hunter":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Sword_Gun_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;	
			abilityNo = 0;
			break;
		case "Mage":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Wooden_Staff_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;	
			abilityNo = 0;
			break;
		case "Fencer":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Rapier_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Lumber Jack":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Hatchet_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Archer":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Bow_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Barbarian":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Barbarian_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Theif":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Theif_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		case "Priest":
			Destroy(this.GetComponent<Platformer2DUserControl>().curSprite);
			sprite = Resources.Load("Priest_Eternal") as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite = Instantiate(sprite,sprite.transform.position,sprite.transform.rotation)as GameObject;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.parent = this.transform.FindChild("Sprites");
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localPosition = sprite.transform.position;
			abilityNo = 0;
			break;
		}
		if(this.GetComponent<PlatformerCharacter2D>().facingRight==false)
		{
			Vector3 theScale = this.GetComponent<Platformer2DUserControl>().curSprite.transform.localScale;
			theScale.x *= -1;
			this.GetComponent<Platformer2DUserControl>().curSprite.transform.localScale = theScale;
		}
		return true;
	}

	// GETTTER AND SETTERS //
	public ITEMS getCurItem()
	{
		return curItem;
	}
	public string getCurClass()
	{
		return mask.getName();
	}

	public bool Change(string name)
	{
		bool stop = false;
		int result = -1;
		for(int i = 0;i<Mask.size()&&!stop;i++)
		{
			if(Mask.get(i).getName().Equals(name))
			{
				stop = true;
				result = i;
			}
		}
		if(result > -1)
		{
			//Debug.Log (equip.mask.getName ()+" "+equip.mask.getMainHand().curDamage);
			mask = Mask.get(result);
			return true;
			//Debug.Log (equip.mask.getName ()+" "+equip.mask.getMainHand().curDamage);
		}
		return false;
	}




	

}
