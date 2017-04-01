using UnityEngine;
using System.Collections;

public class MessangerSwordAi : MonoBehaviour {

	GameObject Player;
	bool stop = true;
	bool fire;
	bool throwStart;

	AudioClip slashSound;
	AudioSource source;

	void Start () {
		Player = GameObject.FindGameObjectWithTag ("Player");
		Invoke ("StopSword", .6f);

		source = GetComponent<AudioSource>();
		slashSound = Resources.Load ("Swoosh") as AudioClip;
	}
	
	void FixedUpdate () 
	{
		if (!throwStart) {
			this.GetComponent<Rigidbody2D> ().AddForce (transform.right * 30);
		}
		if (!stop&&!fire) {
			float Distance = Vector3.Distance (Player.transform.position, this.transform.position);
			Vector3 relativePoint = transform.InverseTransformPoint (Player.transform.position);
			Vector3 vectorToTarget = Player.transform.position - this.transform.position;
			float angle = Mathf.Atan2 (vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
			Quaternion q = Quaternion.AngleAxis (angle, Vector3.forward);
			this.transform.rotation = Quaternion.Slerp (this.transform.rotation, q, Time.deltaTime * 5);
		}
		else if (fire == true) {
			this.GetComponent<Rigidbody2D> ().AddForce (transform.right * 30);
			stop = true;
			Invoke ("StopSword", 1f);
		}

	}

	void StopSword()
	{
		throwStart = true;
		fire = false;
		this.GetComponent<Rigidbody2D> ().velocity = new Vector3 (0, 0, 0);
		Invoke ("RotateSword", 3f);
	}

	void FireSword()
	{
		fire = true;
		//source.PlayOneShot (slashSound);
	}

	void RotateSword()
	{
		stop = false;
		fire = false;
		Invoke ("FireSword", 1f);
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if (col.tag.Equals ("Ground")) {
			StopSword ();
		}
		if(col.tag.Equals("Player")&&fire)
		{
			object[] tempSortage = new object[6];
			tempSortage[0] = 1f;
			tempSortage[1] = 0f;
			tempSortage[2] = 0f;
			tempSortage[3] = 0f;
			tempSortage[4] = 3;

			if(col.tag.Equals("Player"))
			{
				if(col.gameObject.GetComponent<Platformer2DUserControl>().isBlocking())
				{
					Vector3 RP = col.gameObject.transform.InverseTransformPoint(this.transform.position);
					if(RP.x > 0.0)
					{
						//Player.SendMessage("ApplyDamage",0,SendMessageOptions.DontRequireReceiver);
						col.gameObject.SendMessage("StamDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
					else
					{
						col.gameObject.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
					}
				}
				else
					col.gameObject.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
			}
		}
	}
}
