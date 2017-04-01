using UnityEngine;
using System.Collections;

public class PublicItems : MonoBehaviour {

	public ITEMS item;
	
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
	public ITEMS.Type itemType;
	public ITEMS.StatBoost statBoost;
	public int amountToBoost;
	public float timeBoost;
	public ITEMS.typeOfItem TOI;
	
	void Awake () 
	{
		item = new ITEMS(isBuyable,isSellable,isConsumable,itemName,itemDescription,canEquip,itemType
		                 ,buyPrice,sellPrice,isStackable,stackLimit,curStack,statBoost,amountToBoost,timeBoost,TOI);
	}
	public void  ReCreate()
	{
		item = new ITEMS(isBuyable,isSellable,isConsumable,itemName,itemDescription,canEquip,itemType
			,buyPrice,sellPrice,isStackable,stackLimit,curStack,statBoost,amountToBoost,timeBoost,TOI);
	}
}
