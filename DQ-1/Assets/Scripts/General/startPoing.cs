using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startPoing : MonoBehaviour {

	private _playerController thePlayer;
	private _cameraController theCam;

	// Use this for initialization
	void Start () {
		thePlayer = FindObjectOfType<_playerController> ();
		theCam = FindObjectOfType<_cameraController> ();
		thePlayer.transform.position = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
