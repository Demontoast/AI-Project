using UnityEngine;
using System.Collections;

public class PlatformerAI : MonoBehaviour {

	public float attackRange;
	public int damage;
	public float agroRange;
	public bool agro;
	public float playerDistance;
	GameObject player;
	AllPaths allPaths;

	public Paths curPlayerPath;
	public Paths curEnemyPath;
	public Paths NextNode;

	public bool facingRight;

	public float moveSpeed = 0.05f;

	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		allPaths = GameObject.FindGameObjectWithTag("Settings").GetComponent<AllPaths>();
		curEnemyPath = FindEnemyPath();
	}

	void Update()
	{
		playerDistance = Vector2.Distance(this.transform.position,player.transform.position);
		if(playerDistance < agroRange)
			agro = true;
		curPlayerPath = FindPlayerPath();
		if(agro && playerDistance > agroRange)
			Action();
		else 
			Attack();
		
	}

	void Attack()
	{
		face();
		////this.GetComponent<Animator>().SetBool("Move",false);
		//this.GetComponent<Animator>().SetBool("Attack",true);
	}
	
	void Action()
	{
		if(NextNode==null)
			FindNextNode();
		else if(NextNode.Equals(curEnemyPath.Left))
		{
			if(curEnemyPath.LeftType.Equals(Paths.type.Walk))
			{
				Walk ();
			}
			if(curEnemyPath.LeftType.Equals(Paths.type.Jump))
			{
				Jump ();
			}
		}
		else if(NextNode.Equals(curEnemyPath.Right))
		{
			if(curEnemyPath.RightType.Equals(Paths.type.Walk))
			{
				Walk ();
			}
			if(curEnemyPath.RightType.Equals(Paths.type.Jump))
			{
				Jump ();
			}
		}
	}

	void FindNextNode () 
	{
		if(attackRange > playerDistance)
		{
			//ATTACK
		}
		else
		{
			if(curPlayerPath.name == curEnemyPath.name)
			{
				if(curPlayerPath.index < curEnemyPath.index)
				{
					NextNode = curEnemyPath.Left;
					//Go Left 
				}
				else
				{
					NextNode = curEnemyPath.Right;
						//Go Right 
				}
			}

		}
	
	}

	void Walk()
	{
		float nextStop = Vector2.Distance(this.transform.position,NextNode.gameObject.transform.position);
		Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
		face ();
		if(nextStop > 0.5f)
		{
			if(facingRight == true)
				transform.position = new Vector3(transform.position.x + moveSpeed, transform.position.y);
			else
				transform.position = new Vector3(transform.position.x - moveSpeed, transform.position.y);
		}
		else
		{
			curEnemyPath = NextNode;
			NextNode = null;
		}
	}
	void Jump()
	{

	}

	Paths FindPlayerPath()
	{
		int index = 0;
		float lowestDist =  Vector2.Distance(player.transform.position,allPaths.world[0].gameObject.transform.position);
		for(int i = 0;i< allPaths.world.Length;i++)
		{
			if(lowestDist > Vector2.Distance(player.transform.position,allPaths.world[i].gameObject.transform.position))
			{
				lowestDist = Vector2.Distance(player.transform.position,allPaths.world[i].gameObject.transform.position);
				index = i;
			}
		}
		return allPaths.world[index];
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

	Paths FindEnemyPath()
	{
		int index = 0;
		float lowestDist =  Vector2.Distance(this.transform.position,allPaths.world[0].gameObject.transform.position);
		for(int i = 0;i< allPaths.world.Length;i++)
		{
			if(lowestDist > Vector2.Distance(this.transform.position,allPaths.world[i].gameObject.transform.position))
			{
				lowestDist = Vector2.Distance(this.transform.position,allPaths.world[i].gameObject.transform.position);
				index = i;
			}
		}
		return allPaths.world[index];
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
}
