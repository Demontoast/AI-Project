using UnityEngine;
using System.Collections;
//using UnityEditor;

public class Actions : MonoBehaviour {

	GameObject throwing;
	GameObject bullet;
	GameObject fireBall;
	GameObject magicMissile;
	GameObject Lightning;
	GameObject knife;
	GameObject bomb;
	GameObject axe;
	GameObject noviceSlash;
	GameObject elementalSlash;
	GameObject arrow;
	GameObject fireArrow;
	GameObject magicArrow;
	GameObject artArrow;
	GameObject StrongArrow;
	GameObject noManaBall;

	AudioSource source;
	AudioClip slashSound;
	AudioClip throwSound;
	AudioClip shortSwingSound;
	AudioClip dodgeSound;
	AudioClip healSound;
	AudioClip buffSound;
	AudioClip gunShotSound;
	AudioClip bowShotSound;

	AudioClip magicSound;
	AudioClip fireSound;

	float bloodDamge = 0f;
	float poisonDamage = 0f;

	private void Awake ()
	{
		knife = Resources.Load("Throwing Knife 1") as GameObject;
		bullet = Resources.Load("Bullet") as GameObject;
		bomb = Resources.Load("FireBomb") as GameObject;
		fireBall = Resources.Load("FireBalll") as GameObject;
		Lightning = Resources.Load("Lightning") as GameObject;
		magicMissile = Resources.Load("MagicMissile") as GameObject;
		axe = Resources.Load("Axe") as GameObject;
		arrow = Resources.Load("Arrow") as GameObject;
		fireArrow = Resources.Load("FireArrow") as GameObject;
		magicArrow = Resources.Load("MagicArrow") as GameObject;
		artArrow = Resources.Load("ArtArrow") as GameObject;
		StrongArrow = Resources.Load("StrongArrow") as GameObject;
		noManaBall = Resources.Load("NoManaBall") as GameObject;
		source = GetComponent<AudioSource>();
		slashSound = Resources.Load ("Swoosh") as AudioClip;
		throwSound = Resources.Load ("ThrowSound") as AudioClip;
		shortSwingSound = Resources.Load ("ShortSwing") as AudioClip;
		dodgeSound = Resources.Load ("Dodge") as AudioClip;
		healSound =  Resources.Load ("Heal") as AudioClip;
		buffSound =  Resources.Load ("Buff") as AudioClip;
		gunShotSound  = Resources.Load ("GunShot") as AudioClip;
		bowShotSound  = Resources.Load ("BowShot") as AudioClip;
		magicSound  = Resources.Load ("MagicAttack2") as AudioClip;
		fireSound  = Resources.Load ("FireAttack") as AudioClip;
		noviceSlash = Resources.Load ("NoviceSlash") as GameObject;
		elementalSlash = Resources.Load ("ElementalSlash") as GameObject;
	}


	void Slash ()
	{

		float dam = this.transform.parent.parent.GetComponent<CharacterStats> ().getDamage ();
		float dist = this.transform.parent.parent.GetComponent<CharacterStats> ().getDistance ();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0;i<enemies.Length;i++)
		{
			float dis = Vector2.Distance(this.transform.position,enemies[i].transform.position);
			if(dis <= dist)
			{
				Vector3 relativePoint = transform.InverseTransformPoint(enemies[i].transform.position);
				if(relativePoint.x > 0.0)
				{
					enemies[i].SendMessage("ApplyDamage",dam,SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}
		

	void StrongSlash ()
	{
		float dam = this.transform.parent.parent.GetComponent<CharacterStats> ().getDamage ()*2;
		float dist = this.transform.parent.parent.GetComponent<CharacterStats> ().getDistance ();

		GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
		for(int i = 0;i<enemies.Length;i++)
		{
			float dis = Vector2.Distance(this.transform.position,enemies[i].transform.position);
			if(dis <= dist)
			{
				Vector3 relativePoint = transform.InverseTransformPoint(enemies[i].transform.position);
				if(relativePoint.x > 0.0)
				{
					enemies[i].SendMessage("ApplyDamage",dam,SendMessageOptions.DontRequireReceiver);
				}
			}
		}
	}

	void Throw ()
	{
		playThrow ();
		Transform firePoint = this.transform.parent.parent.FindChild ("ThrowPoint");
		GameObject bul = Instantiate (throwing, firePoint.position, throwing.transform.rotation)as GameObject;
		if (this.transform.parent.parent.GetComponent<PlatformerCharacter2D>().facingRight == false) {
			Vector3 theScale = bul.transform.localScale;
			theScale.x *= -1;
			bul.transform.localScale = theScale;
			bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-15, 0);
		} else {
			bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (15, 0);
		}
		Destroy (bul, 3f);
	}

//	void useItem()
//	{
//		playHeal ();
//		bool dontUse = false;
//		bool relic = false;
//		switch (this.transform.parent.parent.GetComponent<Equip> ().curItem.statBoost) {
//		case ITEMS.StatBoost.CurHp:
//			this.transform.parent.parent.GetComponent<CharacterStats> ().Heal (this.transform.parent.parent.GetComponent<Equip> ().curItem.amountToBoost);
//			relic = true;
//			break;
//		if (this.transform.parent.parent.GetComponent<Equip> ().curItem.statBoost != ITEMS.StatBoost.Cross&&this.transform.parent.parent.GetComponent<Equip> ().curItem.statBoost != ITEMS.StatBoost.EternalEssance&&!dontUse) {
//			this.transform.parent.parent.GetComponent<Equip> ().getCurItem ().curStack--;
//			if (this.transform.parent.parent.GetComponent<Equip> ().getCurItem ().curStack == 0 && relic == false) {
//				this.transform.parent.parent.GetComponent<Equip> ().deleteCurItem ();
//			} else if (this.transform.parent.parent.GetComponent<Equip> ().getCurItem ().curStack == 0) {
//				this.transform.parent.parent.GetComponent<Equip> ().getCurItem ().itemName = "Empty Old Relic";
//			}
//
//		}
//
//	}

	void NoviceSlash ()
	{
		Transform firePoint = this.transform.parent.parent.FindChild ("MagicPoint");
		GameObject bul = Instantiate (noviceSlash, firePoint.position, axe.transform.rotation)as GameObject;
		bul.GetComponent<BD>().damage = (this.transform.parent.parent.GetComponent<CharacterStats>().getDamage())*4f;
		if (this.transform.parent.parent.GetComponent<PlatformerCharacter2D>().facingRight == false) {
			Vector3 theScale = bul.transform.localScale;
			theScale.x *= -1;
			bul.transform.localScale = theScale;
			bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-10, 0);
		} else {
			bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (10, 0);
		}
		Destroy (bul, 2f);
	}

	public void playThud()
	{
		source.volume = .6f;
		source.pitch = .7f;
		source.PlayOneShot(slashSound,.6f);
		Invoke ("SoundNormal", .1f);
		Invoke ("PitchNormal", .1f);
	}
	public void playSlash()
	{
		source.volume = .6f;
		source.pitch = 1f;
		source.PlayOneShot(slashSound);
		Invoke ("SoundNormal", .5f);
		Invoke ("PitchNormal", .5f);
	}
	public void playLargeSlash()
	{
		source.volume = .6f;
		source.pitch = .6f;
		source.PlayOneShot(slashSound);
		Invoke ("SoundNormal", .5f);
		Invoke ("PitchNormal", .5f);
	}

	public void playThrow()
	{
		source.volume = .6f;
		source.PlayOneShot(throwSound,.6f);
		Invoke ("SoundNormal", .1f);
	}
	public void playGunShot()
	{
		source.volume = .4f;
		source.PlayOneShot(gunShotSound,.6f);
		Invoke ("SoundNormal", .5f);
	}
	public void playBowShot()
	{
		source.volume = .4f;
		source.PlayOneShot(bowShotSound,.6f);
		Invoke ("SoundNormal", .5f);
	}

	public void playBuff()
	{
		source.volume = .4f;
		source.pitch = .8f;
		source.PlayOneShot(buffSound,.6f);
		Invoke ("PitchNormal", .5f);
		Invoke ("SoundNormal", .5f);
	}

	public void playShortSwing()
	{
		source.volume = .6f;
		source.PlayOneShot(shortSwingSound,.6f);
		Invoke ("SoundNormal", .1f);
	}

	public void playHeal()
	{
		source.volume = .2f;
		source.PlayOneShot(healSound,.3f);
		Invoke ("SoundNormal", 1.2f);
	}

	public void playDodge()
	{
		source.pitch = 1.7f;
		source.volume = .6f;
		source.PlayOneShot(dodgeSound,.6f);
		Invoke ("PitchNormal", .1f);
		Invoke ("SoundNormal", .1f);
	}
	void PitchNormal()
	{
		source.pitch = 1f;
	}

	void SoundNormal()
	{
		source.volume = 1f;
	}

	void StopSound()
	{
		source.Stop ();
	}

	public void playMagic()
	{
		source.volume = .6f;
		source.pitch = 1f;
		source.PlayOneShot(magicSound);
		Invoke ("SoundNormal", 2f);
	}

	public void playFire()
	{
		source.volume = .6f;
		source.pitch = 1f;
		source.PlayOneShot(fireSound);
		Invoke ("SoundNormal", 2f);
	}


}

