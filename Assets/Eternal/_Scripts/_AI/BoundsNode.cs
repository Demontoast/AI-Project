using UnityEngine;
using System.Collections;

public class BoundsNode : MonoBehaviour {

	public bool right;
	public bool left;

	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag.Equals("Enemy"))
		{
//			col.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
//			if(col.GetComponent<AdvSoldierAI>())
//			//	col.GetComponent<AdvSoldierAI>().ReturnStart(right,left);
//			else if(col.GetComponent<AdvKnightAI>())
//				col.GetComponent<AdvKnightAI>().ReturnStart(right,left);
		}
	}
	
}
