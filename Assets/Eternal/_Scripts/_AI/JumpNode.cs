using UnityEngine;
using System.Collections;

public class JumpNode : MonoBehaviour {

	public JumpNode nextNode;
	GameObject Player;
	public bool hasTo;
	public bool check;

	void Start () 
	{
		Player = GameObject.FindGameObjectWithTag("Player");
	}

	public void OnTriggerEnter2D(Collider2D col)
	{
		if(col.tag.Equals("Enemy")&&nextNode!=null)
		{
			if(Player.transform.position.y >= col.gameObject.transform.position.y||hasTo)
			{
				col.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
				if(check)
					col.GetComponent<AIPlatformer>().cJump = true;
				else
					col.GetComponent<AIPlatformer>().jump = true;
				col.GetComponent<AIPlatformer>().player = nextNode.gameObject;
				//Debug.Log(col.name);
			}
		}
	}

}
