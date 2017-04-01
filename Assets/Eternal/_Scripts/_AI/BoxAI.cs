using UnityEngine;
using System.Collections;

public class BoxAI : MonoBehaviour {
	
	GameObject Player;
	bool move;
	void Start()
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}
	private void ApplyDamage(object[] damages)
	{
		if (move == false) {
			move = true;
			Vector3 relativePoint = transform.InverseTransformPoint (Player.transform.position);
			if (relativePoint.x > 0.0) {
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (-5, this.GetComponent<Rigidbody2D>().velocity.y, 0);
				Invoke ("SlowDown", 0.2f);
			} else if (relativePoint.x < 0.0) {
				GetComponent<Rigidbody2D> ().velocity = new Vector3 (5, this.GetComponent<Rigidbody2D>().velocity.y, 0);
				Invoke ("SlowDown", 0.2f);
			}
		}
	}

	void FixedUpdate()
	{
		if (move == false) {
			GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, this.GetComponent<Rigidbody2D>().velocity.y, 0);
		}
	}

	void SlowDown()
	{
		GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, this.GetComponent<Rigidbody2D>().velocity.y, 0);
		move = false;
	}


}
