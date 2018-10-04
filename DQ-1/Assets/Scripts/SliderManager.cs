using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text.RegularExpressions;
using System.IO;
using System;
public class SliderManager : MonoBehaviour {

	public Slider lmood;
	public Slider smood;
	public string scoreFileName;
	private string scorePath;

    //public Text HPtext;
    //public healthmanager publicheatlh

    // Use this for initialization
    void Start () {
		scorePath = System.IO.Path.Combine(Utility.FILE_PREFIX, scoreFileName.Trim());
		if (!scorePath.EndsWith(".txt")){
			scorePath += ".txt";
		}
	}
	
	// Update is called once per frame
	void Update () {
		lmood.maxValue = 400;
		smood.maxValue = 400;
		UpdateValues ();
	}

	void UpdateValues(){ 
		//TODO: write this
		using (StreamReader scoreSR = new StreamReader(scorePath)){
			while(scoreSR.Peek() >= 0){
				string currLine = scoreSR.ReadLine();
				if (currLine.IndexOf(":") != -1){ 
					string[] nameValPair = currLine.Split(':');
					if (nameValPair.Length != 2){
						//Debug.Log("malformatted line");
					}
					if(nameValPair[0].Equals("smood"))
						{ smood.value = int.Parse(nameValPair[1]);}
					if(nameValPair[0].Equals("lmood"))
					{ lmood.value = int.Parse(nameValPair[1]);}
				}
			}
		}
	}


}
