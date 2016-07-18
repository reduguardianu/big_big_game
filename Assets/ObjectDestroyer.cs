using UnityEngine;
using System.Collections;

public class ObjectDestroyer : MonoBehaviour {

	void OnTriggerEnter(Collider collider) {
		if (collider.gameObject.GetComponent<Ground>() != null) {
			return;
		}
		GameObject.Destroy(collider.gameObject);
	}
}
