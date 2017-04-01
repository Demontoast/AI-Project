using UnityEngine;
using System.Collections;

public class EnterNewLevel : MonoBehaviour {

	public string levelName;
	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			Application.LoadLevel(levelName);
		}
	}
}
