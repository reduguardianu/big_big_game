using UnityEngine;
using System.Collections;

public class MainLoop : MonoBehaviour {

	public StageConfig stageInit;

	public Camera cam;

	Player[] players;

	// Use this for initialization
	void Start () {
		players = new Player[2];
		GameObject player = Instantiate<GameObject>(stageInit.playerAPrefab);
		player.transform.position = stageInit.spawn.transform.position;

		players[0] = player.GetComponent<Player>();
		players[0].Init(stageInit);
	}
	
	// Update is called once per frame
	void Update () {
		float topDistance = 0;
		foreach (Player player in players) {
			if (player == null) {
				continue;
			}

			if (topDistance < player.distance) {
				topDistance = player.distance;
			}
		}

		if (cam.transform.position.x < topDistance) {
			cam.transform.position = new Vector3(topDistance, cam.transform.position.y, cam.transform.position.z);
		}
	
	}
}
