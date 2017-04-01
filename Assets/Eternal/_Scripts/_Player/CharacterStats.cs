using UnityEngine;
using System.Collections;
public class CharacterStats: MonoBehaviour {

	// Health //
	[SerializeField]private float maxHealth = 12f;			
	[SerializeField]private float curHealth= 12f;
	[SerializeField]private float damage= 4f;
	[SerializeField]private float distance= 1.7f;
	[SerializeField]private float time= 0.45f;

	bool invincible = false;


	void Awake()
	{

	}

	public void ApplyDamage(float damage)
	{
		if(invincible == false)
		{
			this.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<SpriteRenderer>().color = Color.red;
			Invoke ("colorNorm",.1f);
			curHealth -= damage;
			invincible = true;
			Invoke("invincibleOFF",1f);
		}
	}

	private void colorNorm()
	{
		this.GetComponent<Platformer2DUserControl>().curSprite.GetComponent<SpriteRenderer>().color = Color.white;
	}

	void invincibleOFF()
	{
		invincible = false;
	}
	public bool isInvincible()
	{
		return invincible;
	}
	public void resetStats()
	{
		curHealth = maxHealth;
	}

		
	// Getters and Setters //

	// MaxHealth //
	public float getMaxHealth()
	{
		return maxHealth;
	}
	public void setMaxHealth(float hp)
	{
		maxHealth = hp;
	}
	// CurHealth //
	public float getCurHealth()
	{
		return curHealth;
	}
	public void setCurHealth(float hp)
	{
		curHealth = hp;
	}
	public void Heal(float hp)
	{
		if (curHealth > 0) {
			curHealth += hp;
			if (curHealth > maxHealth)
				curHealth = maxHealth;
		}
	}

	public void Kill()
	{
		ApplyDamage (maxHealth);
	}
	public float getDamage()
	{
		return damage;
	}
	public float getDistance()
	{
		return distance;
	}
	public float getTime()
	{
		return time;
	}

		
}