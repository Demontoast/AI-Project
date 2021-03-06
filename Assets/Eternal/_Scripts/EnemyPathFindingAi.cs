﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyPathFindingAi : EnemyBaseAI {

	ArrayList<GameObject> path;
	[SerializeField] bool follow;
	float atkZone = 1.5f;
	float walkZone = 3f;
	bool dash = false;
	bool block = false;

	[SerializeField] private LayerMask whatIsGround;
	[SerializeField] string target = "Player";
	private Transform groundCheck;
	private float groundedRadius = .2f; 
	[SerializeField] bool grounded;
	[SerializeField] private float jumpForce = 300f;
	float i = 0;
	float a = 0;
	float m = 0;

	public float coolDown;
	public GameObject HealthBar;
	public Color color;

	AudioClip slashSound;
	AudioClip buffSound;
	AudioClip dodgeSound;

	[SerializeField] GameObject[] allies;


	void Start()
	{
		Player = GameObject.FindGameObjectWithTag (target);
		color = this.GetComponent<SpriteRenderer>().color;
		HealthBar = transform.FindChild ("Health").gameObject;
		groundCheck = transform.Find("GroundCheck");
		grounded = true;
		anim = this.GetComponent<Animator> ();
		isHpVis(false);
		source = GetComponent<AudioSource>();
		slashSound = Resources.Load ("Swoosh") as AudioClip;
		buffSound = Resources.Load ("Buff") as AudioClip;
		dodgeSound = Resources.Load ("Dodge") as AudioClip;
		agro = true;
		StartPathFinding ();
	}

	void FixedUpdate()
	{
		Player = GameObject.FindGameObjectWithTag (target);
		grounded = Physics2D.OverlapCircle (groundCheck.position, groundedRadius, whatIsGround);
		anim.SetBool ("Ground", grounded);
		if (Player != null) {
			if (!dead) {
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
				if (!agro) {
					if (Distance < walkZone)
						agro = true;
				} else {
					if (!grounded || block)
						dash = false;
					

					if (!grounded) {
						movespeed = 0.1f;
						//GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
					} else
						movespeed = 0.05f;
				

					if (hit) {
						Vector3 relativePoint = transform.InverseTransformPoint (Player.transform.position);
						if (relativePoint.x < 0.0 && facingRight || relativePoint.x > 0.0 && !facingRight) {
							transform.position = new Vector3 (transform.position.x + movespeed * 1.5f, transform.position.y);
						} else if (relativePoint.x > 0.0 && facingRight || relativePoint.x < 0.0 && !facingRight) {
							transform.position = new Vector3 (transform.position.x - movespeed * 1.5f, transform.position.y);

						}
					}
					else if (Distance <= atkZone && a <= 0) {
						if(grounded)
							GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
						int RND = Random.Range (0, 5);
						if (grounded && (RND == 2 || RND == 3)) {
							block = true;
							anim.SetBool ("Block", true);
						} else {
							anim.SetBool ("Attack", true);
							if (dash || !grounded)
								Invoke ("AttackOff", .5f);
						}
						Face ();
						Atk = true;
						dash = false;
						a = coolDown;
						m = 2f;
						anim.SetBool ("Move", false);
						anim.SetBool ("Roll", false);
						follow = false;
					} else if (CheckForAllies ()) {
//				if (Distance > atkZone && Distance < walkZone && m <= 0&&!hit) {
//					WalkTowards (Distance);
//					follow = false;
//					} 
						if (i <= 0 && m <= 0 && !hit) { //Distance > walkZone && 
							StartPathFinding ();
							i = 5;
							follow = true;
						} else if (path.size () > 0 && follow && m <= 0 && !hit) {
							Move ();
						} else {
							anim.SetBool ("Move", false);
							if (Distance > walkZone && m <= 0) {
								StartPathFinding ();
								i = 5;
								follow = true;
							}
						}
					}
				}
			}
		}

	}

	bool CheckForAllies()
	{
		bool results = false;
//		if (grounded) {
//			for (int i = 0; i < allies.Length && !results; i++) {
//				float dist = Vector3.Distance (allies [i].transform.position, this.transform.position);
//				if (dist < .2f) {
//					results = true;
//					//movespeed = .07f;
//
//				}
//			}
//		}
	return !results;
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
		Debug.Log ("Start: " + nodes [indexE].gameObject.name + " Final: " + nodes [indexP].gameObject.name);
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
		Debug.Log ("PATH");
		TotalPath.add (current);
		while (cameFrom.ContainsKey (current)) {
			GameObject x = null;
			if(cameFrom.TryGetValue(current, out x))
			{
				current = cameFrom [current];
				TotalPath.addFirst (current);
				Debug.Log (current.name);
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
				bool jump = WalkOrJump (currentNode.GetComponent<NodePath> (), path.get (1).GetComponent<NodePath> ());
				//Debug.Log ("Jump");
				path.remove (0);
				currentNode = path.get (0);
				//Debug.Log ("NEXT");
				//Debug.Log ("Next: "+currentNode);
				if (jump) {
					GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
					GetComponent<Rigidbody2D> ().AddForce (new Vector2 (0f, jumpForce));
				}
			} else
				path.remove (0);

		} else if (TargetDist >= .45 && (currentNode.transform.position.x > groundCheck.transform.position.x && currentNode.transform.position.x - groundCheck.transform.position.x > .05f || currentNode.transform.position.x < groundCheck.transform.position.x && groundCheck.transform.position.x - currentNode.transform.position.x > .05f)) {
			if (!dash && !Atk && !block && grounded && TargetDist > atkZone + 1 && m <= 0) {
				int RND = Random.Range (0, 5);
				if (RND == 2)
					dash = true;
			}
			if (dash && m <= 0) {
				anim.SetBool ("Roll", true);
				if (facingRight == true)
					transform.position = new Vector3 (transform.position.x + 0.1f, transform.position.y);
				else
					transform.position = new Vector3 (transform.position.x - 0.1f, transform.position.y);
				Vector3 relativePoint = transform.InverseTransformPoint (Player.transform.position);
				if (facingRight == true && relativePoint.x < 0.0) {
					Flip ();
				} else if (facingRight == false && relativePoint.x < 0.0) {
					Flip ();
				}
				Invoke ("DodgeOff", 0.5f);
			} else {
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

	void AttackOff()
	{
		anim.SetBool("Attack",false);
		anim.SetBool("Block",false);
		block = false;
		Atk = false;
	}
	void DodgeOff()
	{
		anim.SetBool("Roll",false);
		dash = false;
	}

	public void isPlayerClose()
	{
		float PlayerDist = Vector2.Distance(this.transform.position,Player.transform.position);
		if(PlayerDist > atkZone+1.5f)
		{
			anim.SetBool("Attack",false);
			anim.SetBool("Block",false);
			block = false;
			Atk = false;
			a = coolDown;
		}
		else
		{
			Face ();
			a = coolDown;
			m = 2f;
			anim.SetBool("Move",false);
			anim.SetBool("Roll",false);
			int RND = Random.Range (1, 3);
			if(RND == 2)
				anim.SetBool("Attack",true);
			else
				anim.SetBool("Block",true);
			//Invoke("AtkOFF",0.5f);
		}

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
			kickBack();
			Hp-=damage;
			if(Hp < 0)
				Hp = 0;
			//HealthBar.transform.localScale = new Vector3(Hp/MaxHp,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
		}

		this.GetComponent<SpriteRenderer>().color = Color.red;

		Invoke("ColorBack",.1f);
		if(Hp<=0)
		{
			anim.SetBool("Dead",true);
			Invoke("KILL",3f);
			dead = true;
		}
	}

	public virtual void kickBack()
	{
		hit = true;
		Invoke("hitOFF",.25f);
		agro = true;
		Atk = false;
	}
	void hitOFF()
	{
		hit = false;
	}

	public override void CalcHpBar()
	{
		HealthBar.transform.localScale = new Vector3(Hp/MaxHp,HealthBar.transform.localScale.y,HealthBar.transform.localScale.z);
	}


	void slash()
	{
		float Distance = Vector2.Distance(this.transform.position,Player.transform.position);

		if(Distance <= atkZone)
		{
			bool atk = true;
			i = coolDown;

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
				Player.SendMessage("ApplyDamage",Damage,SendMessageOptions.DontRequireReceiver);
			}
		}
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
		
	void Blocking()
	{
		block = true;
	}
	void NotBlocking()
	{
		block = false;
	}

	public void playSlash()
	{
		source.pitch = 1.2f;
		source.PlayOneShot(slashSound);
		Invoke ("PitchNormal", .6f);
	}

	public void playBuff()
	{
		source.volume = .4f;
		source.pitch = .8f;
		source.PlayOneShot(buffSound);
		Invoke ("PitchNormal", .5f);
		Invoke ("SoundNormal", .5f);
	}

	public void playDodge()
	{
		source.pitch = 1.7f;
		source.volume = .6f;
		source.PlayOneShot(dodgeSound,.6f);
		Invoke ("PitchNormal", .1f);
		Invoke ("SoundNormal", .1f);
	}

	void SoundNormal()
	{
		source.volume = .6f;
	}

	void PitchNormal()
	{
		source.pitch = 1f;
	}


}


