using UnityEngine;
using System.Collections;

public class DropTrap : MonoBehaviour {

	GameObject player;
	[SerializeField] GameObject trap;
	[SerializeField] float trigDist = 2f;
	void Start () {
		player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update () 
	{
		float distance = Vector2.Distance(player.transform.position,this.transform.position);
		if(distance < trigDist)
		{
			trap.GetComponent<Animator>().SetBool("Drop",true);
			Invoke ("FallOff", .1f);
		}
	}
	void FallOff()
	{
		trap.GetComponent<Animator>().SetBool("Drop",false);
	}
}
