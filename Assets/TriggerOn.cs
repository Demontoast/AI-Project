using UnityEngine;
using System.Collections;

public class TriggerOn : MonoBehaviour {

	[SerializeField] GameObject objOn;
	[SerializeField] GameObject objOff;
	bool On = false;
	void OnTriggerEnter2D(Collider2D col)
	{
		if (!On) {
			if (col.gameObject.tag.Equals ("Player")) {
				if (objOn != null) {
					objOn.SetActive (true);
				}
				else if (objOff != null) {
					objOff.SetActive (false);
				}
			}
			Invoke ("Onish", .2f);
		}

	}
	void Onish()
	{
		On = false;
	}
}
