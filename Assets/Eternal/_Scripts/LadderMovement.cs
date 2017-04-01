using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class LadderMovement : MonoBehaviour 
{
	bool Up = false;
	bool Down = false;

	void FixedUpdate () 
	{
		float h = CrossPlatformInputManager.GetAxis ("Vertical");

		if(h>0.5)//Input.GetKey(KeyCode.W))
		{
			Up = true;
			this.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<Animator>().SetBool("LadderMove",true);
			this.transform.position += new Vector3(0,.07f,0);
		}
		if(h>-0.5f&&h<0.5f)//Input.GetKeyUp(KeyCode.W))
		{
			Up = false;
			Down = false; //Added
		}
		if(h<-0.5)//Input.GetKey(KeyCode.S))
		{
			Down = true;
			this.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<Animator>().SetBool("LadderMove",true);
			this.transform.position -= new Vector3(0,.07f,0);
		}
		//if(Input.GetKeyUp(KeyCode.S))
		//{
		//	Down = false;
		//}
		if(Down == false&&Up == false)
		{
			this.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<Animator>().SetBool("LadderMove",false);
		}
		if(CrossPlatformInputManager.GetButtonDown ("Jump"))//Input.GetKeyDown(KeyCode.Space))
		{
			this.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<Animator>().SetBool("LadderMove",false);
			this.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<Animator>().SetBool("LadderOn",false);
			this.GetComponent<Rigidbody2D>().gravityScale = 1;
			this.GetComponent<Platformer2DUserControl>().enabled = true;
			this.GetComponent<LadderMovement>().enabled = false;
			Down = false;
			Up = false;
		}
	}
}
