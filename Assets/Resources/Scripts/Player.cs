using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	[HideInInspector]
	public float currentSpeed = 0;
	[HideInInspector]
	public float distance = 0;

	StageConfig stage;

	bool initialized = false;

	public void Init(StageConfig s) {
		stage = s;

		initialized = true;
	}

	void Update() {
		if (!initialized) {
			return;
		}
		currentSpeed = Mathf.Clamp(currentSpeed + stage.acceleration * Time.deltaTime, 0, stage.maxSpeed);
		distance += currentSpeed * Time.deltaTime;

		gameObject.transform.position = new Vector3(distance, 0, 0);
	}


}
