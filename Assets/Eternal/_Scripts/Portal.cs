using UnityEngine;
using System.Collections;

using UnityEngine.UI;
public class Portal : Interactable {

	public string sceneName;
	public int roomNo;
	[SerializeField] bool locked;
	[SerializeField] string key;
	GameObject settings;
	[SerializeField] string enterText = "Enter Portal 'S'";
	void Start () 
	{
		text = enterText;
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
		settings = GameObject.FindGameObjectWithTag("Settings");
	}


	public override void Interact()
	{
		if (locked) {
			Equip equip = player.GetComponent<Equip> ();
			bool stop = false;
			for (int i = 0; i < equip.keys.size () && !stop; i++) {
				if (equip.keys.get (i).itemName.Equals (key)) {
					stop = true;
				}

			}
			if (stop == true) {
				locked = false;	
				useDoor ();
			} else {
				itemDesc.transform.FindChild("Text").GetComponent<Text>().text = "Need "+key;
				itemDesc.active = true;
			}
		} else {
			useDoor ();
		}
	}
	public override void WalkAway()
	{

	}
	void useDoor()
	{
		
		Application.LoadLevel (sceneName);
	}
	public override void Loop(){}
	public override void Click(){}
}
