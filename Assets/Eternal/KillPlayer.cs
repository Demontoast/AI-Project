using UnityEngine;
using System.Collections;

public class KillPlayer : MonoBehaviour {

	[SerializeField] Transform gravePos;

	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals ("Player")) {
			col.GetComponent<CharacterStats> ().Kill ();
		}
	}
}
