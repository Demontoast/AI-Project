using UnityEngine;
using System.Collections;

public class DestructableObjects : MonoBehaviour {

	float Hp = 3;
	bool dead;
	GameObject blownUpItem;
	[SerializeField]string blownUpItemName;

	[SerializeField] bool dropItem = false;
	[SerializeField] string itemsName;
	[SerializeField] int type = 0; //0 - mask, 1-item, 2-passive, 3-key
	[SerializeField] bool onlyOne = false;

	void Start ()
	{
		blownUpItem = Resources.Load(blownUpItemName) as GameObject;
	}
	void BlowUp()
	{
		GameObject x = null;
		GameObject clone = Instantiate (blownUpItem, this.transform.position, this.transform.rotation) as GameObject;
		if (dropItem) {
			if (!onlyOne||onlyOne&&!checkForOne()) 
				x =  Instantiate ( Resources.Load (itemsName+"_I") as GameObject, this.transform.position, this.transform.rotation) as GameObject;
		}
		Destroy (clone, 3f);
		Destroy (this.gameObject);
	}

	private void ApplyDamage(object[] damages)
	{
		Hp -= 1;
		GetComponent<Animator> ().SetBool ("Hit", true);
		Invoke ("HitOff", .1f);
		if (Hp <= 0&&!dead) {
			dead = true;
			BlowUp ();
		}
	}
	void HitOff()
	{
		GetComponent<Animator> ().SetBool ("Hit", false);
	}
	bool checkForOne()
	{
		Equip equip = GameObject.FindGameObjectWithTag ("Player").GetComponent<Equip> ();
		bool stop = false;
		bool results = false;
		switch (type) {
		case 0:
			for (int i = 0; i < equip.Mask.size () && !stop; i++) {
				if (equip.Mask.get (i).getName().Equals (itemsName)) {
					stop = true;
					results = true;
				}
			}
			break;
		case 1:
			for (int i = 0; i < equip.inventory.size () && !stop; i++) {
				if (equip.inventory.get (i).itemName.Equals (itemsName)) {
					stop = true;
					results = true;
				}
			}
			break;
		case 2:
			for (int i = 0; i < equip.passiveInventory.size () && !stop; i++) {
				if (equip.passiveInventory.get (i).itemName.Equals (itemsName)) {
					stop = true;
					results = true;
				}
			}
			break;
		case 3:
			for (int i = 0; i < equip.keys.size () && !stop; i++) {
				if (equip.keys.get (i).itemName.Equals (itemsName)) {
					stop = true;
					results = true;
				}
			}
			break;

		}
		return results;
	}

}
