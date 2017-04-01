using UnityEngine;
using System.Collections;

public class BD : MonoBehaviour {

	public float damage;
	public float fireDamage;
	public float magicDamage;
	public float artDamage;
	public float poisonDamage;
	public string tag;
	public bool dontDestroy;
	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag.Equals(tag))
		{
			object[] tempSortage = new object[6];
			tempSortage[0] = damage;
			tempSortage[1] = fireDamage;
			tempSortage[2] = magicDamage;
			tempSortage[3] = artDamage;
			tempSortage[4] = 3;
			tempSortage[5] = poisonDamage;

			if(tag.Equals("Player"))
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
			else
				col.gameObject.SendMessage("ApplyDamage",tempSortage,SendMessageOptions.DontRequireReceiver);
			if(!dontDestroy)
				Destroy(this.gameObject);
		}
		if (col.tag.Equals ("Ground")&&!dontDestroy) {
			Destroy(this.gameObject);
		}
	}
}
