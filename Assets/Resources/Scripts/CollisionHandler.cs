using UnityEngine;
using System.Collections;
using System;

public class CollisionHandler : MonoBehaviour {

	public event Action<GameChangingCollisionInfo> importantCollision;

	public event Action<GameObject> collisionsEnded;
	void OnCollisionEnter (Collision collider) {
		GameChangingCollisionInfo collisionInfo = collider.gameObject.GetComponent<GameChangingCollisionInfo>();
		if (collisionInfo != null)
		{
			if (importantCollision != null)
			{
				importantCollision(collisionInfo);
			}
		}
	}

	void onTriggerExit(Collision collider) {
		if (collisionsEnded != null)
		{
			collisionsEnded(collider.gameObject);
		}
	}
}
