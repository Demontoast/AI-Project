using UnityEngine;
using System.Collections;

public class Ladder : MonoBehaviour 
{
	public GameObject player;
	public bool right;
	bool notAble = false;
	bool Off = false;
	AudioSource source;
	AudioClip sound;


	void Start () 
	{
		player = GameObject.FindGameObjectWithTag("Player");
		sound = Resources.Load ("Lever")as AudioClip;
		source = GetComponent<AudioSource> ();
	}

	void Update () 
	{
	
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player")&&!notAble&&!Off)
		{
			player.GetComponent<LadderMovement>().enabled = true;
			player.GetComponent<Platformer2DUserControl>().enabled = false;
			player.GetComponent<Rigidbody2D>().gravityScale = 0;
			player.GetComponent<Rigidbody2D>().velocity = new Vector3(0,0,0);
			player.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<Animator>().SetBool("LadderOn",true);
			player.transform.position = new Vector3(this.transform.FindChild("spawn").position.x,player.transform.position.y,player.transform.position.z);
		//	Vector3 relativePoint = transform.InverseTransformPoint(player.transform.position);
			if(right == true && !player.GetComponent<PlatformerCharacter2D>().facingRight)
			{
				player.GetComponent<PlatformerCharacter2D>().Flip();
			}
			else if(right == false && player.GetComponent<PlatformerCharacter2D>().facingRight)
			{
				player.GetComponent<PlatformerCharacter2D>().Flip();
			}
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag.Equals ("Player") && !notAble&&!Off) {
			player.GetComponent<Platformer2DUserControl> ().curSprite.GetComponent<Animator> ().SetBool ("LadderMove", false);
			player.GetComponent<Platformer2DUserControl> ().curSprite.GetComponent<Animator> ().SetBool ("LadderOn", false);
			player.GetComponent<Rigidbody2D> ().gravityScale = 1;
			player.GetComponent<Platformer2DUserControl> ().enabled = true;
			player.GetComponent<LadderMovement> ().enabled = false;
			notAble = true;
			Invoke ("OFF", 0.2f);
		}
	}
	void OFF()
	{
		notAble = false;
	}

	public void TurnOff()
	{
		Off = true;
	}

	public void TurnOn()
	{
		Off = false;
	}

	public void playSound()
	{
		source.pitch = .8f;
		source.PlayOneShot (sound);
	}

}
