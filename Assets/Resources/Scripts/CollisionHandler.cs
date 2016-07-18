using UnityEngine;
using System.Collections;
using System;

public class CollisionHandler : MonoBehaviour {

	public event Action<GameChangingCollisionInfo> importantCollision;

	public event Action<GameObject> groundCollisionsEnded;

    public event Action<GameObject> groundCollisionsStart;


    void OnTriggerEnter(Collider collider) {
        GameChangingCollisionInfo collisionInfo = collider.gameObject.GetComponent<GameChangingCollisionInfo>();
        if (collisionInfo != null)
        {
            if (importantCollision != null)
            {
                importantCollision(collisionInfo);
            }
        }
    }

	void OnCollisionEnter (Collision collider) {
        if (collider.gameObject.GetComponent<Ground>() != null)
        {
            if (groundCollisionsStart != null)
            {
                groundCollisionsStart(collider.gameObject);
            }  
        }
	}

	void OnCollisionExit(Collision collider) {
		if (groundCollisionsEnded != null && collider.gameObject.GetComponent<Ground>() != null)
		{
			groundCollisionsEnded(collider.gameObject);
		}
	} 

}
