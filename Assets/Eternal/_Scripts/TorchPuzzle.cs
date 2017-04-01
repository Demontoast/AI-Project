using UnityEngine;
using System.Collections;

public class TorchPuzzle : MonoBehaviour {

	[SerializeField] Torch[] torches; 
	[SerializeField] GameObject Switch;

	void Start () {
	
	}
	

	void FixedUpdate () {
		check ();
	}

	void check()
	{
		bool stop = true;
		for (int i = 0; i < torches.Length; i++) {
			if (torches [i] != null && !torches [i].IsON ()) {
				stop = false;
			}
		}
		if (stop == true) {
			Switch.active = true;
			for (int i = 0; i < torches.Length; i++) {
				if (torches [i] != null) {
					torches [i].Remain();
					torches [i].enabled = false;
				}
			}
			this.GetComponent<TorchPuzzle> ().enabled = false;
		}
	}
}
