﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlackKnigtPathfindingAI : EnemyBaseAI {

	ArrayList<GameObject> path;
	[SerializeField] bool follow;
	public float walkZone = 2f;

	[SerializeField] private LayerMask whatIsGround;
	private Transform groundCheck;
	private float groundedRadius = .2f; 
	[SerializeField] bool grounded;
	[SerializeField] private float jumpForce = 300f;
	float i = 0;
	float a = 0;
	float m = 0;

	float coolDown = 1.5f;
	public GameObject HealthBar;
	public Color color;
	int amountPushBack = 1;
	AudioClip slashSound;
	AudioClip shoot;
	AudioClip strong;

	float attackRange = 2f;
	float closeAttackRange = 1f;
	bool hit = false;
	bool jump = false;
	float followRange = 5f;

	GameObject HpBar;
	GameObject aura;

	float j = 0;

	GameObject Strong;


	void Start()
	{
		Player = GameObject.FindGameObjectWithTag ("Player");
		color = this.GetComponent<SpriteRenderer>().color;
		HealthBar = transform.FindChild ("Health").gameObject;
		groundCheck = transform.Find("GroundCheck");
		grounded = true;
		anim = this.GetComponent<Animator>();
		isHpVis(false);
		source = GetComponent<AudioSource>();
		slashSound = Resources.Load ("Swoosh")as AudioClip;
		Strong = Resources.Load ("BlackKnightStrongAttack")as GameObject;
		source = GetComponent<AudioSource>();
		shoot = Resources.Load ("ThrowSound")as AudioClip;
		aura = Resources.Load ("BlackKnightAura")as GameObject;
		StartPathFinding ();

	}

	void FixedUpdate()
	{
		if (i > 0) {
			i -= Time.deltaTime;
		}
		if (a > 0) {
			a -= Time.deltaTime;
		}
		if (m > 0) {
			m -= Time.deltaTime;
		}
		if (psn > 0) {
			psn -= Time.deltaTime;
		}
		float Distance = Vector2.Distance (this.transform.position, Player.transform.position);
		if (poisoned&&psn<=0) {
			this.GetComponent<SpriteRenderer>().color = Color.red;
			Invoke("ColorBack",.1f);
			Hp -= psnDamage;
			if (Hp < 0f)
				Hp = 0f;
			CalcHpBar();
			if(Hp<=0&&!dead)
			{
				anim.SetBool("Dead",true);
				Invoke("OFF",3f);
				dead = true;
				//gameObject.active = false;
				if(chest!=null)
					chest.active = true;
			}
			psn = 2f;
		}
		if (Distance > 15) {
			agro = false;
			anim.SetBool ("Move", false);
		}
		if (!agro) {
			if (Distance < walkZone)
				agro = true;
		}

		if (dead == false&&agro&&!hit) {
			grounded = Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);

			anim.SetBool ("Ground", grounded);
			if (!grounded) {
				movespeed = 0.1f;
				//GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
			} else
				movespeed = 0.05f;


			if (Distance > attackRange && Distance < attackRange + 3 && Hp <= MaxHp / 2 && a <= 0) {
				Face ();
				m = 4;
				anim.SetBool ("Strong", true);
				anim.SetBool ("Move", false);
				a = coolDown + 2;
			} else if (Distance > attackRange && Distance < attackRange + 3 && a <= 0) {
				Face ();
				m = 2;
				anim.SetBool ("Range", true);
				anim.SetBool ("Move", false);
				a = coolDown+1;
			} else if (Distance < attackRange && a <= 0) {
				Face ();
				m = 2;
				anim.SetBool ("Attack", true);
				anim.SetBool ("Move", false);
				a = coolDown;

			}




			else if (Distance > attackRange && Distance < walkZone&&m<=0) {
				WalkTowards (Distance);
			}

			else if (Distance > walkZone && i <= 0&&m<=0) {
				StartPathFinding ();
				i = 3;
			}
			else if (path.size () > 0&&m<=0) {
				Move ();
			} else if(m<=0) {
				anim.SetBool ("Move", false);
				if (Distance > walkZone) {
					StartPathFinding ();
					i = 3;
				}
			}
		}

	}

	void StartPathFinding()
	{
		GameObject[] nodes = GameObject.FindGameObjectsWithTag ("Node");
		float smallestP =  Vector3.Distance (nodes[0].transform.position, Player.transform.position);
		int indexP = 0;
		float smallestE =  Vector3.Distance (nodes[0].transform.position, this.transform.position);
		int indexE = 0;
		for (int i = 0; i < nodes.Length; i++) {
			float PlayerDis = Vector3.Distance (nodes[i].transform.position, Player.transform.position);
			float enemyDis = Vector3.Distance (nodes[i].transform.position, this.transform.position);
			if (PlayerDis < smallestP) {
				smallestP = PlayerDis;
				indexP = i;
			}
			if (enemyDis < smallestE) {
				smallestE = enemyDis;
				indexE = i;
			}
		}
		//Debug.Log ("Start: " + nodes [indexE].gameObject.name + " Final: " + nodes [indexP].gameObject.name);
		path = findPath (nodes[indexE],nodes[indexP]);
		//Debug.Log(path.get(path.size()-1));
		//for (int i = 0; i < path.size (); i++) {
		//	Debug.Log (path.get(i).gameObject.name);
		//}
	}

	ArrayList<GameObject> findPath(GameObject First, GameObject Final)
	{
		ArrayList<GameObject> touchedNodes = new ArrayList<GameObject> ();
		ArrayList<GameObject> notEvaulatedNodes = new ArrayList<GameObject> ();
		notEvaulatedNodes.add(First);
		Dictionary<GameObject, GameObject> cameFrom = new Dictionary<GameObject, GameObject>();

		while(notEvaulatedNodes.size()!=0)
		{
			GameObject current = notEvaulatedNodes.get (0);
			//Debug.Log ("Current: " + current.gameObject.name);
			if (current.Equals (Final)) {
				return reverse (cameFrom, Final);
			}
			notEvaulatedNodes.remove (0);
			touchedNodes.add (current);
			for (int i = 0; i < 4; i++) {

				if (current.GetComponent<NodePath>().AdjNodes [i] != null) {
					//Debug.Log ("Adj: " + current.GetComponent<NodePath>().AdjNodes [i].gameObject.name);
					if (!touchedNodes.isIn (current.GetComponent<NodePath>().AdjNodes [i].gameObject)) {
						//Debug.Log ("Has Not Been Touched: " + current.GetComponent<NodePath>().AdjNodes [i].gameObject.name);
						touchedNodes.add (current.GetComponent<NodePath>().AdjNodes [i].gameObject);
						if (!notEvaulatedNodes.isIn (current.GetComponent<NodePath>().AdjNodes [i].gameObject)) {
							notEvaulatedNodes.add (current.GetComponent<NodePath>().AdjNodes [i].gameObject);
						}
						//Debug.Log ("CAMEFROM " + current.GetComponent<NodePath>().AdjNodes [i].gameObject.name + " " + current.gameObject.name);
						cameFrom.Add (current.GetComponent<NodePath>().AdjNodes[i].gameObject, current);
					} 
					else {
						//Debug.Log ("Already Touched: " + current.GetComponent<NodePath>().AdjNodes [i].gameObject.name);
					}
				}
				//else
				//Debug.Log ("Null Node");
			}
		}
		return null;
	}

	ArrayList<GameObject> reverse(Dictionary<GameObject, GameObject> cameFrom,  GameObject current)
	{
		ArrayList<GameObject> TotalPath = new ArrayList<GameObject> ();
		TotalPath.add (current);
		while (cameFrom.ContainsKey (current)) {
			GameObject x = null;
			if(cameFrom.TryGetValue(current, out x))
			{
				current = cameFrom [current];
				TotalPath.addFirst (current);
			}
		}
		return TotalPath;
	}

	void Move()
	{
		GameObject currentNode = path.get (0);
		//Debug.Log ("Current: "+currentNode);
		float TargetDist = Vector2.Distance(groundCheck.transform.position,currentNode.transform.position);
		//Debug.Log (TargetDist);
		if (TargetDist < .45 && grounded) {
			if (path.size () > 1) {
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
				bool jump = WalkOrJump (currentNode.GetComponent<NodePath> (), path.get (1).GetComponent<NodePath> ());
				//Debug.Log ("Jump");
				path.remove (0);
				currentNode = path.get (0);
				//Debug.Log ("NEXT");
				//Debug.Log ("Next: "+currentNode);
				if (jump) {
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
				}
			} else
				path.remove (0);

		} else if (TargetDist >= .45 && (currentNode.transform.position.x > groundCheck.transform.position.x && currentNode.transform.position.x - groundCheck.transform.position.x > .05f || currentNode.transform.position.x < groundCheck.transform.position.x && groundCheck.transform.position.x - currentNode.transform.position.x > .05f)) {
			anim.SetBool ("Move", true);
			if (facingRight == true)
				transform.position = new Vector3 (transform.position.x + movespeed, transform.position.y);
			else
				transform.position = new Vector3 (transform.position.x - movespeed, transform.position.y);
			Vector3 relativePoint = transform.InverseTransformPoint (currentNode.transform.position);
			if (facingRight == true && relativePoint.x < 0.0) {
				Flip ();
			} else if (facingRight == false && relativePoint.x < 0.0) {
				Flip ();
			}
		} else {
			anim.SetBool ("Move", false);
		}
	}

	void WalkTowards(float Distance)
	{
		if (Distance >= .45 && (Player.transform.position.x > this.transform.position.x && Player.transform.position.x - this.transform.position.x > .05f || Player.transform.position.x < this.transform.position.x && this.transform.position.x - Player.transform.position.x > .05f)) {
			anim.SetBool ("Move", true);
			if (facingRight == true)
				transform.position = new Vector3 (transform.position.x + movespeed, transform.position.y);
			else
				transform.position = new Vector3 (transform.position.x - movespeed, transform.position.y);
			Vector3 relativePoint = transform.InverseTransformPoint (Player.transform.position);
			if (facingRight == true && relativePoint.x < 0.0) {
				Flip ();
			} else if (facingRight == false && relativePoint.x < 0.0) {
				Flip ();
			}
		} else {
			anim.SetBool ("Move", false);
		}
	}

	bool WalkOrJump(NodePath current, NodePath next)//false - walk, true - jump
	{
		bool stop = false;
		bool result = false;
		for (int i = 0; i < current.AdjNodes.Length&&!stop; i++) {
			if (current.AdjNodes [i]!=null&&current.AdjNodes [i].Equals (next)) {
				stop = true;
				result = current.actions [i];
			}
		}
		return result;
	}

	void Flip ()
	{
		// Switch the way the Player is labelled as facing.
		facingRight = !facingRight;

		// Multiply the Player's x local scale by -1.
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
		//kickBack();
		if(Hp<=0)
		{
			anim.SetBool("Dead",true);
			Destroy(this.gameObject,3);
			dead = true;
		}
	}

	private void ApplyDamage(object[] damages)
	{
		isHpVis(true);
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
			kickBack();
			Hp-=totalDamage;
			if(Hp < 0)
				Hp = 0;
			CalcHpBar();
		}

		this.GetComponent<SpriteRenderer>().color = Color.red;

		Invoke("ColorBack",.1f);
		if(Hp<=0&&!dead)
		{
			anim.SetBool("Dead",true);
			Invoke("OFF",3f);
			dead = true;
			//gameObject.active = false;
			if(chest!=null)
				chest.active = true;
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

	public virtual void kickBack()
	{
		hit = true;
		Invoke("hitOFF",.5f);
	}
	void hitOFF()
	{
		hit = false;
	}

	public void AttackOff()
	{
		anim.SetBool("Attack",false);
		anim.SetBool ("Strong", false);
		anim.SetBool ("Range", false);
		m = 3f;
		i = coolDown;
	}

	void slash()
	{
		float Distance = Vector2.Distance(this.transform.position,Player.transform.position);

		if(Distance <= attackRange)
		{
			bool attack = true;
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
				attack = false;
			}
			else if(facingRight == false && relativePoint.x < 0.0)
			{
				attack = false;
			}

			if(attack)
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

	public void playSlash()
	{
		source.volume = .6f;
		source.pitch = 1.2f;
		source.PlayOneShot(slashSound);
		Invoke ("SoundNormal", .5f);
		Invoke ("PitchNormal", .5f);
	}

	void PitchNormal()
	{
		source.pitch = 1f;
	}

	void SoundNormal()
	{
		source.volume = 1f;
	}

	public void isPlayerClose()
	{
		float PlayerDist = Vector2.Distance(this.transform.position,Player.transform.position);
		if(PlayerDist > attackRange+1f)
		{
			anim.SetBool("Attack",false);
			Atk = false;
			i = coolDown;
			m = 2f;
		}
		else
		{
			Face ();
			i = coolDown;
			m = 2f;
			anim.SetBool("Move",false);
			anim.SetBool("Attack",true);
		}

	}

	void Shoot()
	{
		playThrow ();
		Transform firePoint = this.transform.FindChild ("FirePoint");
		GameObject bul = Instantiate(Strong, firePoint.position,Strong.transform.rotation)as GameObject;
		if(facingRight == false)
		{
			Vector3 theScale = bul.transform.localScale;
			theScale.x *= -1;
			bul.transform.localScale = theScale;
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(-25,0);
		}
		else
		{
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(25,0);
		}
		Destroy(bul,2f);
	}

	void AuraShoot()
	{
		playThrow ();
		Transform firePoint = this.transform.FindChild ("Aura");
		GameObject bul = Instantiate(aura, firePoint.position,firePoint.transform.rotation)as GameObject;
		if(facingRight == true)
		{
			Vector3 theScale = bul.transform.localScale;
			theScale.x *= -1;
			bul.transform.localScale = theScale;
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(20,0);
		}
		else
		{
			bul.GetComponent<Rigidbody2D>().velocity = new Vector2(-20,0);
		}
		Destroy(bul,3f);
	}

	public void playLargeSlash()
	{
		source.volume = .6f;
		source.pitch = .6f;
		source.PlayOneShot(slashSound);
		Invoke ("SoundNormal", .5f);
		Invoke ("PitchNormal", .5f);
	}

	public void playThrow()
	{
		source.volume = .6f;
		source.PlayOneShot(shoot,.6f);
		Invoke ("SoundNormal", .1f);
	}




}


