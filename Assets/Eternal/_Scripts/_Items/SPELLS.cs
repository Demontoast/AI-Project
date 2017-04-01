using UnityEngine;
using System.Collections;


public class SPELLS : ITEMS 
{
	public int manaUse;
	public float fireDam;
	public float magicDam;
	public float artDam;
	public SpellType type;
	public AtkType atkType;

	public SPELLS(){}

	public SPELLS(bool buyable, bool sellable, string name, string Description, 
	              int buyPrice, int sellPrice, int ManaUse, SpellType spellType,AtkType atkType,float FireDam,float MagicDam,float ArtDam)
		: base(buyable, sellable, false, name, Description, true, ITEMS.Type.Item, buyPrice, sellPrice, false,1, 1,ITEMS.StatBoost.None,0,0,ITEMS.typeOfItem.None)
	{
		//itemName = name;
		//itemDescription = Description;
		manaUse = ManaUse;
		type = spellType;
		this.atkType = atkType;
		fireDam = FireDam;
		magicDam = MagicDam;
		artDam = ArtDam;
	}


	public enum SpellType
	{
		Magic, Fire, Artificial, Other
	};

	public enum AtkType
	{
		Attack, Defense, Buff, AoE
	};

}
