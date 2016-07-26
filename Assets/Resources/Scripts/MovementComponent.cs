using UnityEngine;
using System.Collections;

public class MovementComponent : MonoBehaviour {

    [SerializeField]
    string jump;
    [SerializeField]
    string dash;
    

    void Start () {
    }

	void Update () {

        Player p = GetComponent<Player>();

        

        if (Input.GetButtonDown(jump)) {
            Debug.Log("jump");
            p.Jump();
        }
        if (Input.GetButtonDown(dash)) {
            Debug.Log("dash");
            p.Dash();
        }
	
	}
}
