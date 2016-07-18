using UnityEngine;
using System.Collections;

public class StageConfig : MonoBehaviour {

	[Header("Prefabs")]
	public GameObject playerAPrefab;
	public GameObject playerBPrefab;

	[Header("Refrences")]
	public GameObject spawn;

	[Header("Settings")]
	public float maxSpeed;
	public float acceleration;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
