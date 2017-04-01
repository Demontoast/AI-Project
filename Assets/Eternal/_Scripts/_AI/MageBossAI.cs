using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MageBossAI : EnemyBaseAI {

	bool atk = false;
	float attackRange = 5f;
	float rangeAttackRange = 7f;
	float aoEAttackRange = 2f;

	bool transformOne = false;
	bool transformTwo = false;
	bool transforming = false;

	bool jump = false;
	bool follow = false;
	bool dash = false;
	
	GameObject player;

	float coolDown = 2f;

	Slider HealthBar;
	Color color;
	
	[SerializeField] private LayerMask whatIsGround;
	private Transform groundCheck;
	private float groundedRadius = .2f; 
	bool grounded;
	private float jumpForce = 400f;
	float j = 0;
	float i = 0;
	float m = 0;
	GameObject Canvas;

	GameObject magicBall;
	GameObject HommingBall;
	GameObject TransHommingBall;
	GameObject TransFollowBall;
	GameObject FollowBall;
	GameObject MageGaintBall;
	GameObject TransShootBall;
	GameObject TransShootingBall;


	[SerializeField]Transform FlyZone;

	GameObject Homing1;
	GameObject Homing2;
	GameObject Homing3;
	GameObject Homing4;
	GameObject Homing5;
	GameObject Homing6;
	GameObject Homing7;

	[SerializeField]GameObject[] RaiseSpells;


	[SerializeField]GameObject Barrier;
	[SerializeField]GameObject trigger;
	[SerializeField]GameObject mask;

	AudioClip magicSound;
	AudioClip fireSound;
	AudioClip dodgeSound;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		anim = this.GetComponent<Animator>();
		groundCheck = transform.Find("GroundCheck");
		HealthBar = Canvas.transform.Find("BossInfo/BossHpBar").GetComponent<Slider>();
		color = this.GetComponent<SpriteRenderer>().color;
		magicBall = Resources.Load("MageMagicBall") as GameObject;
		HommingBall = Resources.Load("MageHommingBall") as GameObject;
		FollowBall = Resources.Load("MageFollowBall") as GameObject;
		TransHommingBall = Resources.Load("MageTransHommingBall") as GameObject;
		TransFollowBall = Resources.Load("MageTransFollowBall") as GameObject;
		MageGaintBall = Resources.Load("MageGaintBall") as GameObject;
		TransShootBall = Resources.Load("MageTransShootBall") as GameObject;
		TransShootingBall = Resources.Load("MageTransShootingBall") as GameObject;
		facingRight = false;
		movespeed = 0.05f;
		source = GetComponent<AudioSource> ();
		magicSound = Resources.Load ("MagicAttack2")as AudioClip;
		fireSound = Resources.Load ("FireAttack")as AudioClip;
		dodgeSound = Resources.Load ("Dodge")as AudioClip;
	}

	

	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		float playerDist = Vector2.Distance(this.transform.position,player.transform.position);
		anim.SetBool("Ground", grounded);
		GetComponent<Rigidbody2D>().velocity = new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
		if(!dead&&agro)
		{

			if(i>0) //cooldown
			{
				i -= Time.deltaTime;
			}
			if(m>0) //move cooldown
			{
				m-=Time.deltaTime;
			}

			if(psn>0) //move cooldown
			{
				psn-=Time.deltaTime;
			}


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

					Canvas.transform.FindChild("BossInfo").gameObject.active = false;
					Barrier.GetComponent<Animator>().SetBool("Up",false);
					Barrier.GetComponent<Animator>().SetBool("Down",true);
					for(int k = 0;k<RaiseSpells.Length;k++)
					{
						RaiseSpells[k].active = false;
					}
					trigger.active = false;
					//dead = true;
					//gameObject.active = false;
					anim.SetBool("Dead",true);
					Invoke ("Dead",2f);
					if(chest!=null)
						chest.active = true;
				}
				psn = 2f;
			}
			
			//////////////////////////////////////////////////

				
//				if(playerDist > followRange+2f&&follow&&!dash&&grounded)
//				{
//					dash = true;
//				}
			if(!transforming&&((Hp<900f&&Hp>500&&transformOne==false)||(Hp<500f&&transformTwo==false&&transformOne==true)))
			{
				transforming=true;
			}

			if(!transforming&&!transformTwo)
			{
			if(playerDist > attackRange&&playerDist<rangeAttackRange&&i<=0&&grounded)
			{
				Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
				if(facingRight == true && relativePoint.x < 0.0)
				{
					Flip();
				}
				else if(facingRight == false && relativePoint.x < 0.0)
				{
					Flip ();
				}
				i = coolDown;
				m = 2f;
				anim.SetBool("Move",false);
				anim.SetBool("Roll",false);
				anim.SetBool("Range",true);
				Invoke("AtkOFF",1.3f);
			}
			else if(playerDist > aoEAttackRange&&playerDist < attackRange&&i<=0&&grounded)
			{
					Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
					if(facingRight == true && relativePoint.x < 0.0)
					{
						Flip();
					}
					else if(facingRight == false && relativePoint.x < 0.0)
					{
						Flip ();
					}
					i = coolDown;
					m = 2f;
					anim.SetBool("Move",false);
					anim.SetBool("Roll",false);
					anim.SetBool("Attack",true);
					Invoke("AtkOFF",1f);
			}
			else if(playerDist < aoEAttackRange&&i<=0&&grounded)
			{
				Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
				if(facingRight == true && relativePoint.x < 0.0)
				{
					Flip();
				}
				else if(facingRight == false && relativePoint.x < 0.0)
				{
					Flip ();
				}
				i = coolDown;
				m = 2f;
				anim.SetBool("Move",false);
				anim.SetBool("Roll",false);
				int RND = Random.Range (0, 3);
				if (RND == 2)
					anim.SetBool ("Attack", true);
				else
					anim.SetBool("AoE",true);
				Invoke("AtkOFF",1f);
			}
				//////////////////////////////////////////////////

				if(dash&&m<=0)
				{
					anim.SetBool("Roll",true);
					if(facingRight == true)
						transform.position = new Vector3(transform.position.x + 0.1f, transform.position.y);
					else
						transform.position = new Vector3(transform.position.x - 0.1f, transform.position.y);
					Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
					if(facingRight == true && relativePoint.x < 0.0)
					{
						Flip();
					}
					else if(facingRight == false && relativePoint.x < 0.0)
					{
						Flip ();
					}
					Invoke ("DodgeOff",0.5f);
				}
				else if(agro == true&&m<=0&&playerDist>aoEAttackRange&&(player.transform.position.x>this.transform.position.x&&player.transform.position.x-this.transform.position.x>1f||player.transform.position.x<this.transform.position.x&&this.transform.position.x-player.transform.position.x>1f))
				{
					anim.SetBool("Move",true);
					if(facingRight == true)
						transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
					else
						transform.position = new Vector3(transform.position.x - movespeed, transform.position.y);
					Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
					if(facingRight == true && relativePoint.x < 0.0)
					{
						Flip();
					}
					else if(facingRight == false && relativePoint.x < 0.0)
					{
						Flip ();
					}

				int RND = Random.Range(0,4);
				if(RND == 2&&grounded)
				{
					
				}
				}
			}
			else if(transformTwo)
			{
				if(m<=0)
				{
					if(transform.position.x>FlyZone.transform.position.x+1)
						transform.position = new Vector3(transform.position.x +- movespeed, transform.position.y);
					else if(transform.position.x<FlyZone.transform.position.x-1)
						transform.position = new Vector3(transform.position.x + movespeed, transform.position.y);
					
					if(transform.position.y>FlyZone.transform.position.y+1)
						transform.position = new Vector3(transform.position.x, transform.position.y-movespeed);
					else if(transform.position.y<FlyZone.transform.position.y-1)
						transform.position = new Vector3(transform.position.x, transform.position.y+movespeed);
				}

				if(i<=0)
				{
					int RND = Random.Range(0,3);
					switch(RND)
					{
					case 0:
						anim.SetBool("Attack",true);
						Invoke("AtkOFF",1f);
						i = 8;
						m = 5;
						break;
					case 1:
						anim.SetBool("Range",true);
						Invoke("AtkOFF",1f);
						i = 5;
						m = 5;
						break;
					case 2:
						anim.SetBool("AoE",true);
						Invoke("AtkOFF",1f);
						i = 5;
						m = 5;
						break;
					}
				}
			}
			else
			{
				transforming = false;
				if(transformOne==false)
				{
					transformOne = true;
					anim.SetBool("Transform",true);
					AtkOFF();
					m = 5;
					i = 10;
					//Invoke("transformOFF",2.1f);
				}
				else if(transformTwo==false)
				{
					transformTwo = true;
					this.GetComponent<Rigidbody2D>().gravityScale = 0f;
					anim.SetBool("Transform",true);
					AtkOFF();
					m = 5;
					i = 10;
					//Invoke("transformOFF",1.5f);
				}

			}

		}

		}

	void resetM()
	{
		m = 0;
		i = 2;
	}

	void resetMI()
	{
		m = 0;
		i = 0;
	}

	void transformOFF()
	{
		anim.SetBool("Transform",false);
	}
		
	
	void AtkOFF()
	{
		anim.SetBool("Attack",false);
		anim.SetBool("AoE",false);
		anim.SetBool("Range",false);
	}
	void DodgeOff()
	{
		anim.SetBool("Roll",false);
		dash = false;
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


	void Face()
	{
		Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
		if (relativePoint.x < 0.0)
		{
			//Left
			if(facingRight == true)
				Flip();
		}
		else if (relativePoint.x > 0.0)
		{
			//Right
			if(facingRight == false)
				Flip();
		}
	}

	
	private void ApplyDamage(object[] damages)
	{

		isHpVis(true);
		if(transforming)
			return;
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
		if(Hp<=0&&!dead)
		{
			this.GetComponentInChildren<Animator>().SetBool("Dead",true);
			Canvas.transform.FindChild("BossInfo").gameObject.active = false;
			Barrier.GetComponent<Animator>().SetBool("Up",false);
			Barrier.GetComponent<Animator>().SetBool("Down",true);
			for(int i = 0;i<RaiseSpells.Length;i++)
			{
				RaiseSpells[i].active = false;
			}
			trigger.active = false;
			//dead = true;
			//gameObject.active = false;
			anim.SetBool("Dead",true);
			Invoke ("Dead",2f);
			if(chest!=null)
				chest.active = true;
		}
	}
	void Dead()
	{
		EndDemo ();
		gameObject.active = false;
		GameObject clone = Instantiate(mask,this.transform.position,mask.transform.rotation)as GameObject;

	}
	
	public override void CalcHpBar()
	{
		HealthBar.maxValue=MaxHp;
		HealthBar.value=Hp; 
	}
	
	
	void ColorBack()
	{
		this.GetComponent<SpriteRenderer>().color = color;
	}
	public virtual void AggroON(){}

	public void MagicBlast()
	{
		Transform firePoint = this.transform.FindChild ("MagicPoint");
		GameObject magic = Instantiate(magicBall, firePoint.position,magicBall.transform.rotation)as GameObject;
		//bul.GetComponent<BD>().artDamage = this.transform.parent.parent.GetComponent<Equipment>().mainHand.artDamage;
		if(facingRight == false)
		{
			magic.GetComponent<Rigidbody2D>().velocity = new Vector2(-15,0);
			Vector3 theScale = magic.transform.localScale;
			theScale.x *= -1;
			magic.transform.localScale = theScale;
		}
		else
		{
			magic.GetComponent<Rigidbody2D>().velocity = new Vector2(15,0);
		}
		Destroy(magic,.5f);
	}

	public void GaintMagicBlast()
	{
		Transform firePoint = this.transform.FindChild ("MagicPoint");
		GameObject magic = Instantiate(MageGaintBall, firePoint.position,MageGaintBall.transform.rotation)as GameObject;
		//bul.GetComponent<BD>().artDamage = this.transform.parent.parent.GetComponent<Equipment>().mainHand.artDamage;
		if(facingRight == false)
		{
			magic.GetComponent<Rigidbody2D>().velocity = new Vector2(-15,0);
			Vector3 theScale = magic.transform.localScale;
			theScale.x *= -1;
			magic.transform.localScale = theScale;
		}
		else
		{
			magic.GetComponent<Rigidbody2D>().velocity = new Vector2(15,0);
		}
		Destroy(magic,.7f);
	}

	public void SpawnHomming()
	{
		Transform firePoint1 = this.transform.FindChild ("MagicHomming1");
		Transform firePoint2 = this.transform.FindChild ("MagicHomming2");
		Transform firePoint3 = this.transform.FindChild ("MagicHomming3");
		Homing1 = Instantiate(HommingBall, firePoint1.position,HommingBall.transform.rotation)as GameObject;
		Homing2 = Instantiate(HommingBall, firePoint2.position,HommingBall.transform.rotation)as GameObject;
		Homing3 = Instantiate(HommingBall, firePoint3.position,HommingBall.transform.rotation)as GameObject;
		//Invoke ("MagicFollow",1f);
	}
	void MagicFollow()
	{
		if(Homing1!=null)
		{
			Homing1.GetComponent<SpriteRenderer>().sprite = FollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing1.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing1.gameObject,10f);
		}
		if(Homing2!=null)
		{
			Homing2.GetComponent<SpriteRenderer>().sprite = FollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing2.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing2.gameObject,10f);
		}
		if(Homing3!=null)
		{
			Homing3.GetComponent<SpriteRenderer>().sprite = FollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing3.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing3.gameObject,10f);
		}
	}

	public void SpawnTransHomming()
	{
		Transform firePoint1 = this.transform.FindChild ("TransHomming1");
		Transform firePoint2 = this.transform.FindChild ("TransHomming2");
		Transform firePoint3 = this.transform.FindChild ("TransHomming3");
		Transform firePoint4 = this.transform.FindChild ("TransHomming4");
		Transform firePoint5 = this.transform.FindChild ("TransHomming5");
		Transform firePoint6 = this.transform.FindChild ("TransHomming6");
		Transform firePoint7 = this.transform.FindChild ("TransHomming7");
		Homing1 = Instantiate(TransHommingBall, firePoint1.position,TransHommingBall.transform.rotation)as GameObject;
		Homing2 = Instantiate(TransHommingBall, firePoint2.position,TransHommingBall.transform.rotation)as GameObject;
		Homing3 = Instantiate(TransHommingBall, firePoint3.position,TransHommingBall.transform.rotation)as GameObject;
		Homing4 = Instantiate(TransHommingBall, firePoint4.position,TransHommingBall.transform.rotation)as GameObject;
		Homing5 = Instantiate(TransHommingBall, firePoint5.position,TransHommingBall.transform.rotation)as GameObject;
		Homing6 = Instantiate(TransHommingBall, firePoint6.position,TransHommingBall.transform.rotation)as GameObject;
		Homing7 = Instantiate(TransHommingBall, firePoint7.position,TransHommingBall.transform.rotation)as GameObject;
		//Invoke ("MagicFollow",1f);
	}
	void TransMagicFollow()
	{
		if(Homing1!=null)
		{
			Homing1.GetComponent<SpriteRenderer>().sprite = TransFollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing1.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing1.gameObject,10f);
		}
		if(Homing2!=null)
		{
			Homing2.GetComponent<SpriteRenderer>().sprite = TransFollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing2.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing2.gameObject,10f);
		}
		if(Homing3!=null)
		{
			Homing3.GetComponent<SpriteRenderer>().sprite = TransFollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing3.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing3.gameObject,10f);
		}
		if(Homing4!=null)
		{
			Homing4.GetComponent<SpriteRenderer>().sprite = TransFollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing4.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing4.gameObject,10f);
		}
		if(Homing5!=null)
		{
			Homing5.GetComponent<SpriteRenderer>().sprite = TransFollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing5.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing5.gameObject,10f);
		}
		if(Homing6!=null)
		{
			Homing6.GetComponent<SpriteRenderer>().sprite = TransFollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing6.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing6.gameObject,10f);
		}
		if(Homing7!=null)
		{
			Homing7.GetComponent<SpriteRenderer>().sprite = TransFollowBall.GetComponent<SpriteRenderer>().sprite;
			Homing7.GetComponent<GhostBallAI>().enabled = true;
			Destroy(Homing7.gameObject,10f);
		}
	}

	public void SpawnTransShoot()
	{
		Transform firePoint1 = this.transform.FindChild ("TransHomming1");
		Transform firePoint2 = this.transform.FindChild ("TransHomming2");
		Transform firePoint3 = this.transform.FindChild ("TransHomming3");
		Transform firePoint4 = this.transform.FindChild ("TransHomming4");
		Transform firePoint5 = this.transform.FindChild ("TransHomming5");
		Transform firePoint6 = this.transform.FindChild ("TransHomming6");
		Transform firePoint7 = this.transform.FindChild ("TransHomming7");
		Homing1 = Instantiate(TransShootBall, firePoint1.position,TransShootBall.transform.rotation)as GameObject;
		Homing2 = Instantiate(TransShootBall, firePoint2.position,TransShootBall.transform.rotation)as GameObject;
		Homing3 = Instantiate(TransShootBall, firePoint3.position,TransShootBall.transform.rotation)as GameObject;
		Homing4 = Instantiate(TransShootBall, firePoint4.position,TransShootBall.transform.rotation)as GameObject;
		Homing5 = Instantiate(TransShootBall, firePoint5.position,TransShootBall.transform.rotation)as GameObject;
		Homing6 = Instantiate(TransShootBall, firePoint6.position,TransShootBall.transform.rotation)as GameObject;
		Homing7 = Instantiate(TransShootBall, firePoint7.position,TransShootBall.transform.rotation)as GameObject;
		//Invoke ("MagicFollow",1f);
	}
	void TransMagicShoot()
	{
		if(Homing1!=null)
		{
			Homing1.GetComponent<SpriteRenderer>().sprite = TransShootingBall.GetComponent<SpriteRenderer>().sprite;
			Homing1.GetComponent<ShootingBall>().enabled = true;
			Destroy(Homing1.gameObject,3f);
		}
		if(Homing2!=null)
		{
			Homing2.GetComponent<SpriteRenderer>().sprite = TransShootingBall.GetComponent<SpriteRenderer>().sprite;
			Homing2.GetComponent<ShootingBall>().enabled = true;
			Destroy(Homing2.gameObject,3);
		}
		if(Homing3!=null)
		{
			Homing3.GetComponent<SpriteRenderer>().sprite = TransShootingBall.GetComponent<SpriteRenderer>().sprite;
			Homing3.GetComponent<ShootingBall>().enabled = true;
			Destroy(Homing3.gameObject,3);
		}
		if(Homing4!=null)
		{
			Homing4.GetComponent<SpriteRenderer>().sprite = TransShootingBall.GetComponent<SpriteRenderer>().sprite;
			Homing4.GetComponent<ShootingBall>().enabled = true;
			Destroy(Homing4.gameObject,3);
		}
		if(Homing5!=null)
		{
			Homing5.GetComponent<SpriteRenderer>().sprite = TransShootingBall.GetComponent<SpriteRenderer>().sprite;
			Homing5.GetComponent<ShootingBall>().enabled = true;
			Destroy(Homing5.gameObject,3);
		}
		if(Homing6!=null)
		{
			Homing6.GetComponent<SpriteRenderer>().sprite = TransShootingBall.GetComponent<SpriteRenderer>().sprite;
			Homing6.GetComponent<ShootingBall>().enabled = true;
			Destroy(Homing6.gameObject,3);
		}
		if(Homing7!=null)
		{
			Homing7.GetComponent<SpriteRenderer>().sprite = TransShootingBall.GetComponent<SpriteRenderer>().sprite;
			Homing7.GetComponent<ShootingBall>().enabled = true;
			Destroy(Homing7.gameObject,3);
		}
	}

	void RaiseON()
	{
		for(int i = 0;i<RaiseSpells.Length;i++)
		{
			RaiseSpells[i].active = true;
		}
	}

	void RaiseRaise()
	{
		for(int i = 0;i<RaiseSpells.Length;i++)
		{
			RaiseSpells[i].GetComponent<Animator>().SetBool("Raise",true);
		}
		Invoke ("RaiseOFF",2f);
	}
	void RaiseOFF()
	{
		for(int i = 0;i<RaiseSpells.Length;i++)
		{
			RaiseSpells[i].GetComponent<Animator>().SetBool("Raise",false);
		}
		//Invoke ("RaiseNotActive",1.5f);
	}
	void RaiseNotActive()
	{
		for(int i = 0;i<RaiseSpells.Length;i++)
		{
			RaiseSpells[i].active = false;
		}
	}


	void slash()
	{
		float Distance = Vector2.Distance(this.transform.position,player.transform.position);
		
		if(Distance <= aoEAttackRange)
		{
		
			i = coolDown;
			//Atk = false;
			object[] tempSortage = new object[6];
			
			tempSortage[0] = 0f; //Physical
			tempSortage[1] = 0f; //Fire
			tempSortage[2] = Damage; //Magic
			tempSortage[3] = 0f; //Art
			tempSortage[4] = 0;  //Type (Edge,Blunt,Peirce)
			tempSortage[5] = 0f; //Poison 
			

			

				if(player.GetComponent<Platformer2DUserControl>().isBlocking())
				{
					Vector3 RP = player.transform.InverseTransformPoint(this.transform.position);
					if(RP.x > 0.0)
					{
						//Player.SendMessage("ApplyDamage",0,SendMessageOptions.DontRequireReceiver);
						player.SendMessage("StamDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
				}
				else
					player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
			}

	}

	void swordBack()
	{
		float Distance = Vector2.Distance(this.transform.position,player.transform.position);
		
		if(Distance <= attackRange)
		{
			bool atk = true;
			i = coolDown;
			//Atk = false;
			object[] tempSortage = new object[6];
			
			tempSortage[0] = 0f; //Physical
			tempSortage[1] = 0f; //Fire
			tempSortage[2] = Damage; //Magic
			tempSortage[3] = 0f; //Art
			tempSortage[4] = 0;  //Type (Edge,Blunt,Peirce)
			tempSortage[5] = 0f; //Poison 
			
			Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
			if(facingRight == true && relativePoint.x > 0.0)
			{
				atk = false;
			}
			else if(facingRight == false && relativePoint.x > 0.0)
			{
				atk = false;
			}
			
			if(atk)
			{
				if(player.GetComponent<Platformer2DUserControl>().isBlocking())
				{
					Vector3 RP = player.transform.InverseTransformPoint(this.transform.position);
					if(RP.x > 0.0)
					{
						//Player.SendMessage("ApplyDamage",0,SendMessageOptions.DontRequireReceiver);
						player.SendMessage("StamDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
				}
				else
					player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
			}
		}

		
	}

	void swordFront()
	{
		float Distance = Vector2.Distance(this.transform.position,player.transform.position);
		
		if(Distance <= attackRange)
		{
			bool atk = true;
			i = coolDown;
			//Atk = false;
			object[] tempSortage = new object[6];
			
			tempSortage[0] = 0f; //Physical
			tempSortage[1] = 0f; //Fire
			tempSortage[2] = Damage; //Magic
			tempSortage[3] = 0f; //Art
			tempSortage[4] = 0;  //Type (Edge,Blunt,Peirce)
			tempSortage[5] = 0f; //Poison 
			
			Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
			if(facingRight == true && relativePoint.x < 0.0)
			{
				atk = false;
			}
			else if(facingRight == false && relativePoint.x < 0.0)
			{
				atk = false;
			}
			if(player.transform.position.y>this.transform.position.y)
				atk = false;
			
			if(atk)
			{
				if(player.GetComponent<Platformer2DUserControl>().isBlocking())
				{
					Vector3 RP = player.transform.InverseTransformPoint(this.transform.position);
					if(RP.x > 0.0)
					{
						//Player.SendMessage("ApplyDamage",0,SendMessageOptions.DontRequireReceiver);
						player.SendMessage("StamDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
				}
				else
					player.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
			}
		}
		
		
	}

	void EndDemo()
	{
		Canvas.transform.FindChild ("FadeIn").gameObject.active = true;
		Invoke ("Demo",1.5f);
	}
	void Demo()
	{
		Application.LoadLevel("AsireAfter");
	}

	void playMagic () {
		source.PlayOneShot (magicSound);
	}
	void playFire () {
		source.PlayOneShot (fireSound);
	}

	void playFastMagic () {
		source.pitch = 1.5f;
		source.PlayOneShot (magicSound);
		Invoke ("PitchNormal", .1f);
	}
	void PitchNormal()
	{
		source.pitch = 1f;
	}


	public void playDodge()
	{
		source.pitch = 1.7f;
		source.PlayOneShot(dodgeSound,.6f);
		Invoke ("PitchNormal", .1f);
	}
	
}

