using UnityEngine;
using System.Collections;

public class NatureSpriteAI: EnemyBaseAI {
	
	public float atkZone = 2f;
	public float rangeAtkZone = 6f;
	public bool hit = false;
	public int amountPushBack = 1;
	float m = 0;
	float i = 0;
	public float coolDown;
	public GameObject HealthBar;
	public Color color;
	GameObject Thorn;

	AudioClip slashSound;
	AudioClip shootSound;

	[SerializeField] bool isBlue;
	
	void Start () 
	{
		color = this.GetComponentInChildren<SpriteRenderer>().color;
		Player = GameObject.FindGameObjectWithTag("Player");
		anim = this.GetComponent<Animator>();
		if(!isBlue)
			Thorn = Resources.Load("NatureLeaf") as GameObject;
		else
			Thorn = Resources.Load("NatureSpriteBlueRange") as GameObject;
		HealthBar = transform.Find("Health").gameObject;
		isHpVis(false);
		facingRight = false;
		movespeed = 0.04f;

		source = GetComponent<AudioSource>();
		slashSound = Resources.Load ("Swoosh")as AudioClip;
		shootSound = Resources.Load ("ThrowSound")as AudioClip;
	}
	
	
	void Update () 
	{
		if(i>0)
		{
			i-=Time.deltaTime;
		}
		if(m>0)
		{
			m-=Time.deltaTime;
		}
		if(psn>0)
		{
			psn-=Time.deltaTime;
		}
		if(dead == false)
		{
			float Distance = Vector2.Distance(this.transform.position,Player.transform.position);

			if (poisoned&&psn<=0) {
				this.GetComponent<SpriteRenderer>().color = Color.red;
				Invoke("ColorBack",.1f);
				Hp -= psnDamage;
				if (Hp < 0f)
					Hp = 0f;
				CalcHpBar();
				if(Hp<=0)
				{
					isHpVis (false);
					anim.SetBool("Dead",true);
					Invoke("OFF",3f);
					dead = true;
				}
				psn = 2f;
			}

			if(Distance < rangeAtkZone&&m <= 0)
			{
				Atk = true;
			}

			
			if(Distance <= atkZone&&i <= 0&&hit==false)
			{
				Face();
				anim.SetBool("Attack",true);
				Invoke("ATTACKOFF",0.1f);
				i = coolDown;
				m = 1f;
			}
			else if(Distance <= rangeAtkZone&&Distance > atkZone&&i <= 0&&hit==false)
			{
				Face();
				anim.SetBool("Range",true);
				Invoke("ATTACKOFF",0.1f);
				i = coolDown*2;
				m = 1f;
			}
			if(hit == true)
			{
				//Debug.Log ("HERE");
				if(facingRight == true)
					transform.position = new Vector3(transform.position.x - movespeed*2, transform.position.y);
				else
					transform.position = new Vector3(transform.position.x + movespeed*2, transform.position.y);
			}
			else if(Atk == true&&m<=0)
			{
				if(Distance>atkZone)
				{
					if(facingRight == true)
						transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
					else
						transform.position = new Vector3(transform.position.x - movespeed, transform.position.y);
					Vector3 relativePoint = transform.InverseTransformPoint(Player.transform.position);
					if(facingRight == true && relativePoint.x > 0.0)
					{
						Flip();
					}
					else if(facingRight == false && relativePoint.x > 0.0)
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
		if(facingRight == true && relativePoint.x > 0.0)
		{
			Flip();
		}
		else if(facingRight == false && relativePoint.x > 0.0)
		{
			Flip ();
		}
	}
	

	
	private void ApplyDamage(object[] damages)
	{

		isHpVis(true);
		kickBack ();
		float totalDamage = (float)damages[0];
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
		if(Hp<=0)
		{
			anim.SetBool("Dead",true);
			isHpVis (false);
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
	public void shoot()
	{

		if (!isBlue) {
			playThrow ();
			Transform firePoint = this.transform.FindChild ("LeafExit");
			Transform firePoint1 = this.transform.FindChild ("LeafExit 1");
			Transform firePoint2 = this.transform.FindChild ("LeafExit 2");
			GameObject bul = Instantiate (Thorn, firePoint.position, Thorn.transform.rotation)as GameObject;
			GameObject bul1 = Instantiate (Thorn, firePoint1.position, Thorn.transform.rotation)as GameObject;
			GameObject bul2 = Instantiate (Thorn, firePoint2.position, Thorn.transform.rotation)as GameObject;
			bul.GetComponent<BD> ().damage = this.Damage;
			bul1.GetComponent<BD> ().damage = this.Damage;
			bul2.GetComponent<BD> ().damage = this.Damage;
			if (facingRight == true) {
				Vector3 theScale = bul.transform.localScale;
				theScale.x *= -1;
				bul.transform.localScale = theScale;
				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (15, 0);
				bul2.transform.localScale = theScale;
				bul2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (15, 0);
				bul1.transform.localScale = theScale;
				bul1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (15, 0);
			} else {
				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-15, 0);
				bul1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-15, 0);
				bul2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-15, 0);
			}
			Destroy (bul, 1f);
			Destroy (bul1, 1f);
			Destroy (bul2, 1f);

		} else {
			playThrow ();
			Transform firePoint = this.transform.FindChild ("LeafExit");
			GameObject bul = Instantiate (Thorn, firePoint.position, Thorn.transform.rotation)as GameObject;
			bul.GetComponent<BD> ().damage = this.Damage;
			if (facingRight == true) {
				Vector3 theScale = bul.transform.localScale;
				theScale.x *= -1;
				bul.transform.localScale = theScale;
				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (15, 0);
				} else {
				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-15, 0);
				}
			Destroy (bul, 1f);
		}

	}
	public virtual void kickBack()
	{
		hit = true;
		Invoke ("hitOFF", .4f);
		agro = true;
		Atk = false;
	}
	void hitOFF()
	{
		hit = false;
	}
	void ATTACKOFF()
	{
		anim.SetBool("Attack",false);
		anim.SetBool("Range",false);
	}
	public virtual void AggroON(){}
	public override void isHpVis(bool on)
	{
		HealthBar.active = on;
		transform.FindChild("BackBar").gameObject.active = on;
	}
	public void Slash()
	{
		float Distance = Vector2.Distance(this.transform.position,Player.transform.position);
		if(Distance<1.5f)
		{
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

	public void playLargeSlash()
	{
		source.volume = .6f;
		source.pitch = 1.2f;
		source.PlayOneShot(slashSound);
		Invoke ("SoundNormal", .5f);
		Invoke ("PitchNormal", .5f);
	}

	public void playThrow()
	{
		source.volume = .6f;
		source.PlayOneShot(shootSound,.6f);
		Invoke ("SoundNormal", .1f);
	}

	void PitchNormal()
	{
		source.pitch = 1f;
	}

	void SoundNormal()
	{
		source.volume = 1f;
	}
}