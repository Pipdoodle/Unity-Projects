using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhoneScoreChange : MonoBehaviour {

	public void changeScoresss()
	{
		DialogReader.variables["smood"] += 40;
		DialogReader.WriteScores(DialogReader.varNames, DialogReader.variables);
	}
}