using UnityEngine;
using System.Collections;
//using UnityEditor;
using System.Xml;
using System.Xml.Serialization;

public class ITEMS : ScriptableObject {

	public int buyPrice = 0;
	public int	sellPrice=0;				
	public bool isBuyable= true;			
	public bool isSellable = true;			
	public bool isConsumable = false;		
	public string itemName = "";			
	public string itemDescription = "";	
	public bool canEquip = false;			
	public bool isStackable=false;			
	public int stackLimit = 0;
	public int curStack = 0;
	public Type itemType;
	public StatBoost statBoost;
	public int amountToBoost;
	public float timeBoost;
	//public Sprite icon;
	public typeOfItem typeofItem;
	public bool equipped;

	public ITEMS()
	{
		isBuyable = true;
		isSellable = true;
		isConsumable= true;
		itemName = "";
		itemDescription = "";
		canEquip = true;
		itemType = Type.Item;
		this.buyPrice = 0;
		this.sellPrice = 0;
		isStackable = true;
		stackLimit = 0;
		curStack = 0;
		this.statBoost = StatBoost.None;
		this.amountToBoost = 0;
		this.timeBoost = 0;
		typeofItem = typeOfItem.None;
	}

	public ITEMS(bool buyable, bool sellable, bool consumable, string name, string description, bool equip,
	             	 Type type, int buyPrice, int sellPrice, bool stackable, 
	             int stackMax,int stack,StatBoost statBoost,int amountToBoost,float timeBoost,typeOfItem TOI) 
	{
		isBuyable = buyable;
		isSellable = sellable;
		isConsumable= consumable;
		itemName = name;
		itemDescription = description;
		canEquip = equip;
		itemType = type;
		this.buyPrice = buyPrice;
		this.sellPrice = sellPrice;
		isStackable = stackable;
		stackLimit = stackMax;
		curStack = stack;
		this.statBoost = statBoost;
		this.amountToBoost = amountToBoost;
		this.timeBoost = timeBoost;
		//icon = AssetDatabase.LoadAssetAtPath("Assets/Eternal/"+itemName+".png", typeof(Sprite)) as Sprite;
		typeofItem = TOI;
	}


	public enum Type	
	{
		Item,
		Weapon,	
		Passive,
	};
	
	public enum StatBoost
	{
		All,
		MaxHp,
		MaxMana,
		CurHp,
		CurMana,
		CurStam,
		CurSpeed,
		Poison,
		Fire,
		Magic,
		Artificial,
		Cross,
		ShrineUpgrade,
		Essances,
		HeartShards,
		Shards,
		Damage,
		EternalEssance,
		SharpStone,
		PureEssance,
		SpeedUpDefenceDown,
		DefenceUpSpeedDown,
		KnightEssence,
		None,
	};

	public enum typeOfItem
	{
		Consumable, Throwable, Buff, Bullets, Arrows, Key, Passive, None, Class
	};
}
