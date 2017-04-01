using UnityEngine;
using System.Collections;

public class PublicWeapons : MonoBehaviour {

	public WEAPONS item;
	
	public int buyPrice = 0;
	public int	sellPrice=0;				
	public bool isBuyable= true;			
	public bool isSellable = true;					
	public string itemName = "";			
	public string itemDescription = "";
								
	public bool isOffHand = false;			
	public bool isDamageBoosted = false;	
	public bool isTwoHand = false;			
	public float decreaseManaOrStamina =0f;	
	public float Distance;
	public WEAPONS.scaleBonus status;
	public WEAPONS.weaponType wpType;
	public int StrReq;
	public int SkiReq;
	public int PreReq;
	public int IntReq;
	public char Scaling;
	public WEAPONS.weaponClass wpClass;
	public bool canBuff;
	public float baseDamage = 0;
	public float FireDam;
	public float MagicDam;
	public float ArtDam;
	public int level;
	public float time;
	public float strongTime;

	void Awake () 
	{
		item = new WEAPONS(isBuyable,isSellable,itemName,itemDescription,ITEMS.Type.Weapon,buyPrice
		                   ,sellPrice,baseDamage,FireDam,MagicDam,ArtDam,isOffHand,status,isTwoHand
		                   ,decreaseManaOrStamina,Distance,wpType,StrReq,SkiReq,PreReq,IntReq,Scaling,wpClass,canBuff,level,time,strongTime);
	}
	

}
