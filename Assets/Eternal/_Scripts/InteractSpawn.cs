using UnityEngine;
using System.Collections;

public class InteractSpawn : Interactable {

	public bool isDestroyable;
	public float hp;
	public float maxHp;
	public Color color;
	public GameObject enemy;
	public GameObject item;
	public int souls;
	// Use this for initialization
	void Start () 
	{
		text = "Touch Grave 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
		color = this.GetComponentInChildren<SpriteRenderer>().color;
	}
	
	public override void Interact()
	{
		enemy.active = true;
		turnOff = true;
		Destroy(pickUp);
	}

	private void ApplyDamage(object[] damages)
	{
		if(isDestroyable&&enemy.active == true)
		{
			hp -= (float)damages[0];
			Invoke("ColorBack",.1f);
			this.GetComponent<SpriteRenderer>().color = Color.red;
			if(hp <= 0)
			{
				Invoke("DestroyEnemy",1f);
				enemy.GetComponent<EnemyBaseAI>().setDead(true);
				enemy.GetComponent<Animator>().SetBool("Dead",true);
				item.active = true;

			}
		}
	}
	void DestroyEnemy()
	{
		Destroy(enemy.gameObject);
		Destroy(this.gameObject);
	}
	void ColorBack()
	{
		this.GetComponent<SpriteRenderer>().color = color;	
	}
}
