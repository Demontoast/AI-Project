using UnityEngine;
using System.Collections;

public class CreateStarterStuff : CreateItems {


	void Start () {
		string[] abl_N = { "", "", "", "", "", "" };
		WEAPONS fallenSword = new WEAPONS("Fallen Sword",10,5,0,0,0,0,0,0,false,false,0,1.7f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,0,0.45f,2.2f);
		Classes novice = new Classes("Novice","The start of everything",fallenSword,null,0,1,3,abl_N);

		SPELLS fireBall = new SPELLS(false,false,"Fire Ball","Cast a huge fire with this spell.",0,0,1,SPELLS.SpellType.Fire,SPELLS.AtkType.Attack,10,0,0);
		SPELLS magicMiss = new SPELLS(false,false,"Magic Missile","Cast magic missile with this spell.",0,0,1,SPELLS.SpellType.Magic,SPELLS.AtkType.Attack,0,10,0);


		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(novice,false);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().mask = GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().Mask.get(0);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(fireBall,true);
		GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>().FindEmpty(magicMiss,true);
		//GameObject.FindGameObjectWithTag ("MasterSave").GetComponent<UltraSave> ().SavePlayer ();
	}

}
