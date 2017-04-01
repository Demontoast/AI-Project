using UnityEngine;
using System.Collections;

public class KillAllEnemies : MonoBehaviour {

	[SerializeField]GameObject[] enemies;
	[SerializeField]GameObject Barrier;
	bool stop = false;
	bool isUp = false;

	void Start () {
	
	}
	

	void FixedUpdate () 
	{
		if(!isUp)
			loop ();
	}

	void loop()
	{
		stop = true;
		for (int i = 0; i < enemies.Length; i++) {
			if (enemies [i] != null && !(enemies [i].GetComponent<EnemyBaseAI> ().isDead ())) {
				stop = false;
			}
		}
		if (stop == true) {
			Barrier.GetComponent<Animator> ().SetBool ("Down", true);
			isUp = true;
		}
	}

	public void reset()
	{
		isUp = false;
	}
}
