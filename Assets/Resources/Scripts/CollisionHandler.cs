using UnityEngine;
using System.Collections;
using System;

public class CollisionHandler : MonoBehaviour {

	public event Action<GameChangingCollisionInfo> importantCollision;

	public event Action<GameObject> groundCollisionsEnded;

    public event Action<GameObject> groundCollisionsStart;


	void OnCollisionEnter (Collision collider) {
		GameChangingCollisionInfo collisionInfo = collider.gameObject.GetComponent<GameChangingCollisionInfo>();
		if (collisionInfo != null)
		{
			if (importantCollision != null)
			{
				importantCollision(collisionInfo);
			}
		}
        if (collider.gameObject.GetComponent<Ground>() != null)
        {
            if (groundCollisionsStart != null)
            {
                groundCollisionsStart(collider.gameObject);
            } 
        }
	}

	void onTriggerExit(Collision collider) {
		if (groundCollisionsEnded != null && collider.gameObject.GetComponent<Ground>() != null)
		{
			groundCollisionsEnded(collider.gameObject);
		}
	}

}
