using UnityEngine;
using System.Collections;

public class ShootingBall : MonoBehaviour {

	public bool facingRight = true;	
	public float atkZone = .2f;
	public float movespeed = 0.04f;
	float damage = 4f;
	GameObject Player;
	bool stopRotate = false;
	
	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");


	}
	
	void FixedUpdate () 
	{
		if(!stopRotate)
		{
			float Distance = Vector2.Distance(this.transform.position,Player.transform.position);		
			Vector3 vectorToTarget = Player.transform.position - transform.position;
			float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
			transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * 5);
			Invoke ("Shoot",.5f);
		}
		else
		{
			this.transform.Translate(Vector3.right * Time.deltaTime*10);
		}
	}
	
	void Shoot()
	{
		stopRotate = true;
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
}
