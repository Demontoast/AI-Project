using UnityEngine;
using System.Collections;

public class CreateItemsTutorial : CreateItems {


	void Start () {
		string[] abl_N = {"Slash","","","","",""};
		WEAPONS fallenSword = new WEAPONS("Fallen Sword",10,5,0,0,0,0,0,0,false,false,10,1.7f,WEAPONS.weaponType.Edge,WEAPONS.weaponClass.Sword,true,0,0.45f,.5f);
		Classes noMask = new Classes("No Mask","One who is maskless, is like one without a home.",fallenSword,null,0,1,3,abl_N);

	}

}
