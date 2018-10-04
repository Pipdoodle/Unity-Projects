using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class DialogTester : MonoBehaviour {

	DialogReader dr;
	public bool enableScoreText;
	public bool singleText;
	public bool isAutoTriggered; // TODO: implement this for splash screens
	bool autoStarting;
	public GameObject black;
	public Image Diabox;
	public Text speakerTextbox;
	public GameObject speakerBackground = null;
	public Text fullSpanText;
	Text currTextUsed;

	public static string dialogFileName;
	public static string scoreFileName;

	public Text scoreText;
	bool textInProgress = false;
	int currWaitFrames = 0;
	int currWaitText = 0;
	List<string> waitTextList = new List<string>();
	Dictionary<string,int> waitTextDict = new Dictionary<string,int>();

	public static bool puzzling = false;
	bool selecting = false;
	int indexSelected = 0;
	bool jumpToAction = false;
	bool tagStarted = false;
	bool dialogEnded = false;
	int framesPerSec;
	List<string> currSelectOptions = new List<string>();
	Dictionary<string,string> selectActions = new Dictionary<string, string>();

	public bool hasThoughtSpiral = false;
	public static bool failedPuzzle = false;
	public static bool inputInAction = false;
	public static string currScene;
	public static bool cerealing = false;
	public static bool skipping = false;
    string filepath;

	// Use this for initialization
	void Start () {		
		autoStarting = isAutoTriggered;
		initDR();
		black.SetActive(false);
        filepath = Application.dataPath;
    }

	void initDR() {
		if (dr != null){
			dr.Terminate();
		}

		if (dialogFileName == null){
			GetSceneName();
			dialogFileName = Utility.sceneToFile[currScene];
		}

		string dialogPath = Utility.PrefixFile(dialogFileName.Trim());
		if (!dialogPath.EndsWith(".txt")){
			dialogPath += ".txt";
		}

		if (scoreFileName == null){
			scoreFileName = "scores.txt";
		}
		string scorePath = Utility.PrefixFile(scoreFileName.Trim());
		if (!scorePath.EndsWith(".txt")){
			scorePath += ".txt";
		}

		dr = new DialogReader(dialogPath, scorePath);
		if (DialogReader.variables.Count == 0 || DialogReader.varNames.Count == 0){
			dr.GetInitValues();
		}
		// if (dr.variables.Count == 0 || dr.varNames.Count == 0){
		// 	dr.GetInitValues();
		// }
		Debug.Log("dr initialized");
	}
	
	// Update is called once per frame
	void Update () {
		if (dr == null){
			initDR();
		}

		if (!dr.CheckDialogFileName(dialogFileName)){
			Debug.Log("dialogFileName:" + dialogFileName + ", actual: " + dr.GetDialogFileName());
			initDR();
			UpdateInScene();
			return;
		}

		if (black == null){
			UpdateInScene();
		}

		if (cerealing && Input.GetKeyUp(KeyCode.S)){
			dr.Jump("SkippedEating");
			jumpToAction = true;
			skipping = true;
		}

		//Debug.Log(failedPuzzle);

		if (autoStarting && !Init.initing){
			black.SetActive(true);
			Diabox.enabled = true;
			Debug.Log("autoStarting");
			dr.Begin("splash"); // for splash screens
			//_playerController.busy = true;
			_playerController.playerMoving = false;
			_playerController.busy = true;
			tagStarted = true;

		}

		if (textInProgress){
			black.SetActive(true);
			Diabox.enabled = true;
			Debug.Log("text in progress");
			ReformatWaitText();
		}

		if (selecting && !jumpToAction){
			_playerController.playerMoving = false;
			_playerController.busy = true;

			if (Input.GetKeyUp(KeyCode.DownArrow)){
				indexSelected++;
				if (indexSelected >= currSelectOptions.Count){
					indexSelected = currSelectOptions.Count-1;
				}
				ReformatChoices();

			} else if (Input.GetKeyUp(KeyCode.UpArrow)) {
				indexSelected--;
				if (indexSelected < 0){
					indexSelected = 0;
				}
				ReformatChoices();

			} else if (Input.GetKeyUp(KeyCode.Return)){
				selecting = false;
				string selectedOption = currSelectOptions[indexSelected];
				string jumpAction = selectActions[selectedOption];
				dr.Jump(jumpAction);
				jumpToAction = true;
			}


		} else if 
			(skipping || autoStarting || 
			(tagStarted && (!dialogEnded) 
				&& !puzzling && !textInProgress && !inputInAction &&
			(Input.GetKeyUp(KeyCode.Return) || jumpToAction))){
				indexSelected = 0;
				// if (tagStarted){
				// 	Debug.Log("tagStarted");
				// } 
				// if (!dialogEnded){
				// 	Debug.Log("!dialogEnded");
				// }
				// if (Input.GetKeyUp(KeyCode.Return)){
				// 	Debug.Log("Return");
				// }
				// if (jumpToAction){
				// 	Debug.Log("jumpToAction");
				// }
				autoStarting = false;
				skipping = false;
				jumpToAction = false;
				textInProgress = false;
				inputInAction = false;
				black.SetActive(true);
				Diabox.enabled = true;
				Debug.Log("entered");


				string[] thisAction = dr.NextAction();

				ReformatScores();

				if (thisAction[0].Equals("")) {
					Debug.Log("can't get current action");
				} else {
					selecting = false;
					currSelectOptions.Clear();
					switch(thisAction[0]){
						case "Text":
							_playerController.playerMoving = false;
							ReformatText(thisAction[1]);
							Debug.Log(thisAction[1]);
							break;
						case "WaitText":
							_playerController.playerMoving = false;
							_playerController.busy = true;
							ProcessWaitText(thisAction[1]);
							Debug.Log(thisAction[1]);
							textInProgress = true;
							framesPerSec = (int) Math.Round(1 / Time.deltaTime);
							break;
						case "Select":
							selecting = true;
							selectActions = dr.GetSelectChoices(Int32.Parse(thisAction[1]));
							foreach(KeyValuePair<string,string> entry in selectActions){
								currSelectOptions.Add(entry.Key);
								Debug.Log(entry.Key + " gives " + entry.Value);
							}
							_playerController.playerMoving = false;
							_playerController.busy = true;
							ReformatChoices();
							break;
						case "Puzzle":
							Debug.Log("entering puzzle");
							InitPuzzle(thisAction[1]);
							// if (hasThoughtSpiral || true){
							// 	Debug.Log("hasspiral");
							// 	ThoughtSpiralPuzzle.Initialize(thisAction[1], WinnableScores());
							// 	puzzling = true;
							// 	//_playerController.busy = true;
							// 	_playerController.playerMoving = false;
							// }
							break;
						case "End":
							Debug.Log("end of file reached");
							TerminateScene();
							break;
						case "EndTag":
							Debug.Log("end of tag reached");
							tagStarted = false;
							EndPart();
							break;
						case "COUCH":
							TerminateScene();
							TouchDaCouch (thisAction [1]);
							break;
						default:
							Debug.Log("unknown option " + thisAction[0]);
							break;
					}
				}
		} else {
			if (puzzling){
				//_playerController.busy = true;
				_playerController.playerMoving = false;
				_playerController.busy = true;
			} else {
				//_playerController.busy = false;
				_playerController.playerMoving = true;
				//_playerController.busy = false;
			}

		}

	}

	void EndPart(){
		black.SetActive (false);
		fullSpanText.text = "";
		fullSpanText.enabled = false;
		if (speakerBackground != null) speakerBackground.SetActive(false);
		speakerTextbox.text = "";
		speakerTextbox.enabled = false;
		//_playerController.busy = false;
		_playerController.playerMoving = true;
		_playerController.busy = false;


	}

	void TerminateScene(){
		dialogEnded = true;
		dr.Terminate();
		dr = null;
		EndPart();
		Debug.Log("scene terminated");
		Debug.Log(dr == null);
	}

	void ReformatChoices(){
		fullSpanText.enabled = true;
		fullSpanText.text = "";
		if (speakerBackground != null) speakerBackground.SetActive(false);
		speakerTextbox.enabled = false;

		for (int i=0; i < currSelectOptions.Count; i++){
			Debug.Log(i);
			if (i != indexSelected){
				fullSpanText.text += currSelectOptions[i] + "\n";
			} else {
				fullSpanText.text += "<b>" + currSelectOptions[i] + "</b>" + "\n";
			}
		}
		fullSpanText.text = fullSpanText.text.Trim();

	}

	void ReformatText(string fullText){ //ugly, edit later
		string[] parsedText = DialogReader.ParseBrackets(fullText);
		if (parsedText[0].Equals("")){
			fullSpanText.enabled = true;
			speakerTextbox.enabled = false;
			if (speakerBackground != null) speakerBackground.SetActive(false);
			fullSpanText.text = parsedText[1];
			currTextUsed = fullSpanText;
		} else {
			speakerTextbox.enabled = true;
			speakerTextbox.text = parsedText[0];
			if (speakerBackground != null) speakerBackground.SetActive(true);

			fullSpanText.enabled = true;
			fullSpanText.text = parsedText[1];
			currTextUsed = fullSpanText;
		}
	}

	bool WinnableScores(){
		foreach(string scoreName in DialogReader.varNames){
			if (DialogReader.variables[scoreName] < 50) {
				return false;
			}
		}
		return true;
	}

	void ProcessWaitText(string fullText){
		currTextUsed.text = "";
		waitTextDict.Clear();
		currWaitText = 0;		
		string[] txts = fullText.Split('\n');
		for(int i=0; i < txts.Length-1; i++){
			string txt = txts[i];
			Debug.Log("txt: " + txt);
			int txtEnd = txt.IndexOf("[wait=");
			string pureText = txt.Substring(0,txtEnd).Trim();

			int start = 6 + txtEnd;
			Debug.Log("start:" + start);

			string afterStart = txt.Substring(start,txt.Length-start);
			int end = afterStart.IndexOf("]");
			end += start;

			Debug.Log("end:" + end);
			string waitNumStr = txt.Substring(start, end-start).Trim();
			int waitNum = Int32.Parse(waitNumStr);

			int waitFrames = (int) Math.Ceiling(waitNum / Time.deltaTime);

			waitTextList.Add(pureText);
			waitTextDict.Add(pureText,waitFrames);
		}
		waitTextList.Add(txts[txts.Length-1].Trim());
		waitTextDict.Add(txts[txts.Length-1].Trim(),0);
		Debug.Log("processed wait text");

		currWaitText = 0;
		currWaitFrames = 0;
		textInProgress = true;
		ReformatText(waitTextList[0]);
	}

	void ReformatWaitText(){
		string txt = currTextUsed.text;
		if (currWaitFrames < waitTextDict[waitTextList[currWaitText]]){
			currWaitFrames++;
			if(currWaitFrames % framesPerSec == 0){
				if(txt.EndsWith("...")){
					currTextUsed.text = txt.Substring(0,txt.Length-2);
				} else {
					currTextUsed.text += ".";
				}
			}
		} else {
			if (currWaitText == waitTextList.Count-1){
				textInProgress = false;
				currWaitFrames = -1;
				currWaitText = -1;
				waitTextList.Clear();
				waitTextDict.Clear();
			} else {
				textInProgress = true;
				currWaitFrames = 0;
				currWaitText++;
				currTextUsed.text += "\n" + waitTextList[currWaitText];
			}

			/*reference
			bool textInProgress = false;
			int currWaitFrames = 0;
			int currWaitText = 0;
			List<string> waitTextList = new List<string>();
			Dictionary<string,int> waitTextDict = new Dictionary<string,int>();

			*/

		}
	}

	void ReformatScores(){
		// if (enableScoreText){
		// 	scoreText.text = "";
		// 	foreach(string scoreName in dr.varNames){
		// 		scoreText.text += scoreName + ": " + dr.variables[scoreName] + "\n";
		// 	}
		// 	scoreText.text = scoreText.text.Trim();
		// } 
		if (scoreText != null){
			scoreText.enabled = false;
		}
	}

	void InitPuzzle(string command){
		string[] parsedText = DialogReader.ParseBrackets(command);
		Debug.Log("parsed: " + parsedText[0] + "," +parsedText[1]);
		switch(parsedText[0].Trim().ToLower()){
			case "thoughtspiral":
				Debug.Log("hasspiral");
				Debug.Log("1:" + parsedText[1]);
				EndPart();
				ThoughtSpiralPuzzle.Initialize(parsedText[1], WinnableScores());
				puzzling = true;
				//_playerController.busy = true;
				_playerController.playerMoving = false;
				_playerController.busy = true;
				break;
			case "passwordinput":
				Debug.Log("entering password input");
				inputInAction = true;
				PasswordInput.activated = true;
				PasswordInput.inputEnabled = true;
				_playerController.playerMoving = false;
				_playerController.busy = true;
				break;
			default:
				Debug.Log("puzzle not found");
				break;
		}

	}

	void OnTriggerStay2D(Collider2D other){
		if (!autoStarting && !tagStarted && Input.GetKeyDown(KeyCode.Return)){
			if (dr == null){
				initDR();
			}
			string tagName = other.tag;
			Debug.Log(tagName);
			switch(tagName){
				case "DiningTable":
					Debug.Log("dining");
					if (other.gameObject != null){
						Cereal c = other.gameObject.GetComponent<Cereal>();
						if (c != null){
							dr.SetInfoTarget("cereal", c);
							cerealing = true;
						}
					}
					break;
				default:
					dr.DisableInfo();
					cerealing = false;
					break;
			}

			if (dr.Begin(tagName)){
				//_playerController.busy = true;
				_playerController.playerMoving = false;
				_playerController.busy = true;
				tagStarted = true;
				Debug.Log("beginning " + tagName);
			} else {
				//_playerController.busy = false;
				_playerController.playerMoving = true;
				_playerController.busy = false;
				tagStarted = false;
				Debug.Log("can't begin " + tagName);
			}
		}

	}

	//scene change
	void TouchDaCouch(string couchName){
		currScene = couchName;
		SceneManager.LoadScene (couchName);
		UpdateInScene();
		Debug.Log("couch:" + couchName);
	}

	void GetSceneName(){
		Scene currSceneObj = SceneManager.GetActiveScene();
		if (currSceneObj != null){
			currScene = currSceneObj.name;
		}
	}

	void UpdateInScene(){
		Debug.Log("begin updating scene");
		GetSceneName();

		dialogFileName = Utility.sceneToFile[currScene];
		Debug.Log("new file: " + dialogFileName);
		initDR();

		if (black == null){
			black = GameObject.FindWithTag("Dialog_Elements");
		}

		if (Diabox == null){
			Diabox = GameObject.FindWithTag("Diabox").GetComponent<Image>();
		}

		if (speakerTextbox == null){
			GameObject temp = GameObject.FindWithTag("speakerText");
			if (temp == null){
				Debug.Log("can't find speakerText by tag");
			} else {
				speakerTextbox = temp.GetComponent<Text>();
			}
		}

		if (speakerBackground == null){
			speakerBackground = GameObject.FindWithTag("speakerTextBG");
			if (speakerBackground != null) {
				Debug.Log("got speakerbg");
				speakerBackground.SetActive(false);
			}
		}

		if (fullSpanText == null){
			GameObject temp =  GameObject.FindWithTag("fullText");
			if (temp != null){
				fullSpanText = temp.GetComponent<Text>();
			} else {
				Debug.Log("can't find fullText by tag");
			}
		}

		if (scoreText != null){
			scoreText.enabled = false;
		}

		if (black != null) {
			Debug.Log("got black");
			black.SetActive(false);
		}
		if (Diabox != null) {
			Debug.Log("got diabox");
			Diabox.enabled = false;
		}
		if (speakerTextbox != null) {
			Debug.Log("got speakertxtbx");
			speakerTextbox.enabled = false;
		}
		if (fullSpanText != null) {
			Debug.Log("got fulltext");
			fullSpanText.enabled = false;
		}
		Debug.Log("finished updating scene");

	}

}
