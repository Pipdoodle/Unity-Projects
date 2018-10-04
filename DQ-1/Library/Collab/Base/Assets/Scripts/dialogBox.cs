using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class dialogBox : MonoBehaviour {

	public GameObject dBox;
	public Text dText;

	public bool dialogActive;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (dialogActive && Input.GetKeyUp (KeyCode.Space)) {
			dBox.SetActive (false);
			dText.text = "";
			dialogActive = false;
		}
	}

	public void ShowBox (string dialog)
	{
		dialogActive = true;
		dBox.SetActive (true);
		dText.text = dialog;
	}

	public void Clear () {
		dBox.SetActive (false);
		dText.text = "";
		dialogActive = false;
	}
}
