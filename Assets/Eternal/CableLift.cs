using UnityEngine;
using System.Collections;

public class CableLift : MonoBehaviour {

	public bool moveWhenOn;
	public bool hasTriggerMove;
	public bool stopAtEnds;
	bool triggerOn;
	bool move;
	bool down;
	[SerializeField] GameObject lockedSwitch;

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			transform.parent.GetComponent<Animator> ().SetBool ("Move", true);
			col.transform.parent = this.transform;
			if (moveWhenOn && !hasTriggerMove || moveWhenOn && hasTriggerMove && triggerOn) {
				if (!down) {
					down = true;
					transform.parent.GetComponent<Animator> ().SetBool ("Up", false);
				}
				else {
					down = false;
					transform.parent.GetComponent<Animator> ().SetBool ("Up", true);
				}
			}
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2DFollow> ().damping = .05f;
			lockedSwitch.GetComponent<VertLiftSwitch> ().unlockTheLift (); 
			lockedSwitch.GetComponent<VertLiftSwitch> ().ShortCutON ();
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			col.transform.parent = null;
			GameObject.FindGameObjectWithTag ("MainCamera").GetComponent<Camera2DFollow> ().damping = 0f;

		}
	}

	public bool isDown()
	{
		return down;
	}
	public void setDown(bool x)
	{
		down=x;
	}

}
