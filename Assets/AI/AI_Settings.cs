using UnityEngine;
using System.Collections;

public class AI_Settings : MonoBehaviour {

	GameObject Player;
	GameObject Enemy;
	GameObject[] nodes;

	[SerializeField]GameObject curPlayer;
	[SerializeField]GameObject curEnemy;

	float p = 0;
	float e = 0;

	void Start()
	{
		Player = Resources.Load ("AI_Player") as GameObject;
		Enemy =Resources.Load ("AI_Enemy") as GameObject;
		nodes = GameObject.FindGameObjectsWithTag ("Node");

		Vector3 playerPos = nodes [Random.Range (0, nodes.Length)].transform.position;
		Vector3 enemyPos = nodes [Random.Range (0, nodes.Length)].transform.position;

		curPlayer = Instantiate (Player, playerPos, Player.transform.rotation)as GameObject;
		curEnemy = Instantiate (Enemy, enemyPos, Enemy.transform.rotation)as GameObject;
	}

	void FixedUpdate()
	{
		if (p > 0)
			p -= Time.deltaTime;
		if (e > 0)
			e -= Time.deltaTime;

		if (curPlayer == null&&p<=0) {
			Vector3 playerPos = nodes [Random.Range (0, nodes.Length)].transform.position;
			curPlayer = Instantiate (Player, playerPos, Player.transform.rotation)as GameObject;
			p = 1;
		}
		if (curEnemy == null&&e<=0) {
			Vector3 enemyPos = nodes [Random.Range (0, nodes.Length)].transform.position;
			curEnemy = Instantiate (Enemy, enemyPos, Enemy.transform.rotation)as GameObject;
			e = 1;
		}
	}
}
