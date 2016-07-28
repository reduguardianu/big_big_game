using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Player : MonoBehaviour {

	public int hp;
	public GameObject particles;
	public string winScreen;
	StageConfig stage;

    bool stopped = false;


	float elapsed = 0;

	bool initialized = false;

	float lastPenaltyPunch = 0;
	float lastDash = 0;


    public bool isOnGround;
    public bool isJumping;

	List<GameObject> collidedWith;
    float offset;

    public Animator playerAnimation;


    Vector2 v = Vector2.zero;
    public Vector2 pos = Vector2.zero;

    float groundY;

    void SetVx(float x) {
    	v = new Vector3(Mathf.Max(x, 0), v.y);
    }

    void SetVy(float y) {
    	v = new Vector3(v.x, y);
    }

    public void SetPosX(float x) {
    	pos = new Vector3(x, pos.y);
    }

    void SetPosY(float y) {
    	pos = new Vector3(pos.x, y);
    }

    float jumpTimestamp = -1f;

    public void Jump() {
    	if (isJumping) {
    		return;
    	}
    	jumpTimestamp = Time.time;
    	isJumping = true;
    }

    public void Dash() {
    	if (lastDash + stage.dashCooldown < elapsed) {
    		SetVx(v.x + stage.dashSpeedBoost);
            lastDash = Time.time;
    	}
    }


    public void PenaltyPunch() {
    	if (lastPenaltyPunch + stage.penaltyCooldown < elapsed) {
    		SetVx(v.x + stage.penaltySpeedBoost);
    		lastPenaltyPunch = elapsed;
    		hp--;
    	}
    }

    public void Stop() {
        stopped = true;
        playerAnimation.SetTrigger("Idle");
    }

    public void Init(StageConfig s, float o) {
    	stage = s;
        offset = o;

    	pos = gameObject.transform.position;
    	groundY = stage.groundLevel.transform.position.y;
        gameObject.transform.position = new Vector3(offset, 0, 0);

		collidedWith = new List<GameObject>();

		initialized = true;

		GetComponent<CollisionHandler>().importantCollision += (tmp) => {
			if (collidedWith.IndexOf(tmp.gameObject) == -1) {
				collidedWith.Add(tmp.gameObject);
				OnCollision(tmp.gameObject);
			}
		};
	}

	void OnCollision(GameObject collided) {
		GameObject part;
		Debug.Log("HIT");
		foreach (SpeedMod mod in collided.GetComponents<SpeedMod>()) {
			SetVx(v.x + mod.x);
			if(mod.x < 0)
			{
				gameObject.GetComponent<Player>().playerAnimation.SetTrigger("Hit");
				part = (GameObject)Instantiate(stage.negativeBuffParticles);
				part.transform.position = collided.transform.position;
			}else
			{
				part = (GameObject)Instantiate(stage.positiveBuffParticles);
				part.transform.position = collided.transform.position;
			}
		}

	}


	void Update() {
        if (!initialized || stopped) {
			return;
		}

		isOnGround = pos.y <= groundY;

		//FALLING

		float yChange = 0;
		float xChange = 0;
		if (isOnGround) {
			SetVy(0);
		} else if (!isOnGround && !isJumping) {
			float tmp = v.y - stage.fallAcc * Time.deltaTime;
			SetVy(tmp);
			SetPosY(pos.y + Time.deltaTime * v.y);
		} 

		if (isJumping) {
			
			if (Time.time - jumpTimestamp > stage.jumpTime) {
				isJumping = false;
			} else {
				float t = Time.time - jumpTimestamp;
				float e = stage.jumpCurve.Evaluate(t/stage.jumpTime);
				
				SetPosY(groundY + stage.jumpHeight * e);
			}
		}

		elapsed += Time.deltaTime;

		if (v.x > stage.maxSpeed) {
			 SetVx(Mathf.Max(v.x - stage.decceleration * Time.deltaTime, stage.maxSpeed));
		} else {
			SetVx(Mathf.Min(v.x + stage.acceleration * Time.deltaTime, stage.maxSpeed));
		}

		SetVx(Mathf.Min(v.x, stage.speedHardCap));
		xChange = v.x * Time.deltaTime;
		pos =  new Vector2(pos.x + xChange, Mathf.Max(groundY, pos.y));
        
        

		gameObject.transform.position = new Vector3(pos.x + offset, pos.y);

		UpdateVisuals();
	}

	void UpdateVisuals() {
		playerAnimation.SetBool("jump", !GetComponent<Player>().isOnGround);
        particles.SetActive(lastDash + 0.4f > elapsed);
        

        if (lastPenaltyPunch + 0.2f > elapsed) {
        	playerAnimation.SetTrigger("Hit");
        }
	}



}
