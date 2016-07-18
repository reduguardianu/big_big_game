using UnityEngine;
using System.Collections;
using System;

public class CollisionHandler : MonoBehaviour {

	public event Action<GameChangingCollisionInfo> importantCollision;
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
}
