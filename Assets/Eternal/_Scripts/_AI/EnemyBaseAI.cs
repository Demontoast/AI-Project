using UnityEngine;
using System.Collections;

public class EnemyBaseAI : MonoBehaviour {

	[SerializeField] protected string enemyName;
	[SerializeField] protected float Hp = 5;
	[SerializeField] protected float MaxHp = 5;
	protected GameObject Player;
	[SerializeField] protected float Damage = 10;
	[SerializeField] protected bool dead = false;
	[SerializeField] protected Animator anim;
	[SerializeField] protected bool agro = false;
	[SerializeField] protected bool Atk = false;
	[SerializeField] protected int souls;
	[SerializeField] protected int essence;
	[SerializeField] protected GameObject item;
	[SerializeField] protected int dropRate = 1;
	[SerializeField] protected GameObject chest;
	[SerializeField] protected bool facingRight = true;	
	[SerializeField] protected float movespeed = 1f;
	[SerializeField] protected bool hit = false;	

	[SerializeField] protected phyWeakness phy;
	[SerializeField] protected statWeakness stat;
	protected AudioSource source;

	[SerializeField] protected bool poisonImmue;
	[SerializeField] protected float psnDamage = 5f;
	protected bool poisoned = false;
	protected float psn = 0;

	public enum phyWeakness
	{
		Edge, Blunt, Pierce
	};

	public enum statWeakness
	{
		Magic, Fire, Artificial, Physical
	};

	public virtual void CalcHpBar(){}
	public virtual void AggroON(){}
	
	protected void dropItem()
	{
		int RND = Random.Range (0, dropRate);
		if (RND == 0) {
			Instantiate (item, this.transform.position, item.transform.rotation);
		}

	}
	protected void alwasyDropItem()
	{
		chest.GetComponent<SpriteRenderer> ().enabled = true;
		chest.gameObject.active = true;
		chest.GetComponent<CHEST> ().enabled = true;
		//Instantiate(items[0],this.transform.position,items[0].transform.rotation);
	}
	public void setPlayer(GameObject player)
	{
		Player = player;
	}
	public virtual void isHpVis(bool on){}
	public string getName()
	{
		return enemyName;
	}
	public bool isAgro()
	{
		return agro;
	}
	public void setAgro(bool Agro)
	{
		agro = Agro;
		if(anim!=null)
			anim.SetBool ("Agro", Agro);
	}
	public bool isDead()
	{
		return dead;
	}
	public void setDead(bool Dead)
	{
		 dead = Dead;
	}
	public bool isAtk()
	{
		return agro;
	}
	public void setAtk(bool Attack)
	{
		Atk = Attack;
	}
	public float getHp()
	{
		return Hp;
	}
	public void setHp(float Health)
	{
		Hp=Health;
	}

	public float getMaxHp()
	{
		return MaxHp;
	}
	public void setMaxHp(float maxHealth)
	{
		MaxHp=maxHealth;
	}
	public Animator getAnim()
	{
		return anim;
	}

	public virtual void kill()
	{
		dead = true;
		anim.SetBool("Dead",true);
		if (chest != null) {
			chest.GetComponent<SpriteRenderer> ().enabled = true;
			chest.GetComponent<CHEST> ().enabled = false;
		}
		Hp = 0;
		this.gameObject.active = false;

	}
		

	protected void OFF()
	{
		if (item != null) {
			dropItem ();
		}
		gameObject.active = false;
	}
	protected void KILL()
	{
		Destroy (this.gameObject);
	}

	protected void PoisonOff()
	{
		poisoned = false;
	}
}
