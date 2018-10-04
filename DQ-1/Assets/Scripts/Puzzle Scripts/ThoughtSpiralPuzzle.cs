using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThoughtSpiralPuzzle : MonoBehaviour {

	static bool isWinnable = false;
	public static bool puzzleStarted;
	static bool startingPuzzle;
	public string puzzleFile;
	public Text txtPrefab;
	static List<string> texts = new List<string>();
	static Dictionary<string, List<Text>> textToObj = new Dictionary<string,List<Text>>();
	static string[] specialCodes = new string[] {"jumping to conclusion","magic", "DAG", "couch"};
	int textCount = 0;
	static int waitFrames;
	int waitFramesLeft = 0;
	int maximumFont = 30;
	Text currMatch;
	TypedInput input = new TypedInput();
	
	string typedSoFar = "";
	static string[] genericTexts = new string[] {"Useless", "Why can't I even do this?",
										  "I'm not good enough", "Piece of s",
										  "I hate myself"};

	// Use this for initialization
	void Start () {

		//leftmost = 70
		//top = 480
		//bottom = 200
		//rightmost = 900

	}

	public static void Initialize(string fileName, bool winnable){
		string path = System.IO.Path.Combine(Utility.FILE_PREFIX, fileName.Trim());
		texts.Clear();
		using (StreamReader sr = new StreamReader(path)){
			while (sr.Peek() >= 0){
				texts.Add(sr.ReadLine());	
			}
		}

		isWinnable = winnable;
		if (isWinnable){
			waitFrames = 500;
		} else {
			waitFrames = 500;
		}
		puzzleStarted = true;
		startingPuzzle = true;
		Debug.Log("initialized!!");
	}

	// Update is called once per frame
	void Update () {
		if (puzzleStarted && Input.GetKeyDown(KeyCode.Escape)){
			TerminatePuzzle();
			DialogTester.puzzling = false;
		}
		else if (puzzleStarted){
			waitFramesLeft--;
			if(PuzzleNotCleared() && textCount < 500 && waitFramesLeft <= 0){
				//Debug.Log("puzzle ongoing!");
				startingPuzzle = false;
				modifyVars();
				Text a = Object.Instantiate(txtPrefab);
				if (a != null){
					if (a.transform != null){
						//Debug.Log("transform isn't null");
						a.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
						float xPos = UnityEngine.Random.Range(70.0f, 850.0f);
						float yPos = UnityEngine.Random.Range(200.0f, 480.0f);
						a.transform.SetPositionAndRotation(new Vector3(xPos, yPos), Quaternion.identity);
						string thisText;
						if (textCount < texts.Count){
							thisText = texts[textCount];
						} else {
							int index = new System.Random().Next(0,genericTexts.Length-1);
							thisText = genericTexts[index];
						}
						if (!textToObj.ContainsKey(thisText)){
							textToObj[thisText] = new List<Text>();
						}
						a.text = thisText;
						textToObj[thisText].Add(a);

						if (textCount < 20){
							a.fontSize = new System.Random().Next(14,maximumFont);
						} else {
							a.fontSize = new System.Random().Next(30,50);
						}
						//Debug.Log(a.fontSize.ToString() + " at pos = " + xPos.ToString() + ", " + yPos.ToString());
					// } else {
					// 	Debug.Log("transform is null");
					}
				}
			} else if (! PuzzleNotCleared()) {
				//puzzle cleared
				Debug.Log("puzzle cleared!");
				TerminatePuzzle();
			} else if (PuzzleNotCleared() && waitFrames == 0 && textCount >= 500){
				Debug.Log("you lost!");
				typedSoFar = "";
				DialogTester.failedPuzzle = true;
			}

			if (textCount < 100){
				processTyping();

			}
		} 

	}


	bool PuzzleNotCleared() {
		return startingPuzzle || (puzzleStarted && DialogTester.puzzling && (textToObj.Count > 0));
	}

	void TerminatePuzzle(){
		foreach(List<Text> textList in textToObj.Values){
			foreach(Text elem in textList){
				Destroy(elem);
			}
			textList.Clear();
		}
		typedSoFar = "";
		textToObj.Clear();
		puzzleStarted = false;
		DialogTester.puzzling = false;
	}


	string ShiftedText(string original){
		return original.Replace("/", "?").Replace("1", "!");
	}

	void RemoveHighlights(Text txt){
		if (txt == null){
			return;
		}
		txt.text = txt.text.Replace("<color=red>", "").Replace("</color>", "");

	}

	void SearchText(){
		string shifted = ShiftedText(typedSoFar);
		foreach(string txt in textToObj.Keys){
			string formattedT = txt.Trim().ToLower();
			if (formattedT.StartsWith(typedSoFar)){
				int startIndex = formattedT.IndexOf(typedSoFar);
				int endIndex = startIndex + typedSoFar.Length;
				if (textToObj.Count > 0){
					Text obj = textToObj[txt][0];
					RemoveHighlights(obj);
					obj.text = obj.text.Substring(0, startIndex) + "<color=red>"
							   + obj.text.Substring(startIndex, typedSoFar.Length)
							   + "</color>" + obj.text.Substring(endIndex);
					if (obj != currMatch){
						RemoveHighlights(currMatch);
						currMatch = obj;
					}
					return;
				}
			} else if (formattedT.StartsWith(shifted)){
				int startIndex = formattedT.IndexOf(shifted);
				int endIndex = startIndex + shifted.Length;
				if (textToObj.Count > 0){
					Text obj = textToObj[txt][0];
					RemoveHighlights(obj);
					obj.text = obj.text.Substring(0, startIndex) + "<color=red>"
							   + obj.text.Substring(startIndex, shifted.Length)
							   + "</color>" + obj.text.Substring(endIndex);
					if (obj != currMatch){
						RemoveHighlights(currMatch);
						currMatch = obj;
					}
				}

				return;
			}
		}
		RemoveHighlights(currMatch);

	}


	void FinalizeText(){
		Debug.Log(typedSoFar + " typed!!");
		string shifted = ShiftedText(typedSoFar);
		foreach(string code in specialCodes){
			string formattedCode = code.Trim().ToLower();
			if (typedSoFar.Equals(formattedCode)){
				TerminatePuzzle();
				DialogTester.puzzling = false;
			}
		}
		foreach(string txt in textToObj.Keys){
			string formattedT = txt.Trim().ToLower();
			if (formattedT.Equals(typedSoFar)
				|| formattedT.Equals(shifted)){
				if (textToObj[txt].Count > 0){
					Text obj = textToObj[txt][0];
					if (obj != null){
						Destroy(obj);
						textToObj[txt].RemoveAt(0);
						Debug.Log("object destroyed!");
					}
					if (textToObj[txt].Count <= 0){
						textToObj.Remove(txt);
					}

					typedSoFar = "";
					input.Clear();
					return;
				}
			}
		}
		typedSoFar = "";
	}

	void processTyping(){
		TypedInput.TypedText t = input.ProcessTyping();
		typedSoFar = t.text;
		if (t.finalized){
			FinalizeText();
		} else {
			SearchText();
		}

		// if (Input.GetKeyDown(KeyCode.Backspace)) {
		// 	if (!typedSoFar.Equals("")){
		// 		typedSoFar = typedSoFar.Substring(0, typedSoFar.Length -1);
		// 		Debug.Log(typedSoFar);
		// 		SearchText();
		// 	}
		// } else if (Input.GetKeyDown(KeyCode.Return)) {
		// 	FinalizeText();
		// } else {
		// 	foreach(KeyCode kcode in keycodes) {
		// 		if (Input.GetKeyDown(kcode)) {
		// 			if (kcode.ToString().Length > 1){
		// 				typedSoFar += keyToString[kcode];
		// 			} else {
		// 				typedSoFar += kcode.ToString().ToLower();
		// 			}
		// 			Debug.Log(typedSoFar);
		// 			SearchText();
		// 		}
		// 	}
		// }

	}


	void modifyVars(){
		textCount++;
		maximumFont++;
		if (maximumFont >= 50){
			maximumFont = 50;
		}

		waitFrames -= 5;
		if (waitFrames < 0){
			waitFrames = 0;
		}
		waitFramesLeft = waitFrames;

	}


}
