using UnityEngine;
using System.Collections;

public class AIPlatformer : MonoBehaviour {

	public float Hp = 5;
	public float MaxHp = 5;
	public float Damage = 10;
	[SerializeField] bool dead = false;
	public int souls;
	public GameObject[] items;
	public GameObject chest;

	[SerializeField] bool atk = false;
	public float attackRange = 0.2f;
	public bool jump = false;
	public bool cJump = false;
	[SerializeField] bool follow = false;
	[SerializeField] bool rtnStartR = false;
	[SerializeField] bool rtnStartL = false;
	public float followRange = 0.5f;
	public GameObject player;
	[SerializeField] GameObject upNode;
	[SerializeField] GameObject startNode;
	[SerializeField] float movespeed = 0.05f;
	Animator anim;
	bool facingRight = true;
	[SerializeField] float coolDown;

	[SerializeField] phyWeakness phy;
	[SerializeField] statWeakness stat;
	[SerializeField] float defense;
	GameObject HealthBar;
	Color color;

	[SerializeField] private LayerMask whatIsGround;
	private Transform groundCheck;
	private float groundedRadius = .2f; 
	[SerializeField] bool grounded;
	[SerializeField] private float jumpForce = 400f;
	float j = 0;
	float i = 0;
	[SerializeField]float m = 0;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		anim = this.GetComponent<Animator>();
		groundCheck = transform.Find("GroundCheck");
		HealthBar = transform.Find("Health").gameObject;
		color = this.GetComponent<SpriteRenderer>().color;
	}

	void FixedUpdate () 
	{
		grounded = Physics2D.OverlapCircle(groundCheck.position, groundedRadius, whatIsGround);
		float Distance = Vector2.Distance(this.transform.position,player.transform.position);
		anim.SetBool("Ground", grounded);
		if(i>0) //cooldown
		{
			i-=Time.deltaTime;
		}
		if(m>0) //move cooldown
		{
			m-=Time.deltaTime;
		}
		if(follow&&!dead)
		{
			if(jump == true)
			{
				if(grounded == true)
				{
					//GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
					Invoke ("JumpOFF",.01f);
				}
			}
			if(cJump == true)
			{
				if(grounded == true&&GameObject.FindGameObjectWithTag("Player").transform.position.y-this.transform.position.y > 0.3f)
				{
					GetComponent<Rigidbody2D>().AddForce(new Vector2(0f, jumpForce));
					Invoke ("JumpOFF",.01f);
				}
				else
				{
					cJump = false;
					player = GameObject.FindGameObjectWithTag("Player");
				}
			}
			if(rtnStartR)//stop and wait
			{
				if(player.transform.position.x < transform.position.x)
				{
					m = 0f;
					rtnStartR = false;
				}
				else
					m = 5f;
			}
			if(rtnStartL)//stop and wait
			{
				if(player.transform.position.x > transform.position.x)
				{
					m = 0f;
					rtnStartL = false;
				}
				else
					m = 5f;
			}

			if(!grounded)
				movespeed = 0.07f;
			else
			{
				movespeed = 0.05f;
				//GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				if(!jump&&!cJump&&!rtnStartR&&!rtnStartL)
				{
					if(upNode==null)
						player = GameObject.FindGameObjectWithTag("Player");
					else
						player = upNode;
				}
			}

			if(Distance <= attackRange&&i <= 0&&player.Equals(GameObject.FindGameObjectWithTag("Player")))
			{
				face();
				i = coolDown;
				m = 2f;
				anim.SetBool("Move",false);
				anim.SetBool("Attack",true);
				Invoke("AtkOFF",1f);

			}
			else if(m<=0&&(player.transform.position.x - transform.position.x > .1f || player.transform.position.x - transform.position.x < -.1f))
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
			}
			else
			{
				anim.SetBool("Move",false);
			}
			if(Distance <= 0.2f&&upNode!=null)
			{
				player = GameObject.FindGameObjectWithTag("Player");
				upNode = null;
			}
			
			if(player.transform.position.y - transform.position.y > 2.5f&&upNode==null)
			{
				upNode = findUpNode();
				player = upNode;
			}

		}
		else
		{
			if(Distance <= followRange)
				follow = true;
		}


	}
	void JumpOFF()
	{
		jump = false;
		cJump = false;
	}
	public void ReturnStart(bool right, bool left)
	{
		face ();
		rtnStartR = right;
		rtnStartL = left;
	}
	public enum phyWeakness
	{
		Edge, Blunt, Pierce
	};
	
	public enum statWeakness
	{
		Magic, Fire, Artificial, Physical
	};
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
	GameObject findUpNode()
	{
		GameObject[] upNodes = GameObject.FindGameObjectsWithTag("UpNode");
		float dist = Vector2.Distance(this.transform.position,upNodes[0].transform.position);
		int index = 0;
		for(int i = 1;i<upNodes.Length;i++)
		{
			float DIS = Vector2.Distance(this.transform.position,upNodes[i].transform.position);
			if(DIS<dist)
			{
				dist = DIS;
				index = i;
			}
		}
		return upNodes[index];
	}
	private void ApplyDamage(float damage)
	{
		if(damage > 0)
		{
			Hp-=damage;
			if(Hp < 0)
				Hp = 0;
			//HealthBar.transform.localScale = new Vector3(Hp/MaxHp,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
		}
		
		this.GetComponent<SpriteRenderer>().color = Color.red;
		
		Invoke("ColorBack",.1f);
		if(Hp<=0)
		{
			this.GetComponentInChildren<Animator>().SetBool("Dead",true);
			Destroy(this.gameObject,3);
			dead = true;
		}
	}

	void face()
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
	}
	
	private void ApplyDamage(object[] damages)
	{
		
		float totalDamage = (float)damages[0];
		float def = (defense/2)/100;
		switch(phy)
		{
		case phyWeakness.Blunt:
			if((int)damages[4] == 1)
				totalDamage *= 1.5f;
			else
				totalDamage -= totalDamage * def;
			break;
		case phyWeakness.Edge:
			if((int)damages[4] == 2)
				totalDamage *= 1.5f;
			else
				totalDamage -= totalDamage * def;
			break;
		case phyWeakness.Pierce:
			if((int)damages[4] == 3)
				totalDamage *= 1.5f;
			else
				totalDamage -= totalDamage * def;
			break;
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
			follow = true;
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
	void OFF()
	{
		alwasyDropItem();
		Destroy(this.gameObject);
	}
	void HealOff()
	{
		anim.SetBool("Heal",false);
	}

	void ColorBack()
	{	
		this.GetComponent<SpriteRenderer>().color = color;	
	}
	public void Heal()
	{
		Hp = MaxHp;
		CalcHpBar();
	}
	void dropItem()
	{
		int RND = Random.Range(0,items.Length * 2);
		if(RND < items.Length)
		{
			Instantiate(items[RND],this.transform.position,items[RND].transform.rotation);
		}
	}
	void alwasyDropItem()
	{
		chest.active = true;
		//Instantiate(items[0],this.transform.position,items[0].transform.rotation);
	}
	public void CalcHpBar(){
		HealthBar.transform.localScale = new Vector3(Hp/MaxHp,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
	}
	public void slash()
	{
		float Distance = Vector2.Distance(this.transform.position,player.transform.position);
		
		if(Distance <= attackRange)
		{
			i = coolDown;
			//Atk = false;
			object[] tempSortage = new object[6];
			
			tempSortage[0] = Damage; //Physical
			tempSortage[1] = 0f; //Fire
			tempSortage[2] = 0f; //Magic
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
	void AtkOFF()
	{
		this.GetComponentInChildren<Animator>().SetBool("Attack",false);
	}
}
