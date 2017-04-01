using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour {

	private GameObject player;
	private CharacterStats character;
	private Equip equip;
	private bool regen;

	[SerializeField]private Sprite fullHeart;
	[SerializeField]private Sprite thirdHeart;
	[SerializeField]private Sprite halfHeart;
	[SerializeField]private Sprite oneHeart;
	[SerializeField]private Sprite emptyHeart;

	[SerializeField]private GameObject[] hearts;
	[SerializeField]private Image manaBar;
	[SerializeField]private Sprite regenManaBar;
	[SerializeField]private Sprite normalManaBar;
	[SerializeField]private Image manaBackBar;
	[SerializeField]private Text MaskLv;
	[SerializeField]private Text MaskText;

	void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		character = player.GetComponent<CharacterStats>();
		equip = player.GetComponent<Equip>();
		int heartNumber = (int)character.getMaxHealth()/4;
		for(int i = 0;i<heartNumber;i++)
		{
			hearts[i].active = true;
		}
	}
	void Update()
	{
		fixHearts();
	//	fixMana();
	}

	void fixHearts()
	{
		int maxHeartNumber = (int)character.getMaxHealth()/4;
		int heartNumber = (int)(character.getCurHealth()/4);
		int fraction = (int)character.getCurHealth() % 4;
		if(fraction == 0)
			heartNumber -= 1;
		if(character.getCurHealth()>0)
		{
		switch(fraction)
		{
		case 0:
			hearts[heartNumber].GetComponent<Image>().sprite = fullHeart;
			break;
		case 1:
			hearts[heartNumber].GetComponent<Image>().sprite = oneHeart;
			break;
		case 2:
			hearts[heartNumber].GetComponent<Image>().sprite = halfHeart;
			break;
		case 3:
			hearts[heartNumber].GetComponent<Image>().sprite = thirdHeart;
			break;
		}
		}
		for(int i = 0;i<hearts.Length;i++)
		{
			if(i<maxHeartNumber)
				hearts[i].active = true;
			else
				hearts[i].active = false;
		}
		for(int i = 0;i<heartNumber;i++)
		{
			hearts[i].GetComponent<Image>().sprite = fullHeart;
		}
		for(int i = heartNumber+1;i<maxHeartNumber;i++)
		{
			hearts[i].GetComponent<Image>().sprite = emptyHeart;
		}
	}

//	void fixMana()
//	{
//		//float maxSize = (character.getMaxMana () / 4 * 20) + 6;
//
//		if (character.getManaRegen ()) {
//			manaBar.sprite = regenManaBar;
//		}
//		else
//			manaBar.sprite = normalManaBar;
//
//		float maxSize = 210f;
//		float mana = (character.getCurMana () / character.getMaxMana ()) * maxSize;
//		manaBar.rectTransform.sizeDelta = new Vector2 (mana, 22f);
//		manaBackBar.rectTransform.sizeDelta = new Vector2 (maxSize, 22f);
//		//MaskLv.text = "Lv. " + equip.mask.getMainHand ().level;
//		//MaskText.text = "E: " + character.getCurMana () + "/ " + character.getMaxMana ();
//	}
		
}
