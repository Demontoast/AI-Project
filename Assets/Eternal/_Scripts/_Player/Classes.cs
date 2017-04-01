using UnityEngine;
using System.Collections;

public class Classes : ScriptableObject 
{
	private string className;
	private string classDesc;
	private WEAPONS mainHand;
	private WEAPONS sideArm;
	private int buyPrice;
	private int reqShards;
	private int nextLvShards;
	private string[] abilities;

	public Classes(string ClassName, string ClassDesc,
	               string mainName, int mainPhyDam, int mainFireDam, int mainMagDam,int mainArtDam, bool mainOffHand, bool mainTwoHand, 
	               float mainManaOrStaminaChange, float mainDistance,WEAPONS.weaponType mainWeaponType, WEAPONS.weaponClass mainWpClass,
	               bool mainCanBuff,int mainLevel,float mainTime,float mainStrongTime,string sideName, int sidePhyDam,
	               int sideFireDam, int sideMagDam, int sideArtDam, bool sideOffHand, bool sideTwoHand, float sideManaOrStaminaChange,
		float sideDistance,WEAPONS.weaponType sideWeaponType, WEAPONS.weaponClass sideWpClass, bool sideCanBuff,int sideLevel,float sideTime,float sideStrongTime,string[] Abi)
	{
		className = ClassName;
		classDesc = ClassDesc;
		mainHand = new WEAPONS(mainName,mainPhyDam,0,mainFireDam,0,mainMagDam,0,mainArtDam,0,mainOffHand,mainTwoHand,mainManaOrStaminaChange,mainDistance
		                       ,mainWeaponType,mainWpClass,mainCanBuff,mainLevel,mainTime,mainStrongTime);
		sideArm = new WEAPONS(sideName,sidePhyDam,0,sideFireDam,0,sideMagDam,0,sideArtDam,0,sideOffHand,sideTwoHand,sideManaOrStaminaChange,sideDistance
		                       ,sideWeaponType,sideWpClass,sideCanBuff,sideLevel,sideTime,sideStrongTime);
		abilities = Abi;
	}

	public Classes(string ClassName, string ClassDesc, WEAPONS main, WEAPONS side,int BuyPrice, int ReqShards,int NextLvShards,string[] Abi)
	{
		className = ClassName;
		classDesc = ClassDesc;
		mainHand = main;
		sideArm = side;
		buyPrice = BuyPrice;
		reqShards = ReqShards;
		nextLvShards = NextLvShards;
		abilities = Abi;
	}
	public Classes(){}

	public string getName()
	{
		return className;
	}
	public string getDesc()
	{
		return classDesc;
	}
	public WEAPONS getMainHand()
	{
		return mainHand;
	}
	public WEAPONS getSideHand()
	{
		return sideArm;
	}
	public int getBuyPrice()
	{
		return buyPrice;
	}
	public void setName(string x)
	{
		className = x;
	}
	public void setDesc(string x)
	{
		classDesc=x;
	}
	public void setMainHand(WEAPONS x)
	{
		mainHand = x;
	}
	public void setSideHand(WEAPONS x)
	{
		sideArm=x;
	}
	public int NextLevelShards()
	{
		return nextLvShards;
	}
	public int ReqShards()
	{
		return reqShards;
	}
	public void LevelUp()
	{
		mainHand.level++;
		mainHand.curDamage+=mainHand.phyInc;
		mainHand.baseDamage+=mainHand.phyInc;
		mainHand.fireDamage+=mainHand.fireInc;
		mainHand.baseFireDamage+=mainHand.fireInc;
		mainHand.magicDamage+=mainHand.magicInc;
		mainHand.baseMagicDamage+=mainHand.magicInc;
		mainHand.artDamage+=mainHand.artInc;
		mainHand.baseArtDamage+=mainHand.artInc;
		if(sideArm!=null)
		{
			sideArm.level++;
			sideArm.curDamage+=sideArm.phyInc;
			sideArm.fireDamage+=sideArm.fireInc;
			sideArm.magicDamage+=sideArm.magicInc;
			sideArm.artDamage+=sideArm.artInc;

			sideArm.baseDamage+=sideArm.phyInc;
			sideArm.baseFireDamage+=sideArm.fireInc;
			sideArm.baseMagicDamage+=sideArm.magicInc;
			sideArm.baseArtDamage+=sideArm.artInc;
		}
		reqShards += nextLvShards;
	}
	public string[] getAbilities()
	{
		return abilities;
	}
}
