using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {

	[HideInInspector]
	public float currentSpeed = 0;
	[HideInInspector]
	public float distance = 0;

	StageConfig stage;


	Dictionary<SpeedMod, float> speedMods;


	float moddedAcc = 0;
	float moddedMaxSpeed = 0;


	float elapsed = 0;


	bool initialized = false;

	public void Init(StageConfig s) {
		speedMods = new Dictionary<SpeedMod, float>();
		stage = s;

		initialized = true;
	}

	void ProccessSpeedMods() {
		moddedMaxSpeed = stage.maxSpeed;
		moddedAcc = stage.acceleration;

		var sorted = speedMods.Keys.OrderBy(x => speedMods[x]);

		foreach (SpeedMod speedMod in sorted) {
			if (speedMods[speedMod] + speedMod.duration < elapsed) {
				continue;
			}

			if (speedMod.accMul > 0) {
				moddedAcc += moddedAcc * speedMod.accMul;
			}
			moddedAcc += speedMod.accAdd;

			if (speedMod.maxSpeedMul > 0) {
				moddedMaxSpeed += moddedMaxSpeed * speedMod.maxSpeedMul;
			}
			moddedMaxSpeed += speedMod.maxSpeedAdd;

		}
	}

	void ApplySpeedMod(SpeedMod mod) {
		speedMods[mod] = elapsed;
		ProccessSpeedMods();
		currentSpeed += mod.oneTimeSpeed;
	}



	void Update() {
		if (!initialized) {
			return;
		}

		elapsed += Time.deltaTime;

		ProccessSpeedMods();

		currentSpeed = Mathf.Clamp(currentSpeed + moddedAcc * Time.deltaTime, 0, moddedMaxSpeed);
		distance += currentSpeed * Time.deltaTime;

		gameObject.transform.position = new Vector3(distance, 0, 0);
	}



}
