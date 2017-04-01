using UnityEngine;
using System.Collections;

public class DontTouch : MonoBehaviour 
{
	public float dam;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Enemy"))
		{
			col.SendMessage("ApplyDamage",dam,SendMessageOptions.DontRequireReceiver);
		}
	}
}
