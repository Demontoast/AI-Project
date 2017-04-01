using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SaveDeleteLoad : MonoBehaviour {

//	public Slider loadBar;
//	public int loadBarLoads;
//	public GameObject save;
//
//	void Start () 
//	{
//		loadBar.maxValue = 3;
//		loadBar.minValue = 0;
//		Invoke("Loading",1f);
//	}
//
//	void Loading()
//	{
//		if(this.GetComponent<PlayerSave>().hasLoad())
//		{
//			//this.GetComponent<PlayerSave>().Delete();
//			loadBar.value++;
//			//this.GetComponent<MasterSave>().Delete();
//			loadBar.value++;
////			this.GetComponent<SaveJuubasDomain>().Delete();
////			loadBar.value++;
////			this.GetComponent<SaveJuubasUnderground>().Delete();
////			loadBar.value++;
////			this.GetComponent<SaveStoneKnightBoss>().Delete();
////			loadBar.value++;
//			this.GetComponent<SaveTestBuild>().Delete();
//			loadBar.value++;
//		}
//		else
//		{
//			loadBar.value = loadBar.maxValue;
//		}
//		Invoke("Loaded",1f);
//	}
//	void Loaded()
//	{
//		GameObject MS = Instantiate(save,save.transform.position,save.transform.rotation)as GameObject;
//		MS.GetComponent<MasterSave>().SaveBaseCharacter();
//		//Application.LoadLevel("StoryIntro");
//		Application.LoadLevel("TestingBuild");
//	}

}
