using UnityEngine;
using System.Collections;

public class MovementComponent : MonoBehaviour {

    float baseJump = 5;
    float jumpPressTime;
    float dashCooldown = 0.5f;
    float dashUseTime;
    [SerializeField]
    string jump;
    [SerializeField]
    string dash;


	void Start () {
    }

	void Update () {

        
        if (Input.GetButtonDown(jump) && this.GetComponent<Player>().isOnGround) 
        {
            jumpPressTime = Time.time;
            Debug.Log("click jump");
        }
        if (Input.GetButtonDown(dash) && Time.time > dashUseTime + 0.5f) {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(60, 0, 0));
            dashUseTime = Time.time;
        }

        if (Input.GetButton(jump) && Time.time - jumpPressTime <= 0.2f)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, baseJump - Mathf.Log(Time.time - jumpPressTime + 0.2f)/Mathf.Log(6), 0));
        }
	
	}
}
