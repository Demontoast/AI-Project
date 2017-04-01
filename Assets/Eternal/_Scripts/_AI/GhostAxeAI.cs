using UnityEngine;
using System.Collections;

public class GhostAxeAI : EnemyBaseAI {
	
	public float atkZone = 1.5f;
	public float rangeAtkZone = 5f;
	public bool hit = false;
	public int amountPushBack = 1;
	float i = 0;
	public float coolDown;
	GameObject GhostBall;
	GameObject bul;
	bool spawn;
	
	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");
		anim = this.GetComponent<Animator>();
		GhostBall = Resources.Load("GhostBall")as GameObject;
		Invoke("SpawnON",1.2f);
		facingRight = true;	
		movespeed = 0.05f;
	}
	
	
	void Update () 
	{
		if(i>0)
		{
			i-=Time.deltaTime;
		}
		if(dead == false&&spawn)
		{
			float Distance = Vector2.Distance(this.transform.position,Player.transform.position);
			if(Distance < 7&&!Atk)
			{
				Atk = true;
			}
			
			if(Distance <= atkZone&&i <= 0)
			{
				Face();
				anim.SetBool("Attack",true);
				Invoke("ATTACKOFF",0.1f);
				i = coolDown;
			}
			else if(Distance > atkZone&&Distance<=rangeAtkZone&&i <= 0)
			{
				Face();
				anim.SetBool("Range",true);
				Invoke("ATTACKOFF",0.1f);
				i = coolDown;
			}
			else if(Atk == true)
			{
				if(Distance>atkZone)
				{
					if(facingRight == true)
						transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
					else
						transform.position = new Vector3(transform.position.x - movespeed, transform.position.y);
					Vector3 relativePoint = transform.InverseTransformPoint(Player.transform.position);
					if(facingRight == true && relativePoint.x < 0.0)
					{
						Flip();
					}
					else if(facingRight == false && relativePoint.x < 0.0)
					{
						Flip ();
					}
				}
				if(transform.position.y > Player.transform.position.y+0.25)
					transform.position = new Vector3(transform.position.x,transform.position.y - movespeed);
				else if(transform.position.y < Player.transform.position.y-0.25)
					transform.position = new Vector3(transform.position.x,transform.position.y + movespeed);
				//transform.position = Vector3.MoveTowards(transform.position,Player.transform.position,movespeed*Time.deltaTime);
				
			}
		}
	}
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	void Face()
	{
		Vector3 relativePoint = transform.InverseTransformPoint(Player.transform.position);
		if(facingRight == true && relativePoint.x < 0.0)
		{
			Flip();
		}
		else if(facingRight == false && relativePoint.x < 0.0)
		{
			Flip ();
		}
	}

	void ATTACKOFF()
	{
		anim.SetBool("Attack",false);
		anim.SetBool("Range",false);
	}

	void RangeATTack()
	{
		bul = Instantiate(GhostBall,this.transform.FindChild("FireExit").position,GhostBall.transform.rotation)as GameObject;
		if(facingRight == false)
		{
			Vector3 theScale = bul.transform.localScale;
			theScale.x *= -1;
			bul.transform.localScale = theScale;
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(-15,0);
		}
		else
		{
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(15,0);
		}
		Invoke("Explode",.1f);
	}
	void Explode()
	{
		if(bul!=null)
		{
			bul.GetComponent<Animator>().SetBool("Explode",true);
		}
	}
	void SpawnON()
	{
		spawn = true;
	}

	void Slash()
	{
		float Distance = Vector2.Distance(this.transform.position,Player.transform.position);
		
		if(Distance <= atkZone)
		{
			bool atk = true;
			i = coolDown;
			//Atk = false;
			object[] tempSortage = new object[6];
			
			tempSortage[0] = Damage; //Physical
			tempSortage[1] = 0f; //Fire
			tempSortage[2] = 0f; //Magic
			tempSortage[3] = 0f; //Art
			tempSortage[4] = 0;  //Type (Edge,Blunt,Peirce)
			tempSortage[5] = 0f; //Poison 
			
			Vector3 relativePoint = transform.InverseTransformPoint(Player.transform.position);
			if(facingRight == true && relativePoint.x < 0.0)
			{
				atk = false;
			}
			else if(facingRight == false && relativePoint.x < 0.0)
			{
				atk = false;
			}
			
			if(atk)
			{
				if(Player.GetComponent<Platformer2DUserControl>().isBlocking())
				{
					Vector3 RP = Player.transform.InverseTransformPoint(this.transform.position);
					if(RP.x > 0.0)
					{
						//Player.SendMessage("ApplyDamage",0,SendMessageOptions.DontRequireReceiver);
						Player.SendMessage("StamDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						Player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
				}
				else
					Player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
