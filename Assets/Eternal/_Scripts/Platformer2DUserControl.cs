using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;


[RequireComponent(typeof(PlatformerCharacter2D))]
public class Platformer2DUserControl : MonoBehaviour
{
	private PlatformerCharacter2D character;
	public GameObject curSprite;
	private bool jump;
	private bool atk;
	private bool oneOrTwo;
	private bool roll;
	private bool backStep;
	CharacterStats player;
	Equip equip;
	GameObject Canvas;
	bool use;
	bool blocking = false;
	bool dead = false;
	bool buffed = false;
	bool Talking = false;
	bool slow = false;
	bool fast = false;
	bool slowWJump = false;
	bool rageBuff = false;
	bool mageMelee = false;
	bool oneExtraHeart = false;
	bool threeExtraHeart = false;
	bool oneHeartDoubleDamage = false;
	bool oneHeartCurseDoubleDamage = false;
	bool halfDamageHalfDealt = false;
	bool lessManaCost = false;
	bool fire = false;
	bool magic = false;
	bool art = false;

		
	private void Awake ()
	{
		character = GetComponent<PlatformerCharacter2D> ();
		player = GetComponent<CharacterStats> ();
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
	}

	void Update ()
	{
		
		character.anim = curSprite.GetComponent<Animator>();
		if (player.getCurHealth() <= 0&&dead ==false) 
		{
			dead = true;
		}
			

		if (dead == false) {
			Cursor.visible = false;
			if (Input.GetKeyDown (KeyCode.N) && atk == false) {
				oneOrTwo = !oneOrTwo;
				curSprite.GetComponent<Animator> ().SetBool ("oneOrTwo", oneOrTwo);
				curSprite.GetComponent<Animator> ().SetBool ("Attack", true);
				atk = true;
				Invoke("AtkOff",player.getTime());
			}
			if (Input.GetKeyDown (KeyCode.M)&& atk == false) {
				
			}
			if ((Input.GetKeyUp (KeyCode.M)) && equip.mask.getSideHand () != null) {
				
			}
			if ((Input.GetKeyDown (KeyCode.E)) && atk == false && jump == false) {
				curSprite.GetComponent<Animator> ().SetBool ("Roll", true);
				Invoke ("RollOff", 0.5f);
				roll = true;
			}
			if (Input.GetKeyDown (KeyCode.X)&&character.grounded &&!atk&&!roll&&!use&&equip.curMask!=null) {
				
			}
			if ((Input.GetKeyDown (KeyCode.F)) && use == false && atk == false && roll == false) {
				
			}
			if (!jump && !atk && !use && !blocking)
				jump = CrossPlatformInputManager.GetButtonDown ("Jump");

		} else {
			Cursor.visible = true;
		}
	}


	private void FixedUpdate ()
	{
		bool crouch = Input.GetKey (KeyCode.LeftControl);
		float h = CrossPlatformInputManager.GetAxis ("Horizontal");
		bool re = false;
		if (atk == true && character.grounded||blocking||use)
			h = 0;
		if (roll == true && character.grounded) {
			h *= 3;
		}
		if (backStep == true && character.grounded) {
			re = true;
			if (character.facingRight)
				h = -2;
			else
				h = 2;
		}
		if (slow) {
			jump = false;
			h /= 2;
		}
		if (fast) {
			h *= 1.7f;
		}
		if (slowWJump) {
			h /= 2;
		}
		character.Move (h, crouch, jump, re);
		jump = false;
	}
	void RollOff ()
	{
		curSprite.GetComponent<Animator> ().SetBool ("Roll", false);
		roll = false;
	}
	void BackOff ()
	{
		StrongOff ();
		backStep = false;
	}
	void AtkOff ()
	{
		atk = false;
		curSprite.GetComponent<Animator> ().SetBool ("Attack", false);
		curSprite.GetComponent<Animator> ().SetBool ("Melee", false);
	}
	void StrongOff ()
	{
		curSprite.GetComponent<Animator> ().SetBool ("Strong", false);
	}
	void ShootOff ()
	{
		curSprite.GetComponent<Animator> ().SetBool ("Shoot", false);
	}
	void ThrowOff ()
	{
		curSprite.GetComponent<Animator> ().SetBool ("Throw", false);
	}
	void WalkOn ()
	{
		atk = false;
	}
	void SAtkOff ()
	{
		curSprite.GetComponent<Animator> ().SetBool ("StrongAttack", false);
	}

	public bool isDead()
	{
		return dead;
	}
	public void hasDied()
	{
		dead = true;
	}

	public bool isBlocking()
	{
		return blocking;
	}




}