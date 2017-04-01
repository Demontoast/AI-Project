using UnityEngine;
using System.Collections;

public class miniPortal : Interactable {


	[SerializeField] GameObject Portal;
	[SerializeField] string enterText = "Enter Portal 'S'";
	float i = 0f;


	void Start () 
	{
		text = enterText;
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
	}
	

	public override void Interact()
	{
		if (i <= 0) {
			i = .5f;
			player.transform.position = Portal.transform.position;
			player.transform.position = new Vector3 (player.transform.position.x, player.transform.position.y, 0);
			Destroy (pickUp);

		}
	}

	public override void Loop()
	{
		if (i > 0) {
			i -= Time.deltaTime;
		}
	}
}
