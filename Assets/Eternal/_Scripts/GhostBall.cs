using UnityEngine;
using System.Collections;

public class GhostBall : MonoBehaviour {


	GameObject smallGB;
	void Start () 
	{
		smallGB = Resources.Load("HomingGhostBall")as GameObject;
	}

	void Explode()
	{
		Transform x = this.transform.FindChild("Exit x");
		Transform y = this.transform.FindChild("Exit y");
		Transform z = this.transform.FindChild("Exit z");

		GameObject clone_x = Instantiate(smallGB,x.transform.position,smallGB.transform.rotation)as GameObject;
		GameObject clone_y = Instantiate(smallGB,y.transform.position,smallGB.transform.rotation)as GameObject;
		GameObject clone_z = Instantiate(smallGB,z.transform.position,smallGB.transform.rotation)as GameObject;

		Destroy(clone_x,6f);
		Destroy(clone_y,6f);
		Destroy(clone_z,6f);
		Destroy(this.gameObject);
	}
}
