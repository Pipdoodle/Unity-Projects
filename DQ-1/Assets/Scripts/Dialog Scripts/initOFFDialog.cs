using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class initOFFDialog : MonoBehaviour {

	// Use this for initialization
	void Start () {
		transform.GetChild (0).GetComponent<Image>().enabled = false;
		transform.GetChild (1).GetComponent<Text>().enabled = false;
		transform.GetChild (2).GetComponent<Text>().enabled = false;
		transform.GetChild (3).GetComponent<Text>().enabled = false;
		transform.GetChild (4).GetComponent<Text>().enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
