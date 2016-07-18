using UnityEngine;
using System.Collections;

public class MovementComponent : MonoBehaviour {

    float baseJump = 3;
    float jumpPressTime;




	void Start () {
    }

    private float OutQuintic(float t) 
    {
        return (t*t*t*t*t -5f*t*t*t*t+ 10f*t*t*t + -10f*t*t + 5f*t);
    }

	void Update () {
    
        if (Input.GetButtonDown("Jump") && this.GetComponent<Player>().isOnGround) 
        {
            jumpPressTime = Time.time;
            Debug.Log("click jump");

           // this.transform.localPosition += new Vector3(0,3,0);
        } else if (Input.GetButtonDown("Dash")) {
            Debug.Log("click dash");
            this.transform.localPosition += new Vector3(0,0,3);
        
        }

        if (Input.GetButton("Jump") && Time.time - jumpPressTime <= 1)
        {
            this.GetComponent<Rigidbody>().AddForce(new Vector3(0, baseJump - Mathf.Log(Time.time - jumpPressTime + 1)/Mathf.Log(4), 0));
        
        }
	
	}
}
