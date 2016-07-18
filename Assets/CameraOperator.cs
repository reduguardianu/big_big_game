using UnityEngine;
using System.Collections;

public class CameraOperator : MonoBehaviour {
	float dampTime = 0.0f; //offset from the viewport center to fix damping
	Vector3 velocity = Vector3.zero;
	public GameObject target;

	// Use this for initialization
	void Start () {
	
	}
	
	void LateUpdate() {
		if(target) {
			Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.transform.position);
			Vector3 delta = target.transform.position -
						GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
			var destination = transform.position + delta;
			destination.y = transform.position.y;
			destination.z = transform.position.z;
			transform.position = Vector3.SmoothDamp(transform.position, destination, 
													ref velocity, dampTime);
		}
	}
}
