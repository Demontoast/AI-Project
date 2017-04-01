using UnityEngine;
using System.Collections;

public class SpikeNoise : MonoBehaviour {

	AudioClip spike;
	AudioSource source;
	void Start () {
		source = GetComponent<AudioSource> ();
		spike = Resources.Load ("SpikeSlash") as AudioClip;
	}
	

	void playSound () {
		source.PlayOneShot (spike);
	}
}
