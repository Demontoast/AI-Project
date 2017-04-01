using UnityEngine;
using System.Collections;

public class HiveAI : EnemyBaseAI {

	GameObject[] bees;
	public GameObject bee;
	public Transform spawn;
	Color col;
	bool another;
	GameObject deathHive;

	void Start()
	{
		bees = new GameObject[1];
		col = this.transform.FindChild("Hive").GetComponent<SpriteRenderer>().color;
		anim = this.GetComponent<Animator> ();
		Player = GameObject.FindGameObjectWithTag ("Player");
		deathHive = Resources.Load ("DeadHive") as GameObject;
	}
	void Update () 
	{
		if(bees[0] == null&&!another)
		{
			another = true;
			Invoke ("SpawnBee",0.5f);
		}
//		if(bees[1] == null)
//		{
//			GameObject clone = Instantiate(bee,spawn2.transform.position,spawn2.rotation)as GameObject;
//			bees[1] = clone;
//		}
		
	}
	void SpawnBee()
	{
		GameObject clone = Instantiate(bee,spawn.transform.position,spawn.rotation)as GameObject;
		bees[0] = clone;
		another = false;
	}
	void ApplyDamage(object[] damage)
	{
		Hp -= (float)damage[0];
		anim.SetBool ("Hit", true);
		Invoke ("HitOff", .1f);
		this.transform.FindChild("Hive").GetComponent<SpriteRenderer>().color = Color.red;
		Invoke("ColorBack",.2f);
		if (Hp <= 0) {
			GameObject x = Instantiate (deathHive, this.transform.position, this.transform.rotation) as GameObject;
			Destroy (x, 2f);
			OFF ();
			//this.gameObject.active = false;
		}
	}
	void ColorBack()
	{
		this.transform.FindChild("Hive").GetComponent<SpriteRenderer>().color = col;
	}
	void HitOff()
	{
		anim.SetBool ("Hit", false);
	}
}
