using UnityEngine;
using System.Collections;

public class MovementComponent : MonoBehaviour {

    float baseJump = 5;
    float jumpPressTime;
    float dashCooldown = 0.5f;
    float dashUseTime;


	void Start () {
    }

	void Update () {

        
          if (Input.GetButtonDown("Jump") && this.GetComponent<Player>().isOnGround) 
        {
            jumpPressTime = Time.time;
            Debug.Log("click jump");

           // this.transform.localPosition += new Vector3(0,3,0);
        }
        if (Input.GetButtonDown("Dash") && Time.time > dashUseTime + 0.5f) {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(60, 0, 0));
            dashUseTime = Time.time;
        }

        if (Input.GetButton("Jump") && Time.time - jumpPressTime <= 0.2f)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, baseJump - Mathf.Log(Time.time - jumpPressTime + 0.2f)/Mathf.Log(6), 0));
        }
	
	}
}
