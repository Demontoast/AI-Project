using UnityEngine;
using System.Collections;

public class CreateStarterItems : CreateItems {


	void Start () 
	{
		WEAPONS Axe = new WEAPONS(false,true,"Hatchet","Worn down hatchet",ITEMS.Type.Weapon
		                          ,100,20,12,0,0,0,false,WEAPONS.scaleBonus.Str,true,20,2f,WEAPONS.weaponType.Edge
		                          ,4,1,0,0,'C',WEAPONS.weaponClass.Axe,true,0, 0.60f,.5f);
		WEAPONS huntersBlade = new WEAPONS("Hunter's Blade",20,5,0,0,0,0,0,0,false,false,1,1.5f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.6f,0f);
		WEAPONS fallenSword = new WEAPONS("Fallen Sword",10,5,0,0,0,0,0,0,false,false,0,1.7f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.5f,2.2f);
		WEAPONS longSword = new WEAPONS("Long Sword",15,5,0,0,0,0,0,0,false,false,0,2f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.5f,2.2f);
		WEAPONS GreatSword = new WEAPONS("Great Sword",25,10,0,0,0,0,0,0,false,true,3f,2f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.GreatSword,false,1,1f,1.5f);
		WEAPONS sheild = new WEAPONS("Broken Sheild",100,0,50,5,25,2,0,0,true,false,0,0,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.Sheild,false,1,0,0);
		WEAPONS bow = new WEAPONS("Wooden Bow",10,5,0,0,0,0,0,0,false,true,3f,0,WEAPONS.weaponType.Pierce,WEAPONS.weaponClass.Bow,true,1,0.6f,1.2f);
		WEAPONS staff = new WEAPONS("Wooden Staff",10,0,10,5,10,5,10,5,false,true,5f,1.5f,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.Staff,false,1,0.5f,0f);
		WEAPONS Gun = new WEAPONS("Old Pistol",10,2,0,0,0,0,0,0,true,false,0f,0f,WEAPONS.weaponType.Pierce,WEAPONS.weaponClass.Gun,false,1,.6f,0f);
		WEAPONS poke = new WEAPONS("Rapier",5,5, 0,0,0,0,0,0,false,false,3f,2f,WEAPONS.weaponType.Pierce,WEAPONS.weaponClass.Rapier,true,1,.3f,2.2f);
		WEAPONS Club = new WEAPONS("Great Club",20,7,0,0,0,0,0,0,false,true,3f,2f,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.GreatSword,false,1,1f,1.5f);
		WEAPONS knife = new WEAPONS("Steel Knife",5,3,0,0,0,0,0,0,false,false,4,1.2f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,1,0.4f,2.2f);
		WEAPONS Mace = new WEAPONS("Mace",12,6,0,0,0,0,0,0,false,false,4,1.7f,WEAPONS.weaponType.Blunt,WEAPONS.weaponClass.Sword,true,1,0.5f,2.2f);


		ITEMS relic = new ITEMS(false,true,true,"Old Relic","Ancient relic used in rituals. Regains small Hp.",true,
		                        ITEMS.Type.Item,50,0,true,20,5,ITEMS.StatBoost.CurHp,6,0f,ITEMS.typeOfItem.Consumable);
		ITEMS paper = new ITEMS(false,true,true,"Fire Paper","Ignite your main weapon in flames",true,
		                        ITEMS.Type.Item,100,0,true,20,10,ITEMS.StatBoost.Fire,10,0f,ITEMS.typeOfItem.Buff);
		ITEMS magicPaper = new ITEMS(false,true,true,"Magic Paper","Imbued your main weapon in magic.",true,
		                        ITEMS.Type.Item,100,0,true,20,10,ITEMS.StatBoost.Magic,10,0f,ITEMS.typeOfItem.Buff);
		ITEMS ArtPaper = new ITEMS(false,true,true,"Mother Board","Enhance your main weapon in artificial technology",true,
		                        ITEMS.Type.Item,100,0,true,20,10,ITEMS.StatBoost.Artificial,10,0f,ITEMS.typeOfItem.Buff);
		ITEMS purus = new ITEMS(false,true,true,"Purus","Regain mana.",true,
		                        ITEMS.Type.Item,50,0,true,20,4,ITEMS.StatBoost.CurMana,25,0f,ITEMS.typeOfItem.Consumable);
		ITEMS knives = new ITEMS(false,true,true,"Throwing Knives","Small Knives used for range combat. Provides peirce damage.",true,
		                        ITEMS.Type.Item,150,0,true,20,15,ITEMS.StatBoost.None,10,0f,ITEMS.typeOfItem.Throwable);
		ITEMS fireBomb = new ITEMS(false,true,true,"Fire Bomb","Explodes on contact",true,
		                          ITEMS.Type.Item,150,0,true,10,10,ITEMS.StatBoost.None,0,0f,ITEMS.typeOfItem.Throwable);
		ITEMS antidote = new ITEMS(false,true,true,"Antidote","Used for curing poison.",true,
		                           ITEMS.Type.Item,0,0,true,20,10,ITEMS.StatBoost.Poison,0,0f,ITEMS.typeOfItem.Consumable);
		ITEMS shard = new ITEMS(false,true,true,"Tablet Shard","Used for upgrading weapons.",true,
			ITEMS.Type.Item,0,0,true,99,20,ITEMS.StatBoost.Shards,5,0f,ITEMS.typeOfItem.Consumable);
		ITEMS heartShard = new ITEMS(false,false,true,"Heart Crystal","A red crystal formed in the shape of a heart. Used in rituals to grant life. Perminatly gain one heart."
		                             ,true,ITEMS.Type.Item,0,0,false,1,1,ITEMS.StatBoost.MaxHp,4,0f,ITEMS.typeOfItem.Consumable);
		ITEMS manaShard = new ITEMS(false,false,true,"Mana Crystal","A blue crystal said to be a catalyst of magic. Perminatly gain extra mana."
		                             ,true,ITEMS.Type.Item,0,0,false,1,1,ITEMS.StatBoost.MaxMana,4,0f,ITEMS.typeOfItem.Consumable);
		ITEMS OldOneEssance = new ITEMS(false,false,false,"Old One Essance","Said to be a part of the Old One. No one knows what it is used for or how to obtain more. A gift from a goddess. Used to upgrade a shrine of your choice."
		                                ,false,ITEMS.Type.Item,0,0,true,25,1,ITEMS.StatBoost.ShrineUpgrade,0,0,ITEMS.typeOfItem.Key);

		ITEMS YellowEssance = new ITEMS (false, false, true, "Yellow Essance", "A gift given to each Eternal. The yellow glow that imminates off each Eternal originates from this essance. This allows you to warp back to the last rested shrine with a price of all your essances.", 
			                      true,  ITEMS.Type.Item, 0, 0, false, 1, 1, ITEMS.StatBoost.EternalEssance, 0, 0, ITEMS.typeOfItem.Consumable);

		ITEMS sharpStone = new ITEMS (false, false, false, "Sharp Stone", "A stone used to infuse to masks", false,  ITEMS.Type.Item, 0, 0, true, 20, 1, ITEMS.StatBoost.SharpStone, 0, 0f, ITEMS.typeOfItem.Key);
		//SPELLS fireBall = new SPELLS(false,false,"Fire Ball","Cast a huge fire with this spell.",0,0,1,SPELLS.SpellType.Fire,SPELLS.AtkType.Attack,10,0,0);
		//SPELLS magicMiss = new SPELLS(false,false,"Magic Missile","Cast magic missile with this spell.",0,0,1,SPELLS.SpellType.Magic,SPELLS.AtkType.Attack,0,10,0);

		string[] abl_N = {"","","","","",""};
		string[] abl_B = {"Rage","","","","",""};
		string[] abl_S = {"Block","","","","",""};
		string[] abl_A = {"Focus","","","","",""};
		string[] abl_K = {"Quake","","","","",""};
		string[] abl_M = {"FireBall","","","","",""};
		string[] abl_H = {"Shoot","","","","",""};
		string[] abl_F = {"Pierce","","","","",""};
		string[] abl_T = {"Poison Blade","","","","",""};
		string[] abl_P = {"Holy Cross","","","","",""};

		Classes novice = new Classes("Novice","The start of everything",fallenSword,null,0,1,3,abl_N);
		Classes soldier = new Classes("Soldier","Standard troops in the Sanctus military",longSword,sheild,0,3,5,abl_S);
		Classes archer = new Classes("Archer","Great marksmen found in the highest moutains.",bow,null,0,5,5,abl_A);
		Classes knight = new Classes("Knight","Strong Knights who care the greatest of swords in battles.",GreatSword,null,0,6,10,abl_K);
		Classes mage = new Classes("Mage","Lower class mages who weild magic.",staff,null,0,10,12,abl_M);
		Classes hunter = new Classes("Hunter","Hunting prey using guns and sword is all these brave men know how to do.",huntersBlade,Gun,0,4,7,abl_H);
		Classes fencer = new Classes("Fencer","These skilled warriors use speed and precision to deal critical damage.",poke,null,0,4,7,abl_F);
		Classes barb = new Classes("Barbarian","Ruthless warriors born to battle.",Club,null,0,6,10,abl_B);
		Classes theif = new Classes("Theif","Close combat fighters using tactical posion and the shadows to fight.",knife,null,0,4,7,abl_T);
		Classes priest = new Classes("Priest","Religious fighters using the ways of the Old God.",Mace,null,0,4,7,abl_P);


		PASSIVEITEMS heartRing = new PASSIVEITEMS(true,false,false,false,false,false,false,false,false,false,false,"Heart Ring","Ring of a fallen soldier. Grants One extra heart.",0);
		PASSIVEITEMS FireRobe = new PASSIVEITEMS(false,false,false,true,false,false,false,false,false,false,false,"Fire Robe","Robe crafted from the rare fire rate. Provides small protection from fire.",0);


		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(novice,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().mask = GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().Mask.get(0);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(soldier,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(archer,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(mage,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(hunter,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(knight,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(fencer,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(barb,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(theif,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(priest,false);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(Gun);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(sheild);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(FireGem);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(MagicGem);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(ArtGem);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(fallenSword);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(relic,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(YellowEssance,false);
	//	GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().EquipItem(0,0);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(paper,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(heartRing,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(FireRobe,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(magicPaper,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(ArtPaper,true);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(poke);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(GreatSword);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(staff);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(bullets);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(knives,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(fireBomb,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(OldOneEssance,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(antidote,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(heartShard,true);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(manaShard,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(shard,true);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(purus,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(sharpStone,true);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(Axe);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(fireBall,true);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(magicMiss,true);
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().ChangeClass();
		//if(this.GetComponent<PlayerSave>().enabled == true)
		//	this.GetComponent<PlayerSave>().SaveObject();
	}
	


}
