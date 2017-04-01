using UnityEngine; 
using UnityEngine.UI;
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System;

public class UltraSave : MonoBehaviour {

	string _FileLocation;
	string _FileName; 
	string _data; 
	public UltraData myData;

	public GameObject _Player; 
	CharacterStats Char;
	Equip equip;

	void Awake () 
	{
		_FileName="UltraData.xml"; 
		_FileLocation=Application.dataPath; 
		myData=new UltraData(); 

	} 

	void Start()
	{
		_Player = GameObject.FindGameObjectWithTag ("Player");
		if(_Player != null)
		{
			Char = _Player.GetComponent<CharacterStats>();
			equip = _Player.GetComponent<Equip>();
		}
	}


	public void SaveObject()
	{
		SaveBackUp();

		myData._iUser.On = true;
		Save(Path.Combine(_FileLocation, _FileName));

	}
	public void SaveBaseCharacter()
	{
		SaveBackUp();
		myData._iUser.On = true;
		Save(Path.Combine(_FileLocation, _FileName));

	}
	public void Delete()
	{
		myData._iUser.playerPosX = 0;
		myData._iUser.playerPosY = 0;
		myData._iUser.playerPosZ = 0;

		myData._iUser.tutorialItems = new bool[1];
		myData._iUser.tutorialChests = new bool[1];
		myData._iUser.tutorialIsBossDead = false;
		myData._iUser.tutorialIsNotFirstTime = false;
		myData._iUser.tutorialFightingBoss = false;
		myData._iUser.tutorialShortcut = false;
		myData._iUser.tutorialOn =false;
		myData._iUser.tutorialAreEnemiesDead = new bool[1];

		myData._iUser.asireItems = new bool[1];
		myData._iUser.asireChests = new bool[1];
		myData._iUser.asireIsBossDead = new bool[2];
		myData._iUser.asireIsNotFirstTime = false;
		myData._iUser.asireFightingBoss = new bool[2];
		myData._iUser.asireShortcuts  = new bool[1];
		myData._iUser.asireOn =false;
		myData._iUser.asireEnemyDead = new bool[1];
		myData._iUser.shrineRelicLevels = new int[1];
		myData._iUser.asireAreEnemiesDead = new bool[1];

		myData._iUser.sewerItems = new bool[1];
		myData._iUser.sewerOn = false;
		myData._iUser.sewerAreEnemiesDead = new bool[1];
		myData._iUser.sewerShortcut = false;
		myData._iUser.sewerShrineRelicLevel = 5;

		myData._iUser.AsireShrines = new bool[7];
		myData._iUser.NoxusShrines = new bool[7];
		myData._iUser.ThenoShrines = new bool[7];
		myData._iUser.LucarShrines = new bool[7];

		myData._iUser.shrineScene = "";
		myData._iUser.deathScene = "";
		myData._iUser.gravePosX =	0;
		myData._iUser.gravePosY = 0;
		myData._iUser.graveSouls = 0;
		myData._iUser.gravePosZ = 0;
		myData._iUser.curShrine = 0;
		myData._iUser.curScene = "";
		myData._iUser.lastScene = "";
		myData._iUser.standing = false;
		myData._iUser.On = false;

		myData._iUser.CurHp = 12;
		myData._iUser.CurMana = 12;
		myData._iUser.MaxHp = 12;
		myData._iUser.MaxMana = 12;
		myData._iUser.souls = 0;
		myData._iUser.shards = 0;
		myData._iUser.maskName = "NoMask";
		myData._iUser.heartShards = 0;

		myData._iUser.limit = 0;
		myData._iUser.limitGem = 0;
		myData._iUser.graveLimit = 0;
		myData._iUser.graveLimitGem = 0;

		myData._iUser.item1 = null;
		myData._iUser.item2 = null;
		myData._iUser.item3 = null;
		myData._iUser.item4 = null;

		myData._iUser.passive1 = null;
		myData._iUser.passive2 = null;
		myData._iUser.passive3 = null;
		myData._iUser.passive4 = null;

		myData._iUser.inventory = new ITEMS[10];
		myData._iUser.invSize = 0;

		myData._iUser.keys = new ITEMS[10];
		myData._iUser.keySize = 0;

		myData._iUser.spells = new SPELLS[10];
		myData._iUser.spellSize = 0;

		myData._iUser.spellSlot1 = null;
		myData._iUser.spellSlot2 = null;
		myData._iUser.spellSlot3 = null;
		myData._iUser.spellSlot4 = null;
		myData._iUser.spellSlot5 = null;

		myData._iUser.passiveInventory = new PASSIVEITEMS[10];
		myData._iUser.passiveInvSize = 0;

		myData._iUser.MainHands =  new WEAPONS[10];
		myData._iUser.SideArms =  new WEAPONS[10];
		myData._iUser.MaskNames =  new string[10];
		myData._iUser.MaskDescs =  new string[10];
		myData._iUser.MaskReqShards =  new int[10];
		myData._iUser.MaskNextShards =  new int[10];
		myData._iUser.maskSize =  0;
		myData._iUser.MaskAbilities = new string[1][];

		myData._iUser.skyColor = Color.white;
		myData._iUser.Background = "Sky";
		myData._iUser.Light = 1f;

		myData._iUser.breakRing = false;

		myData._iUser.curItem = null;
		Save(Path.Combine(_FileLocation, _FileName));
	}
	public void restingAtShrine()
	{
		SaveBackUp();
		myData._iUser.standing = false;
		myData._iUser.On = true;

		Save(Path.Combine(_FileLocation, _FileName));
	}
	public void setShrineNO(int no)
	{
		SaveBackUp();
		myData._iUser.curShrine = no;
		Save(Path.Combine(_FileLocation, _FileName));
	}
	public void removeGrave()
	{
		SaveBackUp();
		myData._iUser.graveSouls = 0;
		myData._iUser.graveLimit = 0;
		myData._iUser.graveLimitGem = 0;
		Save(Path.Combine(_FileLocation, _FileName));
	}

	public void DeadSave()
	{
		
	}



	public void LoadObject()
	{
		try
		{
			UltraData x = Load(Path.Combine(_FileLocation, _FileName));

		}
		catch(Exception e)
		{
			Debug.Log ("LoadObject failed "+e);
		}
	}

	public void SaveBackUp()
	{
		if(hasLoad())
		{
			UltraData x = Load(Path.Combine(_FileLocation, _FileName));
			myData._iUser.playerPosX = x._iUser.playerPosX;
			myData._iUser.playerPosY = x._iUser.playerPosY;
			myData._iUser.playerPosZ = x._iUser.playerPosZ;

			myData._iUser.tutorialItems = x._iUser.tutorialItems;
			myData._iUser.tutorialChests = x._iUser.tutorialChests;
			myData._iUser.tutorialIsBossDead = x._iUser.tutorialIsBossDead;
			myData._iUser.tutorialIsNotFirstTime = x._iUser.tutorialIsNotFirstTime;
			myData._iUser.tutorialFightingBoss = x._iUser.tutorialFightingBoss;
			myData._iUser.tutorialOn = x._iUser.tutorialOn;
			myData._iUser.tutorialShortcut = x._iUser.tutorialShortcut;

			myData._iUser.asireItems = x._iUser.asireItems;
			myData._iUser.asireChests = x._iUser.asireChests;
			myData._iUser.asireIsBossDead = x._iUser.asireIsBossDead;
			myData._iUser.asireIsNotFirstTime = x._iUser.asireIsNotFirstTime;
			myData._iUser.asireFightingBoss = x._iUser.asireFightingBoss;
			myData._iUser.asireOn = x._iUser.asireOn;
			myData._iUser.asireShortcuts = x._iUser.asireShortcuts;
			myData._iUser.asireEnemyDead = x._iUser.asireEnemyDead;
			myData._iUser.shrineRelicLevels = x._iUser.shrineRelicLevels;
			myData._iUser.asireAreEnemiesDead = x._iUser.asireAreEnemiesDead;

			myData._iUser.sewerItems = x._iUser.sewerItems;
			myData._iUser.sewerOn = x._iUser.sewerOn;
			myData._iUser.sewerShortcut = x._iUser.sewerShortcut;
			myData._iUser.sewerShrineRelicLevel = x._iUser.sewerShrineRelicLevel;
			myData._iUser.sewerAreEnemiesDead = x._iUser.sewerAreEnemiesDead;

			myData._iUser.AsireShrines = x._iUser.AsireShrines;
			myData._iUser.NoxusShrines = x._iUser.NoxusShrines;
			myData._iUser.ThenoShrines = x._iUser.ThenoShrines;
			myData._iUser.LucarShrines = x._iUser.LucarShrines;

			myData._iUser.shrineScene = x._iUser.shrineScene;
			myData._iUser.deathScene = x._iUser.deathScene;
			myData._iUser.gravePosX =	x._iUser.gravePosX;
			myData._iUser.gravePosY = x._iUser.gravePosY;
			myData._iUser.graveSouls = x._iUser.graveSouls;
			myData._iUser.gravePosZ = x._iUser.gravePosZ;
			myData._iUser.curShrine = x._iUser.curShrine;
			myData._iUser.curScene = x._iUser.curScene;
			myData._iUser.lastScene = x._iUser.lastScene;
			myData._iUser.standing = x._iUser.standing;
			myData._iUser.On = x._iUser.On;
			myData._iUser.breakRing = x._iUser.breakRing;

			myData._iUser.CurHp = x._iUser.CurHp;
			myData._iUser.CurMana = x._iUser.CurMana;
			myData._iUser.MaxHp = x._iUser.MaxHp;
			myData._iUser.MaxMana = x._iUser.MaxMana;
			myData._iUser.souls = x._iUser.souls;
			myData._iUser.shards = x._iUser.shards;
			myData._iUser.maskName = x._iUser.maskName;
			myData._iUser.heartShards = x._iUser.heartShards;

			myData._iUser.limit = x._iUser.limit;
			myData._iUser.limitGem = x._iUser.limitGem;
			myData._iUser.graveLimit = x._iUser.graveLimit;
			myData._iUser.graveLimitGem = x._iUser.graveLimitGem;

			myData._iUser.item1 = x._iUser.item1;
			myData._iUser.item2 = x._iUser.item2;
			myData._iUser.item3 = x._iUser.item3;
			myData._iUser.item4 = x._iUser.item4;

			myData._iUser.passive1 = x._iUser.passive1;
			myData._iUser.passive2 = x._iUser.passive2;
			myData._iUser.passive3 = x._iUser.passive3;
			myData._iUser.passive4 = x._iUser.passive4;

			myData._iUser.inventory = x._iUser.inventory;
			myData._iUser.invSize = x._iUser.invSize;

			myData._iUser.keys = x._iUser.keys;
			myData._iUser.keySize = x._iUser.keySize;

			myData._iUser.spells = x._iUser.spells;
			myData._iUser.spellSize = x._iUser.spellSize;

			myData._iUser.spellSlot1 = x._iUser.spellSlot1;
			myData._iUser.spellSlot2 = x._iUser.spellSlot2;
			myData._iUser.spellSlot3 = x._iUser.spellSlot3;
			myData._iUser.spellSlot4 = x._iUser.spellSlot4;
			myData._iUser.spellSlot5 = x._iUser.spellSlot5;

			myData._iUser.passiveInventory = x._iUser.passiveInventory;
			myData._iUser.passiveInvSize = x._iUser.passiveInvSize;

			myData._iUser.MainHands =  x._iUser.MainHands;
			myData._iUser.SideArms =  x._iUser.SideArms;
			myData._iUser.MaskNames =  x._iUser.MaskNames;
			myData._iUser.MaskDescs =  x._iUser.MaskDescs;
			myData._iUser.MaskReqShards =  x._iUser.MaskReqShards;
			myData._iUser.MaskNextShards =  x._iUser.MaskNextShards;
			myData._iUser.maskSize =  x._iUser.maskSize;
			myData._iUser.MaskAbilities =  x._iUser.MaskAbilities;

			myData._iUser.skyColor = x._iUser.skyColor;
			myData._iUser.Background = x._iUser.Background;
			myData._iUser.Light = x._iUser.Light;

			myData._iUser.curItem = x._iUser.curItem;
		}
	}
		
	public bool hasLoad()
	{
		try
		{
			//GameObject.FindGameObjectWithTag("Canvas").transform.FindChild("Work3").gameObject.active = true;
			UltraData x = Load(Path.Combine(_FileLocation, _FileName));
			if(x._iUser.On == false)
				return false;
			return true;
		}
		catch(Exception e)
		{
			return false;
		}

	}
	/// /// /// /// Player Stats Code /// /// /// /// /// /// /// 
	public void SavePlayer()
	{
		SaveBackUp ();
		Data();
		//Char.CursedOneHeart (false);
		myData._iUser.CurHp = Char.getCurHealth();
		myData._iUser.MaxHp = Char.getMaxHealth();
		myData._iUser.On = true;
		Save(Path.Combine(_FileLocation, _FileName));
	} 


	public void Data()
	{
		if(equip.curMask!=null)
			myData._iUser.maskName = equip.curMask.getName();



		if(equip.items[0]!=null)
			myData._iUser.item1 = findActiveItem(equip.items[0].itemName);
		if(equip.items[1]!=null)
			myData._iUser.item2 = findActiveItem(equip.items[1].itemName);
		if(equip.items[2]!=null)
			myData._iUser.item3 = findActiveItem(equip.items[2].itemName);
		if(equip.items[3]!=null)
			myData._iUser.item4 = findActiveItem(equip.items[3].itemName);

		if(equip.passiveItems[0]!=null)
			myData._iUser.passive1 = findPasssiveItem(equip.passiveItems[0].itemName);
		if(equip.passiveItems[1]!=null)
			myData._iUser.passive2 = findPasssiveItem(equip.passiveItems[1].itemName);
		if(equip.passiveItems[2]!=null)
			myData._iUser.passive3 = findPasssiveItem(equip.passiveItems[2].itemName);
		if(equip.passiveItems[3]!=null)
			myData._iUser.passive4 = findPasssiveItem(equip.passiveItems[3].itemName);

		myData._iUser.inventory = equip.inventory.getArray();
		myData._iUser.invSize = equip.inventory.size();

		myData._iUser.passiveInventory = equip.passiveInventory.getArray();
		myData._iUser.passiveInvSize = equip.passiveInventory.size();

		equip.DeEquipPassiveAll ();

		myData._iUser.keys = equip.keys.getArray();
		myData._iUser.keySize = equip.keys.size();

		myData._iUser.spells = equip.spells.getArray();
		myData._iUser.spellSize = equip.spells.size();

		myData._iUser.spellSlot1 = equip.spellSlots[0];
		myData._iUser.spellSlot2 = equip.spellSlots[1];
		myData._iUser.spellSlot3 = equip.spellSlots[2];
		myData._iUser.spellSlot4 = equip.spellSlots[3];
		myData._iUser.spellSlot5 = equip.spellSlots[4];

		myData._iUser.curItem = equip.curItem;
	}

	ITEMS findActiveItem(string name)
	{
		for (int i = 0; i < equip.inventory.size (); i++) {
			if(equip.inventory.get(i).itemName.Equals(name))
			{
					return equip.inventory.get(i);
			}
		}
		return null;
	}
	PASSIVEITEMS findPasssiveItem(string name)
	{
		for (int i = 0; i < equip.passiveInventory.size (); i++) {
			if(equip.passiveInventory.get(i).itemName.Equals(name))
			{
				return equip.passiveInventory.get(i);
			}
		}return null;

	}

	public void LoadPlayer()
	{
		try
		{
			
		}
		catch(Exception e)
		{
			
		}
	}





	/// /// /// /// Basic Code /// /// /// /// /// /// /// 
	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(UltraData));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, myData);
		}
	}

	public UltraData Load(string path)
	{
		var serializer = new XmlSerializer(typeof(UltraData));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as UltraData;
		}
	}

	//Loads the xml directly from the given string. Useful in combination with www.text.
	public UltraData LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(UltraData));
		return serializer.Deserialize(new StringReader(text)) as UltraData;
	}

	string UTF8ByteArrayToString(byte[] characters) 
	{      
		UTF8Encoding encoding = new UTF8Encoding(); 
		string constructedString = encoding.GetString(characters); 
		return (constructedString); 
	} 

	string SerializeObject(object pObject) 
	{ 
		string XmlizedString = null; 
		MemoryStream memoryStream = new MemoryStream(); 
		XmlSerializer xs = new XmlSerializer(typeof(UltraData)); 
		XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8); 
		xs.Serialize(xmlTextWriter, pObject); 
		memoryStream = (MemoryStream)xmlTextWriter.BaseStream; 
		XmlizedString = UTF8ByteArrayToString(memoryStream.ToArray()); 
		return XmlizedString; 
	} 

	void CreateXML() 
	{ 
		StreamWriter writer; 
		FileInfo t = new FileInfo(_FileLocation+"\\"+ _FileName); 
		if(!t.Exists) 
		{ 
			writer = t.CreateText(); 
		} 
		else 
		{ 
			t.Delete(); 
			writer = t.CreateText(); 
		} 
		writer.Write(_data); 
		writer.Close(); 
		Debug.Log("File written."); 
	} 
}

public class UltraData 
{
	public DemoData _iUser; 
	public UltraData() { } 

	public struct DemoData 
	{
		public string curScene;
		public string deathScene;
		public string lastScene;
		public string shrineScene;
		public int curShrine;
		public bool standing;

		public float playerPosX;
		public float playerPosY;
		public float playerPosZ;

		//BACKGROUND
		public float Light;
		public string Background;
		public Color skyColor;

		//TUTORIAL
		public bool[] tutorialChests;
		public bool[] tutorialItems;
		public bool tutorialIsBossDead;
		public bool tutorialIsNotFirstTime;
		public bool tutorialFightingBoss;
		public bool tutorialOn;
		public bool tutorialShortcut;
		public bool[] tutorialAreEnemiesDead;

		//ASIRE
		public bool[] asireChests;
		public bool[] asireItems;
		public bool[] asireIsBossDead;
		public bool asireIsNotFirstTime;
		public bool[] asireFightingBoss;
		public bool asireOn;
		public bool[] asireShortcuts;
		public bool[] asireEnemyDead;
		public int[] shrineRelicLevels;
		public bool[] asireAreEnemiesDead;

		//DOJO
		public bool dojoChest;
		public bool dojoIsBossDead;
		public bool dojoFightingBoss;

		//SEWER
		public bool[] sewerItems;
		public bool sewerOn;
		public bool sewerShortcut;
		public int sewerShrineRelicLevel;
		public bool[] sewerAreEnemiesDead;

		//Nexus
		public bool[] AsireShrines;
		public bool[] NoxusShrines;
		public bool[] ThenoShrines;
		public bool[] LucarShrines;

		public float CurHp;
		public float CurMana;
		public float MaxHp;
		public float MaxMana;
		public int heartShards;

		public int limit;
		public int limitGem;

		public int souls;
		public int shards;
		public string maskName;

		public ITEMS curItem;
		public ITEMS item1;
		public ITEMS item2;
		public ITEMS item3;
		public ITEMS item4;
		public PASSIVEITEMS passive1;
		public PASSIVEITEMS passive2;
		public PASSIVEITEMS passive3;
		public PASSIVEITEMS passive4;
		public ITEMS[] inventory;
		public PASSIVEITEMS[] passiveInventory;
		public int invSize;
		public int passiveInvSize;
		public SPELLS[] spells;
		public int spellSize;
		public SPELLS spellSlot1;
		public SPELLS spellSlot2;
		public SPELLS spellSlot3;
		public SPELLS spellSlot4;
		public SPELLS spellSlot5;

		public WEAPONS[] MainHands;
		public WEAPONS[] SideArms;
		public string[] MaskNames;
		public string[][] MaskAbilities;
		public string[] MaskDescs;
		public int[] MaskReqShards;
		public int[] MaskNextShards;

		public int maskSize;
		public ITEMS[] keys;
		public int keySize;

		public int graveSouls;
		public int graveLimit;
		public int graveLimitGem;
		public float gravePosX;
		public float gravePosY;
		public float gravePosZ;

		public bool On;
		public bool breakRing;
	}
}

