using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MainMenuEternal : MonoBehaviour 
{
	bool MenusOn = false;
	GameObject Canvas;
	public string shrineScene;
	public string curScene;
	public bool standing;
	public GameObject save;
	bool buttonPress;
	[SerializeField] EventSystem ES;

	void Start () 
	{
		Canvas = GameObject.FindGameObjectWithTag("Canvas");
	}

	void Update () 
	{
		if(MenusOn == true)
		{
			Canvas.transform.FindChild("SpaceStart").gameObject.active = false;
			Canvas.transform.FindChild("Buttons").gameObject.active = true;
			//Invoke ("Highlight", .1f);
//			if(Input.GetJoystickNames ().Length>0)
//				Highlight();
			MenusOn = false;
		}
		if((Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Jump"))&&!buttonPress)
		{
			buttonPress = true;
			MenusOn = true;
			if(this.GetComponent<UltraSave>().hasLoad())
			{
				Canvas.transform.FindChild("Buttons/Load Game").GetComponent<Button>().interactable = true;
			}
		}
	}

	public void NewGame()
	{
		GameObject MS;
		if(GameObject.FindGameObjectWithTag("MasterSave") == null)
			MS = Instantiate(save,save.transform.position,save.transform.rotation)as GameObject;
		
		if(curScene != "")
		{
			CheckForDelete();
		}
		else
		{	
			GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("FadeIn").gameObject.active = true;
			Invoke("NewFade",.7f);
		}
	}
	void NewFade()
	{
		GameObject MS = GameObject.FindGameObjectWithTag ("MasterSave");
		MS.GetComponent<UltraSave> ().Delete ();
		MS.GetComponent<UltraSave>().SaveBaseCharacter();
		//Application.LoadLevel("StoryIntro");
		Application.LoadLevel("NewTutorial");
	}

	public void Credits()
	{
		GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("FadeIn").gameObject.active = true;
		Invoke("LoadCredits",.7f);
	}

	public void LoadCredits()
	{
		Application.LoadLevel("Thanks");
	}

	void CheckForDelete()
	{
		this.transform.FindChild ("Options").gameObject.active = true;
	}
	public void YesNew()
	{
		GameObject.FindGameObjectWithTag("Canvas").transform.FindChild ("FadeIn").gameObject.active = true;
		Invoke("NewFade",.7f);
		this.transform.FindChild ("Options").gameObject.active = false;
	}
	public void NoNew()
	{
		this.transform.FindChild ("Options").gameObject.active = false;
	}
	public void LoadOkay()
	{
		this.transform.FindChild ("NoLoad").gameObject.active = false;
	}
	public void LoadGame()
	{
		GameObject MS;
		if(GameObject.FindGameObjectWithTag("MasterSave") == null)
			MS = Instantiate(save,save.transform.position,save.transform.rotation)as GameObject;
		
		if (curScene.Equals (""))
			ErrorMessage ();
		else {
			GameObject.FindGameObjectWithTag ("Canvas").transform.FindChild ("FadeIn").gameObject.active = true;
			Invoke ("LoadFade", .7f);
		}
	}
	void LoadFade()
	{
		 if (shrineScene == null || shrineScene.Equals ("") || standing)
			Application.LoadLevel (curScene);
		else if (shrineScene != "")
			Application.LoadLevel (shrineScene);
		else
			ErrorMessage ();
	}

	void ErrorMessage()
	{
		this.transform.FindChild ("NoLoad").gameObject.active = true;
	}
	public void Exit()
	{
		Application.Quit();
	}

	void Highlight()
	{
		ES.SetSelectedGameObject(null);
		ES.SetSelectedGameObject(this.transform.FindChild ("Buttons/New Game").gameObject);
	}
}
