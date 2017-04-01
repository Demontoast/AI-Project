using UnityEngine;
using System.Collections;

public class DropLadder : Interactable {

	bool On = false;
	AudioSource source;
	AudioClip sound;

	void Start () 
	{
		text = "Kick Ladder Down 'S'";
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
		sound = Resources.Load ("Lever")as AudioClip;
		source = GetComponent<AudioSource> ();
		this.transform.FindChild ("Ladder").gameObject.GetComponent<Ladder> ().TurnOff ();
	}
	

	public override void Interact()
	{
		Drop ();
		shortcut = true;
	}

	public override void saveOpen(){
		Drop ();
		shortcut = true;
	}

	public void Drop()
	{
		this.GetComponent<Animator> ().SetBool ("Down", true);
		this.transform.FindChild ("Ladder").gameObject.GetComponent<Ladder> ().TurnOn ();
		this.GetComponent<DropLadder> ().enabled = false;
		if(itemDesc!=null)
			itemDesc.active = false;
		if(pickUp!=null)
			Destroy(pickUp);
		On = true;
	}

	public bool isOn()
	{
		return On;
	}

	public void playSound()
	{
		source.pitch = .8f;
		source.PlayOneShot (sound);
	}
		
}
