using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

	private BoxCollider2D bounds;
	private _cameraController theCam;
	// Use this for initialization
	void Start () {
		bounds = GetComponent<BoxCollider2D> ();
		theCam = FindObjectOfType<_cameraController>();
		theCam.SetBounds (bounds);
	}

}
