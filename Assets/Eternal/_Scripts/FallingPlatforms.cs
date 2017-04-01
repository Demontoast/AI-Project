using UnityEngine;
using System.Collections;

public class FallingPlatforms : MonoBehaviour {

	bool fallen = false;
	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player")&&!fallen)
		{
			Invoke("Fall",.7f);
		}
	}
	void Fall()
	{
		fallen = true;
		this.GetComponent<SpriteRenderer>().enabled = false;
		this.GetComponentInParent<Animator>().SetBool("Fall",true);
		this.GetComponent<BoxCollider2D>().enabled = false;
		Invoke ("Normal",5f);

	}
	void Normal()
	{
		fallen = false;
		this.GetComponent<SpriteRenderer>().enabled = true;
		this.GetComponentInParent<Animator>().SetBool("Fall",false);
		this.GetComponent<BoxCollider2D>().enabled = true;
	}
}
