using UnityEngine;
using System.Collections;

public class MainLoop : MonoBehaviour {

	public StageConfig stageInit;

	public Camera cam;

	Player[] players;

	bool gameHasEnded = false;

	// Use this for initialization
	void Start () {
		players = new Player[2];
		GameObject player = Instantiate<GameObject>(stageInit.playerAPrefab);
		player.transform.position = stageInit.spawn.transform.position;

		players[0] = player.GetComponent<Player>();
		players[0].Init(stageInit, stageInit.startOffset);

		GameObject player2 = Instantiate<GameObject>(stageInit.playerBPrefab);
		player2.transform.position = stageInit.spawn.transform.position;

		players[1] = player2.GetComponent<Player>();
		players[1].Init(stageInit, 0);

	}

	void Won(Player player) {
		foreach (Player p in players) {
			p.Stop();
		}

		Debug.Log("WON PLAYER " + player.name);
		gameHasEnded = true;
	}
	
	// Update is called once per frame
	void Update () {
		if (gameHasEnded) {
			return;
		}


		Player moreAdvanced = null;
		foreach (Player player in players) {
			if (player == null) {
				continue;
			}

			if (moreAdvanced == null || moreAdvanced.distance < player.distance) {
				moreAdvanced = player;
			}
		}

		cam.GetComponent<CameraOperator>().target = moreAdvanced.gameObject;

		if (moreAdvanced.distance >= stageInit.finish.transform.position.x) {
			Won (moreAdvanced);
		}
	
	}
}
