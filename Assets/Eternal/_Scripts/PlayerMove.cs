using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour {

	public float Health = 5f;
	bool Right = false;
	bool Left = false;
	bool Atk = false;
	bool facingRight = true;
	//scroll bar data
	public Slider slider;
	//public GameObject[] bodyParts;

	void Start () 
	{
		
	}
	

	void Update () 
	{
		Move();
		Attack();
		Jump();
		if(Input.GetKeyDown(KeyCode.F))
			ApplyDam(1f);
		slider.value = Health;
	}

	private void flip()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
		//for(int i = 0;i<bodyParts.Length;i++)
		//{
		//	bodyParts[i].GetComponent<SpriteRenderer>().sortingOrder *= -1;
		//}
	}

	void Move()
	{
		if(Input.GetKey(KeyCode.D)&&Left==false)
		{
			Right = true;
			if(facingRight == false)
				flip ();
			if(Atk == false)
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(10,GetComponent<Rigidbody2D>().velocity.y);
			//this.rigidbody2D.AddForce(new Vector2(10,0));
			this.GetComponent<Animator>().SetBool("MOVE",true);
		}
		if(Input.GetKeyUp(KeyCode.D)&&Right==true)
		{
			Right = false;
			this.GetComponent<Rigidbody2D>().velocity =new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
			this.GetComponent<Animator>().SetBool("MOVE",false);	
		}

		if(Input.GetKey(KeyCode.A)&&Right==false)
		{
			Left = true;
			if(facingRight == true)
				flip ();
			if(Atk == false)
				this.GetComponent<Rigidbody2D>().velocity = new Vector2(-10,GetComponent<Rigidbody2D>().velocity.y);
			//this.rigidbody2D.AddForce(new Vector2(-10,0));
			this.GetComponent<Animator>().SetBool("MOVE",true);
		}
		if(Input.GetKeyUp(KeyCode.A)&&Left==true)
		{
			this.GetComponent<Rigidbody2D>().velocity =new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
			Left = false;
			this.GetComponent<Animator>().SetBool("MOVE",false);	
		}
	}
	void Attack()
	{
		if(Input.GetButtonDown("Fire1"))
		{
			Atk = true;
			this.GetComponent<Animator>().SetBool("ATTACK",true);
			this.GetComponent<Rigidbody2D>().velocity =new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
			Invoke("ATKOFF",0.5f);
		}
	}
	void ATKOFF()
	{
		Atk = false;
		this.GetComponent<Animator>().SetBool("ATTACK",false);
		if(Right)
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(10,GetComponent<Rigidbody2D>().velocity.y);
		else if(Left)
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(-10,GetComponent<Rigidbody2D>().velocity.y);
	}
	void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space))
		{
			this.GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,10);
		}
	}

	public void ApplyDam(float dam)
	{
		Health-=dam;
		if(Health <= 0)
		{
			Health = 0;
			slider.transform.FindChild("Fill Area").gameObject.active = false;
		}
	}
}
