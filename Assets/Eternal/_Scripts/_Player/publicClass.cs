using UnityEngine;
using System.Collections;

public class publicClass : MonoBehaviour {

	public Classes cls;
	public bool novice;
	public bool soldier;
	public bool knight;
	public bool hunter;
	public bool fencer;
	public bool barbarian;
	public bool mage;
	public bool archer;
	public bool priest;
	public bool theif;

	void Awake () 
	{
		if (novice == true) {
			NOVICE_MASK ();
		}
		else if (soldier == true) {
			SOLDIER_MASK ();
		} else if (knight == true) {
			KNIGHT_MASK ();
		} else if (hunter == true) {
			HUNTER_MASK ();
		} else if (fencer == true) {
			FENCER_MASK ();
		} else if (barbarian) {
			BARBARIAN_MASK ();
		}else if (mage) {
			MAGE_MASK ();
		}else if (archer) {
			ARCHER_MASK ();
		}else if (priest) {
			PRIEST_MASK ();
		}else if (theif) {
			THEIF_MASK ();
		}
	}

	void NOVICE_MASK()
	{
		WEAPONS fallenSword = new WEAPONS("Fallen Sword",13,5,0,0,0,0,0,0,false,false,0,1.7f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.45f,1.1f);
		string[] ab = {"Elemental Slash", "", "", "", "", "" };
		cls = new Classes("Novice","The start of everything",fallenSword,null,0,1,3,ab);
	}
	void SOLDIER_MASK()
	{
		string[] abl = {"Block","","","","",""};
		WEAPONS longSword = new WEAPONS("Long Sword",15,5,0,0,0,0,0,0,false,false,0,2f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.5f,2.2f);
		WEAPONS sheild = new WEAPONS("Broken Sheild",100,0,50,5,25,2,0,0,true,false,0,0,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.Sheild,false,0,0,0);
		cls = new Classes("Soldier","Standard troops in the Sanctus military",longSword,sheild,0,3,5,abl);
	}
	void KNIGHT_MASK()
	{
		string[] abl = {"Quake","","","","",""};
		WEAPONS GreatSword = new WEAPONS("Great Sword",25,10,0,0,0,0,0,0,false,true,6f,2f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.GreatSword,false,1,1f,1.5f);
		cls = new Classes("Knight","Strong Knights who care the greatest of swords in battles",GreatSword,null,0,6,10,abl);
	}
	void HUNTER_MASK()
	{
		string[] abl = {"Shoot","","","","",""};
		WEAPONS huntersBlade = new WEAPONS("Hunter's Blade",20,5,0,0,0,0,0,0,false,false,1,1.5f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.6f,0f);
		WEAPONS Gun = new WEAPONS("Old Pistol",10,2,0,0,0,0,0,0,true,false,0f,0f,WEAPONS.weaponType.Pierce,WEAPONS.weaponClass.Gun,false,1,.6f,0f);
		cls = new Classes("Hunter","Tactical warriors skilled in the new rifle technology.",huntersBlade,Gun,0,4,7,abl);
	}
	void FENCER_MASK()
	{
		string[] abl_F = {"Pierce","","","","",""};
		WEAPONS poke = new WEAPONS("Rapier",5,5, 0,0,0,0,0,0,false,false,3f,2f,WEAPONS.weaponType.Pierce,WEAPONS.weaponClass.Rapier,true,1,.3f,2.2f);
		cls = new Classes("Fencer","These skilled warriors use speed and precision to deal critical damage.",poke,null,0,4,7,abl_F);
	}

	void BARBARIAN_MASK()
	{
		string[] abl_B = {"Rage","","","","",""};
		WEAPONS Club = new WEAPONS("Great Club",20,7,0,0,0,0,0,0,false,true,3f,2f,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.GreatSword,false,1,1f,1.5f);
		cls = new Classes("Barbarian","Ruthless warriors born to battle.",Club,null,0,6,10,abl_B);
	}

	void MAGE_MASK()
	{
		string[] abl_M = {"FireBall","","","","",""};
		WEAPONS staff = new WEAPONS("Wooden Staff",10,0,10,5,10,5,10,5,false,true,2f,1.5f,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.Staff,false,1,0.5f,0f);
		cls = new Classes("Mage","Lower class mages who weild magic.",staff,null,0,10,12,abl_M);
	
	}
	void ARCHER_MASK()
	{
		string[] abl_A = {"Focus","","","","",""};
		WEAPONS bow = new WEAPONS("Wooden Bow",10,5,0,0,0,0,0,0,false,true,3f,0,WEAPONS.weaponType.Pierce,WEAPONS.weaponClass.Bow,true,1,0.6f,1.2f);
		cls = new Classes("Archer","Great marksmen found in the highest moutains.",bow,null,0,5,5,abl_A);
	}

	void PRIEST_MASK()
	{
		string[] abl_P = {"Holy Cross","","","","",""};
		WEAPONS Mace = new WEAPONS("Mace",12,6,0,0,0,0,0,0,false,false,4,1.7f,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.Sword,true,1,0.5f,2.2f);
		cls = new Classes("Priest","Religious fighters using the ways of the Old God.",Mace,null,0,4,7,abl_P);

	}

	void THEIF_MASK()
	{
		string[] abl_T = {"Poison Blade","","","","",""};
		WEAPONS knife = new WEAPONS("Steel Knife",5,3,0,0,0,0,0,0,false,false,4,1.2f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.4f,2.2f);
		cls = new Classes("Theif","Close combat fighters using tactical posion and the shadows to fight.",knife,null,0,4,7,abl_T);
	}
}
