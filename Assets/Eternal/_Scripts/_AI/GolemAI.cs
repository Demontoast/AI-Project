using UnityEngine;
using System.Collections;

public class GolemAI : EnemyBaseAI {
	
	public float atkZone = 2f;
	public bool hasDeath;

	float i = 0;
	public float coolDown;
	public Transform P1;
	public Transform P2;
	Transform curP;
	bool P = false; // p false = P1
	public GameObject HealthBar;
	public Color color;

	AudioClip slashSound;

	void Start () 
	{
		color = this.GetComponentInChildren<SpriteRenderer>().color;
		Player = GameObject.FindGameObjectWithTag("Player");
		HealthBar = transform.Find("Health").gameObject;
		anim = this.GetComponent<Animator>();
		isHpVis(false);
		curP = P1;
		facingRight = false;
		movespeed = 0.015f;
		source = GetComponent<AudioSource>();
		slashSound = Resources.Load ("Swoosh") as AudioClip;
	}

	void FixedUpdate () 
	{
		if(i>0)
		{
			i-=Time.deltaTime;
		}
		if(psn>0)
		{
			psn-=Time.deltaTime;
		}
		if(dead == false)
		{
			float Distance = Vector2.Distance(this.transform.position,Player.transform.position);
			float DisP = Vector2.Distance(this.transform.position,curP.transform.position);
			if (poisoned&&psn<=0) {
				this.GetComponent<SpriteRenderer>().color = Color.red;
				Invoke("ColorBack",.1f);
				Hp -= psnDamage;
				if (Hp < 0f)
					Hp = 0f;
				CalcHpBar();
				if(Hp<=0&&!dead)
				{
					this.GetComponentInChildren<Animator>().SetBool("Dead",true);
					Invoke("OFF",3f);
					dead = true;
				}
				psn = 2f;
			}
			if(i<=0)
			{
			if(Distance <= atkZone&&Distance>=1.5f)
			{
				Face();
				anim.SetBool("Attack",true);
				anim.SetBool("Move",false);
				Invoke("AttackOFF",0.1f);
				i = coolDown;
			}
			else if(Distance>=0.5f&&Distance<=1.5f)
			{
				Face();
				anim.SetBool("CloseAttack",true);
				anim.SetBool("Attack",false);
				anim.SetBool("Move",false);
				Invoke("AttackOFF",0.1f);
			}
			else
			{
				anim.SetBool("Move",true);
				if(DisP<1.35f)
				{
					Flip ();
				}
				if(facingRight == true)
					transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
				else
					transform.position = new Vector3(transform.position.x - movespeed, transform.position.y);
			}
			}
			
		}
	}
	
	public virtual void kickBack(){}
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		if(!P)
			curP = P2;
		else
			curP = P1;
		P = !P;
	}
	
	void Face()
	{
		Vector3 relativePoint = transform.InverseTransformPoint(Player.transform.position);
		//Debug.Log(relativePoint.x);
		if (relativePoint.x >0.0&&facingRight == true)
		{
			Flip();
		}
		else if (relativePoint.x > 0.0&&facingRight == false)
		{
			Flip();
		}
	}
	
	private void ApplyDamage(float damage)
	{

		if(damage > 0)
		{
			Hp-=damage;
			if(Hp < 0)
				Hp = 0;
			HealthBar.transform.localScale = new Vector3(Hp/MaxHp,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
		}
		
		this.GetComponent<SpriteRenderer>().color = Color.red;
		
		Invoke("ColorBack",.1f);
		kickBack();
		if(Hp<=0)
		{
			anim.SetBool("Dead",true);
			Destroy(this.gameObject,3);
			dead = true;
		}
	}
	
	private void ApplyDamage(object[] damages)
	{
		float totalDamage = (float)damages[0];
		isHpVis(true);
		switch(phy)
		{
		case phyWeakness.Blunt:
			if((int)damages[4] == 1)
				totalDamage *= 1.5f;

			break;
		case phyWeakness.Edge:
			if((int)damages[4] == 2)
				totalDamage *= 1.5f;

			break;
		case phyWeakness.Pierce:
			if((int)damages[4] == 3)
				totalDamage *= 1.5f;

			break;
		}
		if ((float)damages [5] > 0f) {
			poisoned = true;
			Invoke ("PoisonOff", 10f);
		}
		if(totalDamage < 0)
			totalDamage = 0;
		switch(stat)
		{
		case statWeakness.Fire:
			totalDamage += (float)(damages[1])*2;
			totalDamage += (float)(damages[2])/5;
			totalDamage += (float)(damages[3])/5;
			break;
		case statWeakness.Magic:
			totalDamage += (float)damages[1]/5;
			totalDamage += (float)damages[2]*2;
			totalDamage += (float)damages[3]/5;
			break;
		case statWeakness.Artificial:
			totalDamage += (float)damages[1]/5;
			totalDamage += (float)damages[2]/5;
			totalDamage += (float)damages[3]*2;
			break;
		case statWeakness.Physical:
			totalDamage += (float)damages[1]/5;
			totalDamage += (float)damages[2]/5;
			totalDamage += (float)damages[3]/5;
			break;
		}
		if(totalDamage > 0)
		{
			Hp-=totalDamage;
			if(Hp < 0)
				Hp = 0;
			CalcHpBar();
		}
		
		this.GetComponent<SpriteRenderer>().color = Color.red;
		
		Invoke("ColorBack",.1f);
		if(Hp<=0&&!dead)
		{
			this.GetComponentInChildren<Animator>().SetBool("Dead",true);
			Invoke("OFF",3f);
			dead = true;
		}
	}
	
	public override void CalcHpBar()
	{
		HealthBar.transform.localScale = new Vector3(Hp/MaxHp,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
	}

	void ColorBack()
	{
		this.GetComponent<SpriteRenderer>().color = color;
	}
	public virtual void AggroON(){}
	public override void isHpVis(bool on)
	{
		HealthBar.active = on;
		transform.FindChild("BackBar").gameObject.active = on;
	}
	
	public void AttackOFF()
	{
		anim.SetBool("Attack",false);
		anim.SetBool("CloseAttack",false);
		Atk = false;
	}

	public void closeAttack()
	{
		float Distance = Vector2.Distance(this.transform.position,Player.transform.position);
		if(Distance < 2f)
		{
			i = coolDown;
			Atk = false;
			object[] tempSortage = new object[6];
			
			tempSortage[0] = Damage; //Physical
			tempSortage[1] = 0f; //Fire
			tempSortage[2] = 0f; //Magic
			tempSortage[3] = 0f; //Art
			tempSortage[4] = 0;  //Type (Edge,Blunt,Peirce)

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

	public void playSlash()
	{
		source.pitch = .7f;

		source.PlayOneShot(slashSound);
		Invoke ("PitchNormal", 1f);
	}

	public void playThud()
	{
		source.pitch = .5f;
		source.volume = .8f;
		source.PlayOneShot(slashSound);
		Invoke ("PitchNormal", 1f);
		Invoke ("SoundNormal", 1f);
	}


	void PitchNormal()
	{
		source.pitch = 1f;
	}

	void SoundNormal()
	{
		source.volume = .6f;
	}



}
