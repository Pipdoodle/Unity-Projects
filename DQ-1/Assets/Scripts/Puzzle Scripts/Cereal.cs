using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cereal : MonoBehaviour {

	bool started = false;
	public Image chee_1;
	public Image chee_2;
	public Image chee_3;
	public Image corn_1;
	public Image corn_2;
	public Image corn_3;
	public Image sad_1;
	public Image sad_2;
	public Image sad_3;
    bool sadMode;
    bool corn;
    bool chee;
    public DialogReader dr;
    public GameObject skipBar;

	// Use this for initialization
	void Start () {
		DisableAll();
		skipBar.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void Begin(DialogReader DR){
		started = true;
		dr = DR;
		Debug.Log("is dr null in Begin: " + (dr == null));
		skipBar.SetActive(true);
		Debug.Log("began");
	}

    void DisableAll(){
		chee_1.enabled = false;
		chee_2.enabled = false;
		chee_3.enabled = false;
		corn_1.enabled = false;
		corn_2.enabled = false;
		corn_3.enabled = false;
		sad_1.enabled = false;
		sad_2.enabled = false;
		sad_3.enabled = false;
	}

	public void OnSkip(){
		Debug.Log("onSkip!!");
		Debug.Log("is dr null" + (dr == null));
		if (dr != null) {
			dr.Jump("SkippedEating");
			Debug.Log("skipped eating");
		}
	}

	public void SetImage(string type){
		skipBar.SetActive(true);
		Debug.Log("received: " + type);
		switch(type){
			case "Colorful":
				sadMode = false;
				break;
			case "Bland":
				sadMode = true;
				break;
			case "Cinnamon":
				DisableAll();
				chee = true;
				corn = false;
				if(sadMode){
					sad_1.enabled = true;
				} else {
					chee_1.enabled = true;
				}
				break;
			case "Corn":
				DisableAll();
				chee = false;
				corn = true;
				if(sadMode){
					sad_1.enabled = true;
				} else {
					corn_1.enabled = true;
				}
				break;
			case "Pour":
				DisableAll();
				if(sadMode){
					sad_2.enabled = true;
				} else if (chee) {
					chee_2.enabled = true;
				} else if (corn){
					corn_2.enabled = true;
				}
				break;
			case "FinishBowl":
				DisableAll();
				if(sadMode){
					sad_3.enabled = true;
				} else if (chee) {
					chee_3.enabled = true;
				} else if (corn){
					corn_3.enabled = true;
				}
				break;
			case "DoneEating":
			case "End":
				DisableAll();
				skipBar.SetActive(false);
				break;
			case "Table":
				DisableAll();
				skipBar.SetActive(false);
				break;
			default:
				return;
		}

	}
}
