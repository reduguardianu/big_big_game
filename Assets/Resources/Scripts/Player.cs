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


	float elapsed = 0;

	bool initialized = false;
    public bool isOnGround;

	List<GameObject> collidedWith;
    float offset;

    public Animator playerAnimation;

    public float CurrentVerticalSpeed {
    	get {
    		return GetComponent<Rigidbody>().velocity.x;
    	}
    	set {
    		GetComponent<Rigidbody>().velocity = new Vector3(value, GetComponent<Rigidbody>().velocity.y);
    	}
    }

    public void Stop() {
		GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
        stopped = true;
    }

    public void Init(StageConfig s, float o) {
        offset = o;
        gameObject.transform.position = new Vector3(offset, 0, 0);


		collidedWith = new List<GameObject>();
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
		foreach (SpeedMod mod in collided.GetComponents<SpeedMod>()) {
			CurrentVerticalSpeed =+ mod.x;
			gameObject.GetComponent<Player>().playerAnimation.SetTrigger("Hit");
		}

		foreach (Bumper mod in collided.GetComponents<Bumper>()) {
			GetComponent<Rigidbody>().AddForce(new Vector3(mod.x, mod.y, 0));
			gameObject.GetComponent<Player>().playerAnimation.SetTrigger("Hit");
		}

	}


	void Update() {
        if (!initialized || stopped) {
			return;
		}

		elapsed += Time.deltaTime;

		float delta = stage.acceleration * Time.deltaTime;

		if (CurrentVerticalSpeed > stage.maxSpeed) {
			CurrentVerticalSpeed = Mathf.Max(CurrentVerticalSpeed - delta, stage.maxSpeed);
		} else {
			CurrentVerticalSpeed = Mathf.Min(CurrentVerticalSpeed + delta, stage.maxSpeed);
		}

		CurrentVerticalSpeed = Mathf.Min(CurrentVerticalSpeed, stage.speedHardCap);
		distance = gameObject.transform.position.x - offset;
	}



}
