using UnityEngine;
using System.Collections;

public class PlayerMover : MonoBehaviour {

	[SerializeField]
	private float moveForce;

	void OnTriggerStay(Collider collider) {
		if (collider.gameObject.GetComponent<Player>() == null) {
			return;
		}
		collider.gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(moveForce, 0, 0));
	}
}
