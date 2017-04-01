using UnityEngine;
using System.Collections;


public class ItemsV2 : ScriptableObject 
{
	public int buyPrice = 0;
	public int	sellPrice=0;				// price if sold
	public bool isBuyable= true;			//if item can be bought
	public bool isSellable = true;			//if item can be sold
	public bool isConsumable = false;		//if item is consumed upon use
	public bool isToolItem = false;		//if the item is a tool or not
	public string itemName = "";			//the item's name
	public string itemDescription = "";	//the item's description
	public bool canEquip = false;			//if the item can be equiped or not
	public bool isStackable=false;			//if the item can be stacked in the inventory/ground or not
	public int stackLimit = 0;
	public int curStack = 0;
	public ClassRestriction restrict;
	public Type itemType;
	public EquipRegion equipSlot;
	public StatBoost statBoost;
	public int amountToBoost;
	public float timeBoost;


	public ItemsV2(bool buyable, bool sellable, bool consumable, string name, string description, bool equip,
	               bool isTool, EquipRegion slot, Type type, int buyPrice, int sellPrice, bool stackable, 
	               ClassRestriction restriction, int stackMax,int stack,StatBoost statBoost,int amountToBoost,float timeBoost) 
	{
		isBuyable = buyable;
		isSellable = sellable;
		isConsumable= consumable;
		itemName = name;
		itemDescription = description;
		canEquip = equip;
		isToolItem= isTool;
		restrict = restriction;
		itemType = type;
		this.buyPrice = buyPrice;
		this.sellPrice = sellPrice;
		isStackable = stackable;
		stackLimit = stackMax;
		curStack = stack;
		equipSlot = slot;
		this.statBoost = statBoost;
		this.amountToBoost = amountToBoost;
		this.timeBoost = timeBoost;
	}

	public enum Type	//enums of the types of items can be changed to add more
	{
		Item,
		Armour,
		Weapon,
		ToolItem,
		StatBoster,		//add more, change name?
		Perminant
	};
	
	public enum EquipRegion	//different spots item can be equpied if it is equpiable 
	{
		HeadSlot,
		ChestSlot,
		LegSlot,
		FootSlot,
		LeftHandSlot,
		RightHandSlot,
		ToolSlot,
		StorageSlot,	//different name?
		None,
	};
	
	public enum ClassRestriction
	{
		Mage,
		Archer,
		Fighter,
		Rouge,
		All,
	};

	public enum StatBoost
	{
		Health,
		Endurance,
		Strength,
		Skill,
		Speed,
		Intellegence,
		All,
		CurHp,
		CurMana,
		CurStam,
		CurSpeed,
		None,
	};


}
