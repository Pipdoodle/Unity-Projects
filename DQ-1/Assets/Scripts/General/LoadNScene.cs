using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNScene : MonoBehaviour {

	public GameObject black;
	public GameObject Diabox;
	public GameObject speakerTextbox;
	public GameObject fullSpanText;
	public GameObject scoreText;
	//public GameObject speakerBackground;

	public static bool newSceneLoaded = false;

	public string newLevelName;

	// Use this for initialization
	void Start () {
       // SceneManager.LoadScene("Introduction 1");
    }
	
	// Update is called once per frame
	void Update () {
		/*if (newSceneLoaded) {
			newSceneLoaded = false;
			Debug.Log (lManager._ddolObjs.ToArray ().ToString());
			string[] array = { "a", "b" };
			lManager._ddolObjs.ForEach (Debug.Log);
			black = lManager._ddolObjs.Find (obj => obj.name == "Canvas"); //.transform.GetChild(0)
			if (black == null) {
				Debug.Log (black);
				Debug.Log ("hello");
			}
			Debug.Log (black);
			Diabox = black.transform.GetChild(0).GetComponent<Image> ();
			fullSpanText = black.transform.GetChild(1).GetComponent<Text> ();
			shortSpanText = black.transform.GetChild(2).GetComponent<Text> ();
			speakerTextbox = black.transform.GetChild(3).GetComponent<Text> ();
			scoreText = black.transform.GetChild(4).GetComponent<Text> ();

		}*/
	}

	void OnTriggerEnter2D( Collider2D other)
	{
		/*if (newSceneLoaded) {
			SceneManager.LoadScene (newLevelName);
		}*/
		black.SetActive (true); //necessary?
		fullSpanText.GetComponent<Text>().enabled = true;
		Diabox.GetComponent<Image>().enabled = true;
		speakerTextbox.SetActive(false);
		// speakerTextbox.GetComponent<Text>().enabled = false;
		//speakerBackground.SetActive(false);
		fullSpanText.GetComponent<Text>().text = "Press ENTER to Continue to" + newLevelName;
		Debug.Log ("ontrigger LoadNScene");
	}

	void OnTriggerStay2D( Collider2D other)
	{
		if (Input.GetKeyUp(KeyCode.Return)) {  //Iliano doesn't know what good style is.
			black.SetActive (false);
			fullSpanText.GetComponent<Text>().enabled = false;
			Diabox.GetComponent<Image>().enabled = false;
			fullSpanText.GetComponent<Text>().text = "";
			newSceneLoaded = true;
			SceneManager.LoadScene (newLevelName);
			DialogTester.dialogFileName = Utility.sceneToFile[newLevelName];
			DialogTester.currScene = newLevelName;
			Debug.Log("level: " + newLevelName + ", filename: " + Utility.sceneToFile[newLevelName]);
		}
	}

	void OnTriggerExit2D( Collider2D other)
	{
		black.SetActive (false); //necessary?
		fullSpanText.GetComponent<Text>().enabled = false;
		Diabox.GetComponent<Image>().enabled = false;
		fullSpanText.GetComponent<Text>().text = "";
	}

}
//other.gameObject.name == "mainChar"