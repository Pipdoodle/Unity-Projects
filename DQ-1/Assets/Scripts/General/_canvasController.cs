using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _canvasController : MonoBehaviour {
	private static bool playerExists;
	// Use this for initialization
	void Start () {
		if (!playerExists) {
			playerExists = true;
			transform.gameObject.DontDestroyOnLoad();
		} else
		{Destroy (gameObject);}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
