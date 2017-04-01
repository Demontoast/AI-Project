using UnityEngine;
using System.Collections;

public class Paths : MonoBehaviour {

	public Paths Right;
	public Paths Left;
	public Paths Up;
	public Paths Down;
	public type RightType;
	public type LeftType;
	public type UpType;
	public type DownType;
	public int name;
	public int index;

	public enum type
	{
		None,Walk,Jump,LadderUp,LadderDown
	}

}
