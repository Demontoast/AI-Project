using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
//using UnityEditor;

public class HUD : MonoBehaviour {

	public CharacterStats player;
	public Equip equip;

	Sprite Empty;
	Sprite EmptyMask;
	Sprite EmptyBody;
	Sprite EmptyPants;
	Sprite EmptyWeapon1;
	Sprite EmptyWeapon2;
	Sprite EmptyItem;
	Sprite EmptyBullets;
	Sprite EmptyPassive;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterStats>();
		equip = GameObject.FindGameObjectWithTag("Player").GetComponent<Equip>();
		Empty = Resources.Load<Sprite>("Empty") as Sprite;
		EmptyWeapon1 = Resources.Load<Sprite>("EmptyWeapon1") as Sprite;
		EmptyWeapon2 = Resources.Load<Sprite>("EmptyWeapon2") as Sprite;
		EmptyMask = Resources.Load<Sprite>("EmptyMask") as Sprite;
		EmptyBody = Resources.Load<Sprite>("EmptyBody") as Sprite;
		EmptyPants = Resources.Load<Sprite>("EmptyPants") as Sprite;
		EmptyItem = Resources.Load<Sprite>("EmptyItem") as Sprite;
		EmptyPassive = Resources.Load<Sprite>("EmptyRing") as Sprite;
	}
	
	// Update is called once per frame
	void Update () 
	{
		if(player.getCurHealth() <= 0)
		{
			player.gameObject.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<Animator>().SetBool("Dead",true);
			player.gameObject.GetComponent<Platformer2DUserControl> ().hasDied ();
			Invoke ("DeadON",1.5f);
		}
	
	}

	public void RespawnButton()
	{
		
	}
}
