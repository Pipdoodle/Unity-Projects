using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class PasswordInput : MonoBehaviour {

	//deprecated code kept for reference and/or generalization
	// StreamReader sr;
	// static Dictionary<string,int> variables = new Dictionary<string,int>();
	// public Text prompt;
	// public Text response;
	// public static bool activated = false;
	// bool prompting = false;
	// TypedInput ti = new TypedInput();
	// List<InputMatch> matchers = new List<InputMatch>();
	// public static readonly string ALL_MATCHER = "[All]";

	// // Use this for initialization
	// void Start () {
		
	// }

	// struct InputMatch{
	// 	string matcher;
	// 	Dictionary<string,string> actions;
	// }

	// public static void Initialize(string fileName){
	// 	string path = "Assets/Resources/Text/" + fileName.Trim();
	// 	sr = new StreamReader(path);
	// 	variables.clear();
	// 	activated = true;
	// }

	// // Update is called once per frame
	// void Update () {
	// 	if (activated){
	// 		NextAction();
	// 	}
	// }

	// void NextAction(){
	// 	if (sr.Peek() == null){
	// 		Terminate();
	// 	}
	// 	string currLine = sr.ReadLine().Trim();
	// 	string[] parts = currLine.Split(':');
	// 	switch(parts[0].Trim()){
	// 		case "EndPrompt":
	// 			Terminate();
	// 			break;

	// 		case "Prompt":
	// 			prompting = true;
	// 			prompt.text = parts[1].Trim();
	// 			break;
	// 		case "InitVar":
	// 			string[] nameVal = parts[1].Split('(');
	// 			string val = nameVal[1].Trim().Replace(")","");
	// 			variables[nameVal[0].Trim()] = Int32.Parse(val);
	// 			break;
	// 		case "Input":
	// 			if (Equals(parts[1].Trim(),ALL_MATCHER){
	// 				InputMatch newMatcher = new InputMatch();
	// 				newMatcher.matcher = ALL_MATCHER;
	// 				newMatcher.actions = new Dictionary<string,string>();
	// 				while(sr.Peek() != null){
	// 					string currAction = sr.ReadLine().Trim();
	// 					while(currAction.EndsWith("\\")){
	// 						currAction += "\n" + sr.ReadLine().Trim();
	// 					}
	// 					string[] splittedAction = currAction.Split(':');
	// 					switch(splittedAction[0].Trim()){
	// 						case "Switch":
	// 							int numIters = Int32.Parse(splittedAction[1].Trim());
	// 							for (int i=0; i<numIters; i++){
	// 								string newLine = sr.ReadLine();
	// 								string[] splittedChoice = newLine.Split(':');

	// 							}
	// 					}
	// 				}
	// 			}
	// 			break;
	// 		default:
	// 			return;
	// 	}

	// }

	// void Terminate(){
	// 	activated = false;
	// 	prompting = false;
	// }

	public Text prompt;
	public Text response;
	public Text inputField;
	public Text talkText;
	public static bool activated = false;
	bool prompting = false;
	public GameObject computer;

	string promptStr = "Enter your password:";
	string typedSoFar = "";
	TypedInput ti = new TypedInput();
	int count = 5;
	public static bool inputEnabled = false;

	string[] textResponses = new string[] {"What's my password again?",
										   "How could I forget my pasword?",
										   "What is it? What is it? What is it?",
										   "Did I really forget? How could I be so dumb to forget?"};

	void Start(){
		computer.SetActive(false);
		inputEnabled = false;
	}

	void Update(){
		if (activated && count >= 0){
			computer.SetActive(true);
			inputField.enabled = true;
			prompt.enabled = true;
			response.enabled = true;
			talkText.enabled = true;
			Debug.Log("here");
			prompt.enabled = true;
			prompt.text = promptStr;
			
			TypedInput.TypedText tt = ti.ProcessTyping();

			if (inputEnabled){
				typedSoFar = tt.text;
				inputField.text = typedSoFar;
			}
			
			if (tt.finalized){
				Debug.Log("resetting");
				count--;
				typedSoFar = "";
				inputField.text = "";
				ti.Clear();
				response.text = "Incorrect password!";
				response.text += "\nYou have " + count.ToString();
				if (count == 1){
					response.text += " attempt left.";
				} else {
					response.text += " attempts left.";
				}
			}

			if (count == 0){
				response.text += "\nYou have been locked out of your computer.";
				inputEnabled = false;
			} else if (count > 0 && count < 5){
				talkText.text = textResponses[5-count-1];
			} else if (count < 0){
				Terminate();
			}
			Debug.Log("typed so far:" + inputField.text);


		}
	}


	void Terminate(){
		talkText.text = "";
		ti.Clear();
		activated = false;
		DialogTester.inputInAction = false;
		computer.SetActive(false);
	}


}
