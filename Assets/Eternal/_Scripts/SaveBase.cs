using UnityEngine; 
using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class SaveBase : MonoBehaviour {

	protected string _FileLocation,_FileName; 
	protected SettingsData myData;

	void Start () 
	{
		myData=new SettingsData(); 
	} 

	public void Delete()
	{
		Save(Path.Combine(_FileLocation, _FileName));
	}

	public void Save(string path)
	{
		var serializer = new XmlSerializer(typeof(SettingsData));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, myData);
		}
	}
	
	public SettingsData Load(string path)
	{
		var serializer = new XmlSerializer(typeof(SettingsData));
		using(var stream = new FileStream(path, FileMode.Open))
		{
			return serializer.Deserialize(stream) as SettingsData;
		}
	}
	
	//Loads the xml directly from the given string. Useful in combination with www.text.
	public SettingsData LoadFromText(string text) 
	{
		var serializer = new XmlSerializer(typeof(SettingsData));
		return serializer.Deserialize(new StringReader(text)) as SettingsData;
	}

	public virtual void SaveData(){}
	public virtual void LoadData(){}
	public virtual void BossKill(){}

}
public class SettingsData 
{ 
	public DemoData _iUser; 
	public SettingsData() { } 
	
	public struct DemoData 
	{
		public int curShrine;
		public bool[] chests;
		public bool[] items;
		public bool isBossDead;
		public bool isNotFirstTime;
		public bool On;

		public bool[] rooms;
	}

}
