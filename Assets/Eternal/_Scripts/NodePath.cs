using UnityEngine;
using System.Collections;

public class NodePath : MonoBehaviour {

	public NodePath[] AdjNodes; //0-Right, 1-Left, 2-Up, 3-Down
	public bool[] actions; //0-Walk, 1-Jump
}
