using UnityEngine;
using System.Collections;

public class Interactables : MonoBehaviour {

	[SerializeField] bool health;
	[SerializeField] bool damage;
	bool hasItem;
	[SerializeField] GameObject player;
	GameObject enemy;
	Sprite noItem;
	Sprite yesItem;
	float t;
	[SerializeField] float maxTimer = 3;

	void Start()
	{
		hasItem = true;
		noItem = (Resources.Load ("noItem")as GameObject).GetComponent<SpriteRenderer>().sprite;
		yesItem = this.GetComponent<SpriteRenderer> ().sprite;
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");
	}

	void FixedUpdate () {
		player = GameObject.FindGameObjectWithTag ("Player");
		enemy = GameObject.FindGameObjectWithTag ("Enemy");

		if (t > 0)
			t -= Time.deltaTime;
		else {
			t = 0;
			hasItem = true;
			this.GetComponent<SpriteRenderer> ().sprite = yesItem;
		}

		if (hasItem) {
			if (player != null) {
				float dist = Vector2.Distance (player.transform.position, this.transform.position);
				if (dist < .5f) {
					if (health)
						player.GetComponent<CharacterStats> ().setCurHealth (player.GetComponent<CharacterStats> ().getMaxHealth ());
					//else if(damage)
						//Increase Damage
					hasItem = false;
					this.GetComponent<SpriteRenderer> ().sprite = noItem;
					t = maxTimer;
				}
			}
			if (enemy != null) {
				float dist = Vector2.Distance (enemy.transform.position, this.transform.position);
				if (dist < .5f) {
					if (health)
						enemy.GetComponent<EnemyBaseAI> ().setHp (enemy.GetComponent<EnemyBaseAI> ().getMaxHp ());
					//else if(damage)
						//Increase Damage
					hasItem = false;
					this.GetComponent<SpriteRenderer> ().sprite = noItem;
					t = maxTimer;
				}
			}
		}
	}
}
