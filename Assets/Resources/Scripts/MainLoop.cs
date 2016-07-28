using UnityEngine;
using System.Collections;
using System.Linq;

public class MainLoop : MonoBehaviour {

	public StageConfig stageConfig;

	public Camera cam;

	Player[] players;

	bool gameHasEnded = false;

	// Use this for initialization
	void Start () {
		players = new Player[2];
		GameObject player = Instantiate<GameObject>(stageConfig.playerAPrefab);
		player.transform.position = stageConfig.spawn.transform.position;
		players[0] = player.GetComponent<Player>();
		players[0].Init(stageConfig, stageConfig.startOffset);
        players[0].GetComponent<Player>().playerAnimation.SetTrigger("Run");

        GameObject player2 = Instantiate<GameObject>(stageConfig.playerBPrefab);
		player2.transform.position = stageConfig.spawn.transform.position;

		players[1] = player2.GetComponent<Player>();
		players[1].Init(stageConfig, 0);
        players[1].GetComponent<Player>().playerAnimation.SetTrigger("Run");

	}

	float winScreenCounter = 0;
	Player winner;

	void Won(Player player) {
		foreach (Player p in players) {
			p.Stop();
		}

		Debug.Log("WON PLAYER " + player.name);
		gameHasEnded = true;
		winner = player;
	}


	
	// Update is called once per frame
	void Update () {
		if (gameHasEnded) {
			winScreenCounter += Time.deltaTime;
			if (stageConfig.winScreenDelay < winScreenCounter) {
				Application.LoadLevel(winner.winScreen);
			}
			return;
		} 

		for (int i = 0; i < players.Length; i++) {

			if (players[i].hp <= 0) {
				Won(players[(i + 1) % players.Length]);
				return;
			}
		}


		var first = players[0];
		var second = players[1];
		if (second.pos.x > first.pos.x) {
			first = players[1];
			second = players[0];
		}

		if (first.pos.x - second.pos.x > stageConfig.penaltyDistance) {
			second.SetPosX(first.pos.x - stageConfig.penaltyDistance);
			second.PenaltyPunch();
		}

		
		cam.GetComponent<CameraOperator>().target = first.gameObject;
		///ukasz edit
		cam.GetComponent<CameraOperator>().second = second.gameObject;
		///

		if (first.pos.x >= stageConfig.finish.transform.position.x) {
			Won (first);
		}
	
	}
}
