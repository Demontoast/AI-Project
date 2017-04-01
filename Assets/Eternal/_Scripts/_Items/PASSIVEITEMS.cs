using UnityEngine;
using System.Collections;

public class PASSIVEITEMS : ITEMS 
{
	public bool extraHeart = false;
	public bool extraThreeHearts = false;
	public bool extraMana = false;

	public bool fireDefense25 = false;
	public bool fireDefense50 = false;
	public bool fireDefense100 = false;

	public bool magicDefense25 = false;
	public bool magicDefense50 = false;
	public bool magicDefense100 = false;

	public bool oneHeartDoubleDamage = false;
	public bool oneHeartCurseDoubleDamage = false;
	public bool halfDamageHalfDealt = false;
	public bool lessManaCost = false;

	public bool extraEssence = false;
	public bool noDeathPenalty = false;

	public int Price;

	public PASSIVEITEMS()
	{
		extraHeart = false;
	}

	public PASSIVEITEMS(bool ExtraHeart,bool ExtraThreeHearts, bool ExtraMana,bool FireDefense25,bool MagicDefense25,bool OneHeartDoubleDamage, bool OneHeartCurseDoubleDamage, bool HalfDamageHalfDealt,bool LessManaCost,bool ExtraEssence, bool NoDeathPenalty,
		string name, string description,int Price) : base(false, false, false, name, description, true,  ITEMS.Type.Passive, Price, 0, false,1, 1,ITEMS.StatBoost.None,0,0,ITEMS.typeOfItem.Passive)
	{
		extraHeart = ExtraHeart;
		extraThreeHearts = ExtraThreeHearts;
		fireDefense25 = FireDefense25;
		magicDefense25 = magicDefense25;
		oneHeartDoubleDamage = OneHeartDoubleDamage;
		oneHeartCurseDoubleDamage = OneHeartCurseDoubleDamage;
		halfDamageHalfDealt = HalfDamageHalfDealt;
		lessManaCost = LessManaCost;
		extraEssence = ExtraEssence;
		noDeathPenalty = NoDeathPenalty;
		buyPrice = Price;
	}
}
