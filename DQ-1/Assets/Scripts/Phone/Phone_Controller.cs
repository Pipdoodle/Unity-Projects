using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class Phone_Controller: MonoBehaviour {
	//public GameObject Phone_Panel;
	//public _playerController PlayerObject; 
	public static bool phoneUP = false;
	private GameObject dimmer;
	// Use this for initialization
	void Start () {
		//PlayerObject = FindObjectOfType<_playerController> (); 
		transform.GetComponent<Image>().enabled = false;
		dimmer = GameObject.FindGameObjectWithTag ("dimmer");
		dimmer.GetComponent<Image>().enabled = false;
		transform.GetChild (0).gameObject.SetActive (false);
		transform.GetChild (1).gameObject.SetActive (false);
		transform.GetChild (2).gameObject.SetActive (false);
		transform.GetChild (3).gameObject.SetActive (false);
		transform.GetChild (4).gameObject.SetActive (false);
		phoneUP = false;
		Debug.Log("PHONE START!");
		}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp (KeyCode.Alpha1) && !phoneUP) {
			transform.GetComponent<Image>().enabled = true;
			dimmer.GetComponent<Image>().enabled = true;
			transform.GetChild (0).gameObject.SetActive (true);
			transform.GetChild (1).gameObject.SetActive (false);
			transform.GetChild (2).gameObject.SetActive (false);
			transform.GetChild (3).gameObject.SetActive (false);
			transform.GetChild (4).gameObject.SetActive (true);
			phoneUP = true;
			//_playerController.busy = true;

		
		} else if (Input.GetKeyUp (KeyCode.Alpha1) && phoneUP) {
			transform.GetComponent<Image>().enabled = false;
			dimmer.GetComponent<Image>().enabled = false;
			transform.GetChild (0).gameObject.SetActive (false);
			transform.GetChild (1).gameObject.SetActive (false);
			transform.GetChild (2).gameObject.SetActive (false);
			transform.GetChild (3).gameObject.SetActive (false);
			transform.GetChild (4).gameObject.SetActive (false);
			phoneUP = false;
			//_playerController.busy = false;

		}
	}
}