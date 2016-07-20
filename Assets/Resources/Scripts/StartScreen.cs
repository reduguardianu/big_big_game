﻿using UnityEngine;
using System.Collections;

public class StartScreen : MonoBehaviour {

	public string stageToLoad;

	public string keyToContinue;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown(keyToContinue)) {
			Application.LoadLevel(stageToLoad);
		}
	}
}
