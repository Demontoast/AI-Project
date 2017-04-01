using UnityEngine;
using System.Collections;

public class CreateItems : MonoBehaviour {


	void Start () 
	{
		//Armor
		//bool buyable, bool sellable, string name, string description,EquipRegion slot, int buyPrice, int sellPrice, 
		//int meleeDefense, int magicDefense,int fireDefense, int artDefense
//		MASK fallenMask = new MASK(false,false,"Fallen Mask","A mask from a fallen friend",
//		                             10,1,5,0,0,0,0,3,0,0,0,0,0);
//		MASK PerfectMask = new MASK(false,false,"Perfect Mask","The ultimate mask",
//		                           10,1,5,0,0,0,5,5,5,5,5,5,5);
//		ARMOR LeatherShirt = new ARMOR(false,true,"Leather Shirt","A worn down white shirt",ITEMS.EquipRegion.ChestSlot,
//		                           10,1,5,0,0,0,24);
//		ARMOR LeatherPants = new ARMOR(false,true,"Leather Pants","A worn down brown pants",ITEMS.EquipRegion.LegSlot,
//		                               10,1,5,0,0,0,13);
		WEAPONS Axe = new WEAPONS(false,true,"Hatchet","Worn down hatchet",ITEMS.Type.Weapon
		                          ,100,20,12,0,0,0,false,WEAPONS.scaleBonus.Str,true,20,2f,WEAPONS.weaponType.Edge
		                          ,4,1,0,0,'C',WEAPONS.weaponClass.Axe,true,0, 0.60f,.5f);
		WEAPONS fallenSword = new WEAPONS(false,true,"Fallen Sword","Broken Sword from a friend",ITEMS.Type.Weapon
		                          ,10,1,10,0,0,0,false,WEAPONS.scaleBonus.Str,false,15,2f,WEAPONS.weaponType.Edge
		                                  ,3,2,0,0,'C',WEAPONS.weaponClass.Sword,true,0, 0.60f,0f);
		WEAPONS sheild = new WEAPONS(false,true,"Broken Sheild","Worn down sheild",ITEMS.Type.Weapon
		                          ,250,50,95,50,50,50,true,WEAPONS.scaleBonus.none,false,0,0f,WEAPONS.weaponType.Blunt
		                             ,0,0,0,0,'C',WEAPONS.weaponClass.Sheild,false,0, 0f,0f);
		WEAPONS FireGem = new WEAPONS(false,true,"Fire Gem","Gem that allows you to control fire.",ITEMS.Type.Weapon
		                            ,250,50,0,20,0,0,true,WEAPONS.scaleBonus.Int,false,5,0f,WEAPONS.weaponType.Magic
		                              ,0,0,0,4,'C',WEAPONS.weaponClass.Gem,false,0, 0.60f,0f);
		WEAPONS MagicGem = new WEAPONS(false,true,"Magic Gem","Gem that allows you to control Magic.",ITEMS.Type.Weapon
		                              ,250,50,0,0,20,0,true,WEAPONS.scaleBonus.Int,false,5,0f,WEAPONS.weaponType.Magic
		                               ,0,0,0,4,'C',WEAPONS.weaponClass.Gem,false,0, 0.60f,0f);
		WEAPONS ArtGem = new WEAPONS(false,true,"Artificial Gem","Gem that allows you to control lightning.",ITEMS.Type.Weapon
		                              ,250,50,0,0,0,20,true,WEAPONS.scaleBonus.Int,false,5,0f,WEAPONS.weaponType.Magic
		                             ,0,0,0,4,'C',WEAPONS.weaponClass.Gem,false,0, 0.60f,0f);
		WEAPONS GreatSword = new WEAPONS(true,true,"Great Sword","Broken Great Sword from a friend",ITEMS.Type.Weapon
		                                 ,20,1,25,0,0,0,false,WEAPONS.scaleBonus.Str,true,30,2f,WEAPONS.weaponType.Edge
		                                 ,5,2,0,0,'C',WEAPONS.weaponClass.GreatSword,true,0, 1.1f,1.5f);
		WEAPONS staff = new WEAPONS(true,true,"Wooden Staff","An Elder Staff forged from the great wood trees.",ITEMS.Type.Weapon
		                                 ,2000,1,0,20,5,5,false,WEAPONS.scaleBonus.Int,true,5,3f,WEAPONS.weaponType.Magic
		                            ,0,0,0,5,'A',WEAPONS.weaponClass.Staff,false,0, 0.60f,0f);
		//		WEAPONS Hammer = new WEAPONS(false,true,"Hammer","Old Hammer",ITEMS.EquipRegion.LeftHandSlot,ITEMS.Type.Weapon
//		                             ,500,100,20,0,0,0,false,WEAPONS.scaleBonus.Str,true,50,2f,WEAPONS.weaponType.Blunt
//		                             ,12,0,0,'A',WEAPONS.weaponClass.Hammer,true,0);
//		GRIMOIRE book = new GRIMOIRE(true,true,"Fire Grimoire","Old dusty book",5,1,0,5,0,0,0,0,5,'C',0,GRIMOIRE.BookType.Fire);
		WEAPONS Gun = new WEAPONS(false,true,"Old Pistol","A pistol that barely works",ITEMS.Type.Weapon
		                          ,10,1,5,0,0,0,true,WEAPONS.scaleBonus.Pre,true,15,10f,WEAPONS.weaponType.Pierce
		                          ,0,0,2,0,'C',WEAPONS.weaponClass.Gun,false,0, 0.60f,0f);
//		WEAPONS poke = new WEAPONS(false,true,"Rapier","Worn down rapier",ITEMS.EquipRegion.LeftHandSlot,ITEMS.Type.Weapon
//		                          ,100,20,3,0,0,0,false,WEAPONS.scaleBonus.Dex,false,20,2f,WEAPONS.weaponType.Pierce
//		                          ,1,5,0,'B',WEAPONS.weaponClass.Rapier,true,0);
		WEAPONS poke = new WEAPONS(true,true,"Rapier","Worn down rapier",ITEMS.Type.Weapon
		                           ,100,50,5,0,0,0,false,WEAPONS.scaleBonus.Dex,false,3,2f,WEAPONS.weaponType.Pierce
		                           ,1,5,0,0,'B',WEAPONS.weaponClass.Rapier,true,0, 0.60f,0f);
		ITEMS relic = new ITEMS(false,true,true,"Old Relic","Ancient relic used in rituals. Regains small Hp.",true,
		                        ITEMS.Type.Item,50,0,true,10,10,ITEMS.StatBoost.CurHp,50,0f,ITEMS.typeOfItem.Consumable);
		ITEMS paper = new ITEMS(false,true,true,"Fire Paper","Ignite your main weapon in flames",true,
		                        ITEMS.Type.Item,100,0,true,20,10,ITEMS.StatBoost.Fire,10,0f,ITEMS.typeOfItem.Buff);
		ITEMS magicPaper = new ITEMS(false,true,true,"Magic Paper","Embuted your main weapon in magic",true,
		                        ITEMS.Type.Item,100,0,true,20,10,ITEMS.StatBoost.Magic,10,0f,ITEMS.typeOfItem.Buff);
		ITEMS ArtPaper = new ITEMS(false,true,true,"Mother Board","Enhance your main weapon in artificial technology",true,
		                        ITEMS.Type.Item,100,0,true,20,10,ITEMS.StatBoost.Artificial,10,0f,ITEMS.typeOfItem.Buff);
		ITEMS grass = new ITEMS(false,true,true,"Grass","Holy Grass",true,
		                        ITEMS.Type.Item,50,0,true,99,99,ITEMS.StatBoost.CurStam,0,0f,ITEMS.typeOfItem.Consumable);
		ITEMS purus = new ITEMS(false,true,true,"Purus","Regain mana.",true,
		                        ITEMS.Type.Item,50,0,true,99,99,ITEMS.StatBoost.CurMana,25,0f,ITEMS.typeOfItem.Consumable);
		ITEMS knives = new ITEMS(false,true,true,"Throwing Knives","Normal Knives",true,
		                        ITEMS.Type.Item,150,0,true,99,15,ITEMS.StatBoost.None,10,0f,ITEMS.typeOfItem.Throwable);
		ITEMS bullets = new ITEMS(false,true,true,"Bullets","Used with a Pistol",false,
		                        ITEMS.Type.Item,30,0,true,25,25,ITEMS.StatBoost.None,0,0f,ITEMS.typeOfItem.Bullets);
		ITEMS fireBomb = new ITEMS(false,true,true,"Fire Bomb","Explodes on contact",true,
		                          ITEMS.Type.Item,150,0,true,10,10,ITEMS.StatBoost.None,0,0f,ITEMS.typeOfItem.Throwable);
		ITEMS antidote = new ITEMS(false,true,true,"Antidote","Used for curing poison.",true,
		                           ITEMS.Type.Item,0,0,true,20,10,ITEMS.StatBoost.Poison,0,0f,ITEMS.typeOfItem.Consumable);
		ITEMS shard = new ITEMS(false,true,true,"Tablet Shard","Used for upgrading weapons.",false,
			ITEMS.Type.Item,0,0,true,99,1,ITEMS.StatBoost.Shards,0,0f,ITEMS.typeOfItem.Consumable);
		SPELLS fireBall = new SPELLS(false,false,"Fire Ball","Cast a huge fire with this spell.",0,0,10,SPELLS.SpellType.Fire,SPELLS.AtkType.Attack,10,0,0);
		SPELLS magicMiss = new SPELLS(false,false,"Magic Missile","Cast magic missile with this spell.",0,0,10,SPELLS.SpellType.Magic,SPELLS.AtkType.Attack,0,10,0);
//		ARMOR Ulta = new ARMOR(false,true,"Ultra Armor","Best in its class",ITEMS.EquipRegion.ChestSlot,
//		                               10000,1,50,50,50,50,124);
//		ARMOR KnightArmor = new ARMOR(false,true,"Knight Armor","2nd best in its class",ITEMS.EquipRegion.ChestSlot,
//		                       10000,1,25,25,25,25,70);
//		//Fire Grimoire



//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(fallenMask);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipArmor(0,0);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(PerfectMask);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(LeatherShirt);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipArmor(0,1);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(Ulta);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(KnightArmor);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(LeatherPants);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipArmor(0,2);
////		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipMainHand(0);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(Gun);
////		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipOffHand(0);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(sheild);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(FireGem);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(MagicGem);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(ArtGem);
////		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(Hammer);
////		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(Gun);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(fallenSword);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(relic);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipItem(0,0);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(paper);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(magicPaper);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(ArtPaper);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(grass);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(poke);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(GreatSword);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(staff);
//	//	GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipMainHand(0);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(bullets);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(knives);
//	//	GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(book);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(fireBomb);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(antidote);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(shard);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(purus);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(Axe);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(fireBall);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty2(magicMiss);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().ChangeClass("Novice");
//		if(this.GetComponent<PlayerSave>().enabled == true)
//			this.GetComponent<PlayerSave>().SaveObject();
		//GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().EquipOffHand(0);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty(Body,false);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty(Leg,false);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty(item,false);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty(perm1,false);
//		GameObject.FindGameObjectWithTag("Player").GetComponent<Equipment>().FindEmpty(perm2,false);
	}
	


}
