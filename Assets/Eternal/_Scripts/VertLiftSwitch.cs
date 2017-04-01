using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class VertLiftSwitch : Interactable {


	[SerializeField] GameObject lift;
	[SerializeField] GameObject barrier;
	[SerializeField] bool isLift = false;
	[SerializeField] bool locked = false;
	[SerializeField] bool needKey = false;
	[SerializeField] string key;
	[SerializeField] bool up = false; //false = up/Left
	[SerializeField] GameObject pair;
	bool notFirst = false;
	Sprite liftOn;
	[SerializeField] string liftOnName = "LiftOn";
	Sprite liftOff;
	[SerializeField] string liftOffName = "LiftOff";
	[SerializeField] string hoverText =  "Activate Switch On 'S'";
	[SerializeField] bool gun;
	[SerializeField] bool cable;
	[SerializeField] bool once;

	AudioSource source;
	AudioClip sound;

	void Start () 
	{
		text = hoverText;
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
		player = GameObject.FindGameObjectWithTag("Player");
		liftOn =  (Resources.Load(liftOnName) as GameObject).GetComponent<SpriteRenderer>().sprite;
		liftOff =  (Resources.Load(liftOffName) as GameObject).GetComponent<SpriteRenderer>().sprite;

		sound = Resources.Load ("Lever")as AudioClip;
		source = GetComponent<AudioSource> ();
	}
	

	public override void Interact()
	{ 
		if (needKey) {
			if (locked) {
				bool stop = false;
				ArrayList<ITEMS> keysArray = player.GetComponent<Equip> ().keys;
				for (int i = 0; i < keysArray.size () && !stop; i++) {
					if (keysArray.get (i).itemName.Equals (key)) {
						stop = true;
						if (isLift)
							unlockLift ();
						else {
							
							playSound ();
							lift.GetComponent<Animator> ().SetBool ("Move", true);
							this.GetComponent<SpriteRenderer>().sprite = liftOn;
							Destroy(pickUp);
							turnOff = true;
							Invoke("TurnOn",2f);
							shortcut = true;
							if (once) {
								this.enabled = false;
							}
						}
					}
				}
				if (!stop) {
					itemDesc.active = true;
					itemDesc.transform.FindChild ("Text").GetComponent<Text> ().text = "Need " + key;
					Invoke ("itemOFF", 1);
				}
			} else {
				if (isLift) {
					unlockLift ();
					playSound ();
				}
				else {
					playSound ();
					lift.GetComponent<Animator> ().SetBool ("Move", true);
					this.GetComponent<SpriteRenderer>().sprite = liftOn;
					Destroy(pickUp);
					turnOff = true;
					Invoke("TurnOn",2f);
					shortcut = true;
				}
			}
		} else {
			if (locked) {
				itemDesc.active = true;
				itemDesc.transform.FindChild ("Text").GetComponent<Text> ().text = "Switch is Locked";
				Invoke ("itemOFF", 1);
			} else if (isLift) {
				if (barrier != null) {
					barrier.GetComponent<Animator> ().SetBool ("Move", true);	
				}
				playSound ();
				MoveLift ();
			
			}
			else {
				playSound ();
				if(this.GetComponent<SpriteRenderer>()!=null)
					this.GetComponent<SpriteRenderer>().sprite = liftOn;
				Destroy(pickUp);
				if (gun) {
					lift.GetComponent<Animator> ().enabled = true;
				} else if (cable) {
					if (lift.GetComponent<CableLift> ().isDown () && up) {
						lift.transform.parent.GetComponent<Animator> ().SetBool ("Up", true);
						lift.GetComponent<CableLift> ().setDown (false);
					} else if (!lift.GetComponent<CableLift> ().isDown () && !up) {
						lift.transform.parent.GetComponent<Animator> ().SetBool ("Up", false);
						lift.GetComponent<CableLift> ().setDown (true);
					}
					lift.transform.parent.GetComponent<Animator> ().SetBool ("Move", true);
					if (pair != null) {
						pair.GetComponent<VertLiftSwitch> ().unlockTheLift (); 
						pair.GetComponent<VertLiftSwitch> ().ShortCutON ();
					}
						
				} else {
					lift.transform.GetComponent<Animator> ().SetBool ("Move", true);
				}
//				if(up)
//					lift.GetComponent<Animator> ().SetBool ("Up", true);
//				else
//					lift.GetComponent<Animator> ().SetBool ("Up", false);
				turnOff = true;
				Invoke("TurnOn",2f);
				shortcut = true;
			}
		}
	}

	public override void saveOpen() {
		locked = false;

		lift.GetComponent<Animator> ().SetBool ("Move", true);
		if(this.GetComponent<SpriteRenderer>()!=null)
			this.GetComponent<SpriteRenderer>().sprite = liftOn;
		shortcut = true;
	}

	void itemOFF()
	{
		//itemDesc.active = false;
	}

	void unlockLift()
	{
		lift.GetComponent<MovingPlatforms>().enabled = true;
		Destroy(pickUp);
		this.GetComponent<SpriteRenderer>().sprite = liftOn;
		this.GetComponent<VertLiftSwitch>().enabled = false;
	}

	void MoveLift()
	{
		lift.GetComponent<MovingPlatforms>().MoveOn();

		if (pair != null) {
			pair.GetComponent<VertLiftSwitch> ().unlockTheLift (); 
			pair.GetComponent<VertLiftSwitch> ().ShortCutON ();
		if (up) {
			lift.GetComponent<MovingPlatforms> ().MoveUp ();
		} else {
			lift.GetComponent<MovingPlatforms> ().MoveDown ();
			}
		}
//		if (up&&!lift.GetComponent<MovingPlatforms>().getP()) {
//			lift.GetComponent<MovingPlatforms> ().MoveUp ();
//			up = false;
//		} else if(!up&&lift.GetComponent<MovingPlatforms>().getP()) {
//			lift.GetComponent<MovingPlatforms> ().MoveDown ();
//			up = true;
//		}
//		if (!notFirst) {
//			notFirst = true;
//			if (up)
//				lift.GetComponent<MovingPlatforms> ().MoveUp ();
//			else
//				lift.GetComponent<MovingPlatforms> ().MoveDown ();
//		}
		Destroy(pickUp);
		this.GetComponent<SpriteRenderer>().sprite = liftOn;
		turnOff = true;
		Invoke("TurnOn",2f);
	}

	void TurnOn()
	{
		if(this.GetComponent<SpriteRenderer>()!=null)
			this.GetComponent<SpriteRenderer>().sprite = liftOff;
		turnOff = false;
	}

	public void unlockTheLift()
	{
		locked = false;
	}

	public void playSound()
	{
		source.pitch = .8f;
		source.PlayOneShot (sound);
	}
}
