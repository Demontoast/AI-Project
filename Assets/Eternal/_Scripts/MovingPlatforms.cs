using UnityEngine;
using System.Collections;

public class MovingPlatforms : MonoBehaviour 
{

	public bool vertical;
	public bool horizontal;
	public bool moveWhenOn;
	public bool hasTriggerMove;
	public bool stopAtEnds;
	bool triggerOn;
	bool move;

	public GameObject P1; //Left or Up
	public GameObject P2; //Right or Down
	GameObject curP;
	bool P; //if false go Left/Up
	[SerializeField] bool startDown;

	void Start() 
	{
		if (!startDown) {
			curP = P1;
			P = false;
		} else {
			curP = P2;
			P = true;
		}
		if (!hasTriggerMove && !moveWhenOn) {
			triggerOn = true;
			move = true;
		}
	}

	void FixedUpdate()
	{
		if (move) {
			float Distance = Vector2.Distance (this.transform.position, curP.transform.position);
			if (Distance <= .5) {
				if (stopAtEnds) {
					move = false;
				}
				P = !P;
				if (!P)
					curP = P1;
				else
					curP = P2;
			}
			if (vertical) {
				if (P)
					transform.position = new Vector3 (transform.position.x, transform.position.y - 0.05f);
				else
					transform.position = new Vector3 (transform.position.x, transform.position.y + 0.05f);
			} else {
				if (P)
					transform.position = new Vector3 (transform.position.x + 0.05f, transform.position.y);
				else
					transform.position = new Vector3 (transform.position.x - 0.05f, transform.position.y);
			}
		}
	}

	void OnTriggerEnter2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			col.transform.parent = this.transform;
			if (moveWhenOn&&!hasTriggerMove||moveWhenOn&&hasTriggerMove&&triggerOn)
					move=true;
		}
	}
	void OnTriggerExit2D(Collider2D col)
	{
		if(col.gameObject.tag.Equals("Player"))
		{
			col.transform.parent = null;
		}
	}

	public void MoveOn()
	{
		triggerOn = true;
	}

	public void MoveUp()
	{
		P = false;
		curP = P1;
		move = true;
	}

	public void MoveDown()
	{
		P = true;
		curP = P2;
		move = true;
	}

	public bool getP()
	{
		return P;
	}
}
