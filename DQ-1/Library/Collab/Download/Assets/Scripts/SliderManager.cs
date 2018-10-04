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
	public Image lfill;
	public Image sfill;

	private IntAnimStatus sAnim;
	private IntAnimStatus lAnim;
	private int animRate = 100;

	public string scoreFileName;
	private string scorePath;

	//57 4B E3 FF = base purple
	public Color baseColor = new Color(	((float)0x57)/0xFF, 
									 	((float)0x4B)/0xFF, 
									 	((float)0xE3)/0xFF, 
									 	((float)0xFF)/0xFF);

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
		lmood.maxValue = 100;
		smood.maxValue = 100;
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
					if(nameValPair[0].Equals("smood")){ 
						int newSmood = int.Parse(nameValPair[1]);
						sAnim = UpdateAnim(smood, sAnim, sfill, newSmood);
						Debug.Log("sfill's color: " + sfill.color);
					} 

					if(nameValPair[0].Equals("lmood")){ 
						int newLmood = int.Parse(nameValPair[1]);
						lAnim = UpdateAnim(lmood, lAnim, lfill, newLmood);
						Debug.Log("lfill's color: " + lfill.color);
					}
				}
			}
		}
	}

	IntAnimStatus UpdateAnim(Slider mood, IntAnimStatus anim, Image fill, int newValue){
		if (anim != null){
			if (anim.isOngoing()){
				Debug.Log("changing anim, step = " + anim.step);
				if(anim.end != newValue){
					anim.renew(newValue);
					Debug.Log("anim renewed");
				} 
				mood.value = anim.next();
				Color endColor;
				if (anim.step > 0){
					endColor = Color.green;
				} else if (anim.step < 0) {
					endColor = Color.red;
				} else {
					endColor = Color.black;
				}
				Debug.Log("step = " + anim.step);
				//fill.color = endColor;
				fill.color = Color.Lerp(endColor, baseColor, anim.getRatioDone());
				Debug.Log("smood:" + (anim == sAnim) + ", fill.color = " + fill.color);
				return anim;
			} else {
				if (anim.end == newValue){
					return anim;
				}
				//otherwise the current value is already reached. do nothing
			}
		}
		Debug.Log("making new anim");
		return new IntAnimStatus((int)Math.Floor(mood.value), newValue, animRate);

	}

}
