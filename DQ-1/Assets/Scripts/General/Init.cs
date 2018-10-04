using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class Init : MonoBehaviour {

	public static bool initing = true;
	// Use this for initialization
	void Start () {
		if (initing){
			InitIt();
		}
	}

	public static void InitIt(){
		List<string> lines = new List<string>();
		using (StreamReader sr = new StreamReader(Utility.SCORES_FILE)){
			while (sr.Peek() >= 0){
				string currLine = sr.ReadLine();
				if (currLine.ToLower().StartsWith("smood:")){
					currLine = "smood:50";
				}
				if (currLine.ToLower().StartsWith("clothes:")){
					currLine = "clothes:0";
				}
				if (currLine.ToLower().StartsWith("destroyLetter:")){
					currLine = "destroyLetter:0";
				}
				if (currLine.StartsWith("dayOneCheckedComputer:")){
					currLine = "dayOneCheckedComputer:0";
				}
				if (currLine.StartsWith("dayOneBreakfast:")){
					currLine = "dayOneBreakfast:0";
				}
				lines.Add(currLine);
			}
		}

		using(StreamWriter sw = new StreamWriter(Utility.SCORES_FILE, false)){
			foreach(string line in lines){
				Debug.Log("writing " + line);
				sw.WriteLine(line);
			}
		}

		DialogTester.dialogFileName = "introduction.txt";
		DialogTester.scoreFileName = "scores.txt";
		initing = false;

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
