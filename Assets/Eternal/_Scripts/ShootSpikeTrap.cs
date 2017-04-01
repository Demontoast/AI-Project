using UnityEngine;
using System.Collections;

public class ShootSpikeTrap : MonoBehaviour {

	Transform pos1;
	Transform pos2;
	Transform pos3;
	GameObject Spike;
	int i = 0;
	bool shot = false;

	AudioClip spike;
	AudioSource source;

	void Start () {
		Spike = Resources.Load ("ShootSpikes") as GameObject;
		pos1 = transform.FindChild ("Pos1");
		pos2 = transform.FindChild ("Pos2");
		pos3 = transform.FindChild ("Pos3");
		source = GetComponent<AudioSource> ();
		spike = Resources.Load ("SpikeSlash") as AudioClip;
	}
	
	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals ("Player")&&!shot) {
			shot = true;
			ShootSpike ();
		}
	}

	void ShootSpike()
	{
		playSound ();
		GameObject clone1 = Instantiate (Spike, pos1.transform.position, Spike.transform.rotation)as GameObject;
		clone1.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -15);

		GameObject clone2 = Instantiate (Spike, pos2.transform.position, Spike.transform.rotation)as GameObject;
		clone2.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -15);

		GameObject clone3 = Instantiate (Spike, pos3.transform.position, Spike.transform.rotation)as GameObject;
		clone3.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, -15);

		if (i < 3)
			Invoke ("ShootSpike", 1f);
		i++;

	}

	void playSound () {
		source.PlayOneShot (spike);
	}
		
}
