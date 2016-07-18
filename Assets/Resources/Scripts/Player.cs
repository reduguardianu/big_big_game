using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {

	[HideInInspector]
	public float currentSpeed = 0;
	[HideInInspector]
	public float distance = 0;
	public int hp;
	StageConfig stage;

    bool stopped = false;

	Dictionary<SpeedMod, float> speedMods;


	float moddedAcc = 0;
	float moddedMaxSpeed = 0;


	float elapsed = 0;


	bool initialized = false;
    public bool isOnGround;

	List<GameObject> collidedWith;
    float offset;


    public void Stop() {
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        stopped = true;
    }

    public void Init(StageConfig s, float o) {
        offset = o;


		collidedWith = new List<GameObject>();
		speedMods = new Dictionary<SpeedMod, float>();
		stage = s;

		initialized = true;

		GetComponent<CollisionHandler>().importantCollision += (tmp) => {
			if (collidedWith.IndexOf(tmp.gameObject) == -1) {
				collidedWith.Add(tmp.gameObject);
				OnCollision(tmp.gameObject);
			}
		};
        GetComponent<CollisionHandler>().groundCollisionsEnded += (tmp) => {
            isOnGround = false;
        };

        GetComponent<CollisionHandler>().groundCollisionsStart += (tmp) => {
            isOnGround = true;
        };
	}

	void OnCollision(GameObject collided) {
		var mods = collided.GetComponents<SpeedMod>();
		if (mods.Length > 0) {
			ApplySpeedMods(mods);
		}
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
				moddedAcc = moddedAcc * speedMod.accMul;
			}
			moddedAcc += speedMod.accAdd;

			if (speedMod.maxSpeedMul > 0) {
				moddedMaxSpeed = moddedMaxSpeed * speedMod.maxSpeedMul;
			}
			moddedMaxSpeed += speedMod.maxSpeedAdd;

		}
	}

	void ApplySpeedMods(SpeedMod[] mods) {
		foreach (SpeedMod mod in mods) {
			speedMods[mod] = elapsed;
			ProccessSpeedMods();
			currentSpeed =  Mathf.Clamp(currentSpeed +  mod.oneTimeSpeed, 0, moddedMaxSpeed);

		}
	}



	void Update() {
        if (!initialized || stopped) {
			return;
		}

		elapsed += Time.deltaTime;

		ProccessSpeedMods();

		currentSpeed = Mathf.Clamp(currentSpeed + moddedAcc * Time.deltaTime, 0, moddedMaxSpeed);
		distance = gameObject.transform.position.x + currentSpeed * Time.deltaTime;
		gameObject.transform.position = new Vector3(distance, gameObject.transform.position.y, gameObject.transform.position.z);
	}



}
