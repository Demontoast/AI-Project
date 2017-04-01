using UnityEngine;
using System.Collections;

public class PublicPassive : MonoBehaviour {

	public PASSIVEITEMS item;

	public bool extraHeart = false;
	public bool extraThreeHearts = false;
	public bool extraMana = false;
	public bool fireDefense25 = false;
	public bool fireDefense50 = false;
	public bool fireDefense100 = false;
	public bool magicDefense25 = false;
	public bool oneHeartDoubleDamage = false;
	public bool oneHeartCurseDoubleDamage = false;
	public bool halfDamageHalfDealt = false;
	public bool lessManaCost = false;
	public bool extraEssence = false;
	public bool  noDeathPenalty = false;
	public string name;
	public string desc;
	public int buyPrice;

	void Awake () {
		item = new PASSIVEITEMS (extraHeart, extraThreeHearts, extraMana, fireDefense25,magicDefense25,oneHeartDoubleDamage,oneHeartCurseDoubleDamage,halfDamageHalfDealt,lessManaCost,extraEssence,noDeathPenalty, name, desc,buyPrice);
	}
}
