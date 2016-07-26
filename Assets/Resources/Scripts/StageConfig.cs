using UnityEngine;
using System.Collections;

public class StageConfig : MonoBehaviour {

	[Header("Prefabs")]
	public GameObject playerAPrefab;
	public GameObject playerBPrefab;

	[Header("References")]
	public GameObject spawn;
	public GameObject finish;
	public GameObject groundLevel;

	[Header("Settings")]
	public float maxSpeed;
	public float speedHardCap;
	public float acceleration;
	public float decceleration;
	public float startOffset;
	public float dashSpeedBoost;
	public float dashCooldown;

	public float penaltyDistance;
	public float penaltySpeedBoost;
	public float penaltyCooldown;
	public float fallAcc;

	public float jumpTime;
	public float jumpHeight;
	public AnimationCurve jumpCurve;

	public float winScreenDelay = 3f;
	public string player1WinScene;
	public string player2WinScene;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
