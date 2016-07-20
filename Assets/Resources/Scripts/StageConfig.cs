using UnityEngine;
using System.Collections;

public class StageConfig : MonoBehaviour {

	[Header("Prefabs")]
	public GameObject playerAPrefab;
	public GameObject playerBPrefab;

	[Header("Refrences")]
	public GameObject spawn;
	public GameObject finish;

	[Header("Settings")]
	public float maxSpeed;
	public float speedHardCap;
	public float acceleration;
	public float startOffset;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
