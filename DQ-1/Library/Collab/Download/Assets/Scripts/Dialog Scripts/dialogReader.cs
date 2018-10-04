using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.IO;
using System;


public class DialogReader {

	public StreamReader sr;
	string dialogFile;
	static string scoreFile;
	string tag = "";
	int currLine = 0;
	bool selectMode = false;
	bool previouslyActive = false;
	public static List<string> varNames = new List<string>();
	public static Dictionary<string,int> variables = new Dictionary<string,int>();
	static string[] actionOptions = new string[] {"Text", "Select", "COUCH", "Puzzle"};

	bool sendingInfo = false;
	string infoTarget = "";

	Cereal c;

	public DialogReader(string dialogPath, string scorePath){
		Debug.Log("dialog:" + dialogPath);
		Debug.Log("score:" + scorePath);
		if (File.Exists(dialogPath)){
			dialogFile = dialogPath;
			sr = new StreamReader(dialogFile);
		} else {
			Debug.Log("dialog file" + dialogPath + "doesn't exist");
		}

		if (File.Exists(scorePath)){
			scoreFile = scorePath;
		} else {
			Debug.Log("score file" + scorePath + "doesn't exist");
		}
	}

	public string GetDialogFileName(){
		return dialogFile.Trim();
	}

	public bool CheckDialogFileName(string other){
		string processedOther = other.Replace(".txt", "").Trim();
		string[] splitForward = (dialogFile).Split('/');
		List<string[]> splitTwice = new List<string[]>();
		for (int i=0; i<splitForward.Length; i++){
   			splitTwice.Add(splitForward[i].Split('\\'));
		}
		int lastRow = splitTwice.Count - 1;
		int lastCol = splitTwice[lastRow].Length - 1;
		string target = splitTwice[lastRow][lastCol].Replace(".txt", "");
		//Debug.Log("other: " + processedOther + ", target: " + target);
		return processedOther.Replace(".txt","").Equals(target);

	}

	public bool Begin(string tagName){
		if(previouslyActive){
			Terminate();
		} else {
			//Debug.Log("initing values");
			if (variables.Count <= 0){
				GetInitValues();
			} else {
				variables.Clear();
				varNames.Clear();
				GetInitValues();
			}		
		}


		Debug.Log("variables now have " + variables.Count);
		Scene currScene = SceneManager.GetActiveScene();
		if (currScene != null){
			string sceneName = currScene.name;
			dialogFile = Utility.PrefixFile(Utility.sceneToFile[sceneName]);
		}
		sr = new StreamReader(dialogFile);
		previouslyActive = true;
		while (sr.Peek() >= 0){
			string curr = sr.ReadLine().Trim();
			if (curr.StartsWith("Tag:")){
				int clnI = curr.IndexOf(':');
				string currTg = curr.Substring(clnI+1).Trim().ToLower();
				if (currTg == tagName.ToLower()){
					return true;
				} else {
					SkipToNearest("EndTag");
				}
			}
		}
		//Debug.Log("can't find "+ tagName);
		return false;
	}

	public void PrintDialog(string dialogFile){
		using (StreamReader sr = new StreamReader(dialogFile)) {
			while (sr.Peek() >= 0){
				Debug.Log(sr.ReadLine());
			}
			Debug.Log("finished reading");
		}
	}

	public static string[] ParseBrackets(string fullText){
		if (fullText.Trim().StartsWith("[")){
			string[] result = new string[2];
			int speakerStartIndex = fullText.IndexOf("[");
			int speakerEndIndex = fullText.IndexOf("]");
			int len = speakerEndIndex - speakerStartIndex -1;

			result[0] = fullText.Substring(speakerStartIndex+1, len).Trim();
			result[1] = fullText.Substring(speakerEndIndex+1).Trim();
			return result;
		} else { // no speaker tag
			return new string[] {"", fullText.Trim()};
		}
	}

	public void Jump(string title){ //rewrite to jump independent of curr line
		Terminate();
		sr = new StreamReader(dialogFile);
		SendInfo(title.Trim());

		string target = "Jump:"+title;
		int lineCount = 0;
			while (sr.Peek() >= 0){
				lineCount++;
				string currLineStr = sr.ReadLine();
				if (currLineStr.Trim().Equals(target)){
					currLine = lineCount;
					//Debug.Log("Jumped to" + title);
					return;
				}
			}
			//Debug.Log("Jump:" + title + " not found.");
	}

	void SkipToNearest(string keyword){
		string currLineStr = sr.ReadLine();
		if (currLineStr == null){
			return;
		}
		currLine++;
		if (!currLineStr.Trim().Equals(keyword)){
			SkipToNearest(keyword);
		} else {
			return;
		}

	}

	void SkipToEndJump(){
		SkipToNearest("EndJump");
	}

	public Dictionary<string, string> GetSelectChoices(int numChoices){
		Dictionary<string,string> result = new Dictionary<string, string>();
		for (int i=0; i<numChoices; i++){
			string currLineStr = sr.ReadLine();
			string choiceText;
			string jumpName;
			if (currLineStr.Trim().StartsWith(i.ToString() + ":")) {
				int textStartLoc = currLineStr.IndexOf(":");
				choiceText = currLineStr.Substring(textStartLoc+1);

				string nextLineStr = sr.ReadLine();
				int nameStartLoc = nextLineStr.IndexOf(":");
				jumpName = nextLineStr.Substring(nameStartLoc+1);

				result.Add(choiceText, jumpName);
				currLine += 2;
			} else {
				//Debug.Log("malformatted choices");
				return null;
			}
		}
		return result;
	}

	public bool EvalBoolStr(string expr){
		if (expr.Trim().ToLower().Equals("default")){
			return false;
		}

		string[] splitted = expr.Split(';');
		bool result = false;
		string pending = "";
		for (int i=0; i<splitted.Length; i++){
			string currPart = splitted[i].Trim();
			bool curr = false;
			if (currPart.IndexOf("(") != -1 && currPart.IndexOf(")") != -1){
				//is expression
				int nameEnd = currPart.IndexOf("(");
				int exEnd = currPart.IndexOf(")");
				string varName = currPart.Substring(0, nameEnd).Trim();
				//Debug.Log(varName);
				string ex = currPart.Substring(nameEnd+1, exEnd-nameEnd-1).Trim();
				try {
					if (ex.StartsWith(">=")){
						int index = ex.IndexOf("=");
						string num = ex.Substring(index+1).Trim();
						int n = int.Parse(num);
						curr = (variables[varName] >= n);
					} else if (ex.StartsWith("<=")){
						int index = ex.IndexOf("=");
						string num = ex.Substring(index+1).Trim();
						int n = int.Parse(num);
						curr = (variables[varName] <= n);
					} else if (ex.StartsWith("==")){
						int index = ex.IndexOf("=");
						string num = ex.Substring(index+2).Trim();
						int n = int.Parse(num);
						curr = (variables[varName] == n);
					} else if (ex.StartsWith(">")){
						int index = ex.IndexOf(">");
						string num = ex.Substring(index+1).Trim();
						int n = int.Parse(num);
						curr = (variables[varName] > n);
					} else if (ex.StartsWith("<")){
						int index = ex.IndexOf("<");
						string num = ex.Substring(index+1).Trim();
						int n = int.Parse(num);
						curr = (variables[varName] < n);
					} else {
						//Debug.Log("!!!expression unknown: " + ex);
						return false;
					}
				} catch (KeyNotFoundException e){
					//Debug.Log("not found " + varName);
				}

			} else if (currPart.Equals("or")){ 
				//is and/or
				pending = currPart;
				if (result == true){
					return true;
				} else {
					continue;
				}
			} else if (currPart.Equals("and")){
				pending = currPart;
				if (result == false){
					return false;
				} else {
					continue;
				}

			} else {
				//Debug.Log("!!!expression unknown: " + splitted[i]);
				return false;
			}
			if (i == 0){
				result = curr;
			} else {
				if (pending != ""){
					switch (pending){
						case "and": 
							result = result && curr;
							break;
						case "or":
							result = result || curr;
							break;
					}
					pending = "";
				} else {
					//Debug.Log("malformed expression " + expr);
				}
			}
		}
		return result;


	}

	string GetTextAfterColon(string currLineStr){
		int nameStartLoc = currLineStr.IndexOf(":");
		string text = currLineStr.Substring(nameStartLoc+1).Trim();
		while (text.EndsWith("\\")){
			text = text.Substring(0, text.Length-1);
			text += "\n" + sr.ReadLine().Trim();
		}
		return text;
	}

	int repeatsLeft = 0;
	string[] repeatStore;
	public string[] NextAction(){
		if(variables.Count == 0 || varNames.Count == 0){
			GetInitValues();
			//Debug.Log("inited");
		}

		if (repeatsLeft > 0 && repeatStore != null){
			//Debug.Log("repeat"+repeatsLeft);
			repeatsLeft--;
			return repeatStore;
		}

		string currLineStr = sr.ReadLine();
		currLine++;
		if (currLineStr == null){
			return new string[] {"End", ""};
		} 

		//for initialization
		currLineStr = currLineStr.Trim();
		if (currLineStr.StartsWith("dVar:")){
			int startLoc = currLineStr.IndexOf(":");
			string dVarsStr = currLineStr.Substring(startLoc+1).Trim();
			string[] dVarsArr = dVarsStr.Split(';');

			foreach(string dVar in dVarsArr){
				int openI = dVar.IndexOf("(");
				int closeI = dVar.IndexOf(")");
				if (openI == -1 || closeI == -1){
					//Debug.Log("malformatted dVar at " + currLine.ToString());
					return NextAction();
				}
				string varName = dVar.Substring(0,openI).Trim();
				string changeS = dVar.Substring(openI+1,closeI-openI-1).Trim();
				int change = int.Parse(changeS);
				//Debug.Log(varName);
				variables[varName] += change;
			}
			WriteScores(varNames, variables);
			return NextAction();
			
		} else if (currLineStr.StartsWith("Jump:")){
			SkipToEndJump();
			return NextAction();
		} else if (currLineStr.StartsWith("JumpTo:")){
			int nameStartLoc = currLineStr.IndexOf(":");
			string name = currLineStr.Substring(nameStartLoc+1).Trim();
			Jump(name);
			return NextAction();
		} else if (currLineStr.StartsWith("ScoreCase:")){
			int nameStartLoc = currLineStr.IndexOf(":");
			string exp = currLineStr.Substring(nameStartLoc+1).Trim();
			bool evaluated = EvalBoolStr(exp);
			if (evaluated){
				SkipToNearest("Then:");
				return NextAction();
			} else {
				SkipToNearest("Else:");
				return NextAction();
			}
		} else if (currLineStr.StartsWith("EndThen")
					|| currLineStr.StartsWith("EndElse")){
			SkipToNearest("EndScoreCase");
			return NextAction();
		} else if (currLineStr.StartsWith("EndTag")){
			SendInfo("End");
			return new string[] {"EndTag", ""};
		} else if (currLineStr.StartsWith("Repeat")){
			repeatStore = NextAction();
			int repeatsLeft = Int32.Parse(GetTextAfterColon(currLineStr))-1;
			//Debug.Log("repeatsLeft="+repeatsLeft);
			return repeatStore;
		} else if (currLineStr.StartsWith("ScoreSwitch")){
			//Debug.Log("score switched!");
			bool jumped = false;
			string defaultJump = "";
			Dictionary<string,string> scoreChoices = GetSelectChoices(Int32.Parse(GetTextAfterColon(currLineStr)));
			foreach(KeyValuePair<string,string> kv in scoreChoices){
				string exp = kv.Key;
				string jumpName = kv.Value;
				if (exp.Trim().ToLower().Equals("Default")){
					defaultJump = jumpName;
				} else if (EvalBoolStr(exp)){
					jumped = true;
					Jump(jumpName);
					break;
				}
			}
			if (!jumped && !defaultJump.Equals("")){
				Jump(defaultJump);
			}
			return NextAction();
		} foreach(string key in actionOptions){
			if (currLineStr.StartsWith(key + ":")) {
				int nameStartLoc = currLineStr.IndexOf(":");
				string text = currLineStr.Substring(nameStartLoc+1).Trim();
				while (text.EndsWith("\\")){
					text = text.Substring(0, text.Length-1);
					text += "\n" + sr.ReadLine().Trim();
				}
				bool isWaitText = false;
				Regex wait = new Regex(@"\[wait=\d+\]$");
				while (wait.Match(text).Success){
					isWaitText = true;
					string add = sr.ReadLine().Trim();
					text += "\n" + add;
				}
				if (isWaitText){
					return new string[] {"WaitText", text};
				} else {			
					return new string[] {key,text};
				}
			}
		}
		return NextAction();			
	}



	public void GetInitValues(){ 
		//Debug.Log("scoreFile is " + scoreFile);
		using (StreamReader scoreSR = new StreamReader(scoreFile)){
			while(scoreSR.Peek() >= 0){
				string currLine = scoreSR.ReadLine();
				if (currLine.IndexOf(":") != -1){ 
					string[] nameValPair = currLine.Split(':');
					if (nameValPair.Length != 2){
						//Debug.Log("malformatted line");
					}
					varNames.Add(nameValPair[0]);
					int val = int.Parse(nameValPair[1]);
					//Debug.Log(nameValPair[0] + ":" + nameValPair[1]);
					variables.Add(nameValPair[0],val);
				}
			}
		}
	}

	public void SetInfoTarget(string target, Cereal cer){
		sendingInfo = true;
		infoTarget = target;
		c = cer;
		c.Begin(this);
		c.dr = this;
		//Debug.Log("is dr is null: " + (c.dr == null));
		//Debug.Log("set info target");
	}

	public void DisableInfo(){
		sendingInfo = false;
		infoTarget = "";
		//if (c != null) c.dr = null;
		//Debug.Log("disable info");
	}

	void SendInfo(string info){
		if (sendingInfo && !infoTarget.Equals("")){
			switch(infoTarget){
				case "cereal":
					if (c != null){
						c.SetImage(info);
					}
					//Debug.Log("sending " + info + " to cereal");
					break;
				default:
					//Debug.Log("unknown: sending " + info + " to " + infoTarget);
					return;
			}
		}
	}


	public static void WriteScores(List<string> varNames, Dictionary<string,int> varDict){
		if (varNames.Count == 0 || varDict.Count == 0){
			//Debug.Log("0 variables to write");
			return;
		}
		using (StreamWriter scoreWR = new StreamWriter(scoreFile, false)){
			foreach(string scoreName in varNames){
				int scoreNum = varDict[scoreName];

				scoreWR.WriteLine(scoreName + ":" + scoreNum);
				//Debug.Log(scoreName + ":" + scoreNum);
			}
		}
	}

	public void Terminate(){
		//Debug.Log("terminating");
		if (sr != null) sr.Close();
		WriteScores(varNames,variables);
	}





}
