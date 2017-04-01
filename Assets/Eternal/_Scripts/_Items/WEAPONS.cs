using UnityEngine;
using System.Collections;

public class WEAPONS : ITEMS {

	public float baseDamage = 0;	
	public float phyInc = 0;
	public float curDamage = 0;

	public float baseFireDamage;
	public float fireInc;
	public float fireDamage;

	public float baseMagicDamage;
	public float magicInc;
	public float magicDamage;

	public float baseArtDamage;
	public float artInc;
	public float artDamage;

	public int level;

	public weaponType type;
	public weaponClass wpClass;

	public int strReq = 0;
	public int skiReq = 0;
	public int intReq = 0;
	public int preReq = 0;

	public char scaling;
	public scaleBonus status;

	public bool isOffHand = false;			
	public bool isDamageBoosted = false;	
	public bool isTwoHand = false;	
	public bool canBuff = false;

	public float decreaseStamina =0f;	

	public float Distance;
	public float animTime;
	public float strongTime;


	public WEAPONS()
	{
		baseDamage = 0;
		isOffHand = false;
		status = scaleBonus.none;
		isTwoHand = false;
		decreaseStamina = 0;
		this.Distance = 0;
		this.type = weaponType.Edge;
		scaling = 'S';
		strReq = 0;
		skiReq = 0;
		intReq = 0;
		wpClass = weaponClass.Sword;
		canBuff = false;
		baseFireDamage = 0;
		baseMagicDamage = 0;
		baseArtDamage = 0;
		level = 0;
		animTime = 0f;
		strongTime =0f;
	}

	public WEAPONS(bool buyable, bool sellable, string name, string description, Type type, 
	               int buyPrice, int sellPrice, float damage, float FireDam, float MagicDam, float ArtDam, bool offHand, scaleBonus Stats, 
	               bool twoHand, float manaOrStaminaChange,float Distance, weaponType wpType, int StrReq, 
	               int SkiReq,int PreReq, int IntReq, char Scaling,weaponClass WpClass,bool CanBuff,int Level,float time,float StrongTime) : base(buyable, sellable, false, name, description, true, Type.Weapon, buyPrice, sellPrice, false,1, 1,ITEMS.StatBoost.None,0,0,ITEMS.typeOfItem.None)
	{
		baseDamage = curDamage = damage;
		isOffHand = offHand;
		status = Stats;
		isTwoHand = twoHand;
		decreaseStamina = manaOrStaminaChange;
		this.Distance = Distance;
		this.type = wpType;
		scaling = Scaling;
		strReq = StrReq;
		skiReq = SkiReq;
		intReq = IntReq;
		preReq = PreReq;
		wpClass = WpClass;
		canBuff = CanBuff;
		baseFireDamage = fireDamage = FireDam;
		baseMagicDamage = magicDamage = MagicDam;
		baseArtDamage = artDamage = ArtDam;
		level = Level;
		animTime = time;
		strongTime = StrongTime;
	}

	public WEAPONS(string name,float damage,float pInc, float FireDam,float fInc, float MagicDam,float mInc, float ArtDam,float aInc, bool offHand,
	               bool twoHand, float manaOrStaminaChange, float Distance,weaponType type, weaponClass WpClass,
	               bool CanBuff,int Level,float time,float StrongTime) : base(false, false, false, name, "", true, Type.Weapon, 0, 0, false,1, 1,ITEMS.StatBoost.None,0,0,ITEMS.typeOfItem.None)
	{
		baseDamage = curDamage = damage;
		isOffHand = offHand;
		status = scaleBonus.none;
		isTwoHand = twoHand;
		decreaseStamina = manaOrStaminaChange;
		this.Distance = Distance;
		this.type = type;
		scaling = 'X';
		strReq = 0;
		skiReq = 0;
		intReq = 0;
		preReq = 0;
		this.wpClass = WpClass;
		canBuff = CanBuff;
		baseFireDamage = fireDamage = FireDam;
		baseMagicDamage = magicDamage = MagicDam;
		baseArtDamage = artDamage = ArtDam;
		level = Level;
		animTime = time;
		strongTime = StrongTime;
		phyInc = pInc;
		fireInc = fInc;
		magicInc = mInc;
		artInc = aInc;
	}


	public enum scaleBonus
	{
		Str, Dex, Int, Pre, none
	}
	public enum weaponType
	{
		Edge, Blunt, Pierce, Magic
	}
	public enum weaponClass
	{
		Sword, Axe, Gun, Spear, Rapier, Hammer, GreatSword, Sheild, Staff, Gem, Bow
	}

//	public float calcScaleDamage()
//	{
//		float total = 0f;
//		Character x = GameObject.FindGameObjectWithTag("Player").GetComponent<Character>();
//		if(status.Equals(scaleBonus.Str)&&x.strengthLevel - strReq > 0)
//			total= MIN(30,(x.strengthLevel - strReq)) * SCALE(scaling) * baseDamage;
//		else if(status.Equals(scaleBonus.Dex)&&x.skillLevel - skiReq>0)
//			total= MIN(30,(x.skillLevel - skiReq)) * SCALE(scaling) * baseDamage;
//		else if(status.Equals(scaleBonus.Int)&&x.intelegenceLevel - intReq>0)
//			total= MIN(30,(x.intelegenceLevel - intReq)) * SCALE(scaling) * baseDamage;
//		return total;
//	}

	private float SCALE(char scale)
	{
		float x = 0.0f;
		switch(scale)
		{
		case 'S':
			x = .25f;
			break;
		case 'A':
			x = .20f;
			break;
		case 'B':
			x = .15f;
			break;
		case 'C':
			x = .10f;
			break;
		case 'D':
			x = .05f;
			break;
		}
		return x;
	}

	private int MIN(int x, int y)
	{
		if(x < y)
			return x;
		else 
			return y;
	}
}


