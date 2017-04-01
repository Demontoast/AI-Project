using UnityEngine;
using System.Collections;

public class MaskOff : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Invoke ("Off",1f);
	}
	
	// Update is called once per frame
	void Off () {
		this.GetComponent<Animator>().SetBool("Move",true);
		Invoke ("Des",2f);
	}
	void Des()
	{
		Destroy(this.gameObject);
	}
}
