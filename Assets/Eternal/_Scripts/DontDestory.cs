using UnityEngine;
using System.Collections;

public class DontDestory : MonoBehaviour {

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}
	void Update () 
	{
		DontDestroyOnLoad(this.gameObject);
	}
}
