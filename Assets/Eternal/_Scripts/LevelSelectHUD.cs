using UnityEngine;
using System.Collections;

public class LevelSelectHUD : MonoBehaviour {

	//Juuba Shrines
	public void GrassLands()
	{
		//GameObject.FindGameObjectWithTag("MasterSave").GetComponent<MasterSave>().setShrineNO(1);
		Application.LoadLevel("JuubaAbove");
	}
	public void LumberJack()
	{
		//GameObject.FindGameObjectWithTag("MasterSave").GetComponent<MasterSave>().setShrineNO(2);
		Application.LoadLevel("JuubaAbove");
	}
	public void ExitJuuba()
	{
		this.transform.FindChild("JuubaDomain").gameObject.active = false;
	}
	//Etharus Shrines
}
