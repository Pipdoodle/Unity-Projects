using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class couch : MonoBehaviour {
	private bool accept;
	private dialogBox diaBox;
	// Use this for initialization
	void Start () {
		accept = false;
		diaBox = FindObjectOfType<dialogBox> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	
	void OnTriggerStay2D(Collider2D other){
     
		//diaBox.ShowBox ("Press 'E'");
		if (Input.GetKeyUp (KeyCode.E)) {
            Debug.Log("in");
            if (!accept)
            {
                accept = true;
                diaBox.ShowBox("Heyo");
            }
            else
            {
                accept = false;
                diaBox.Clear();
            }
		}
		//else diaBox.Clear ();
	}

	/*void OnTriggerStay2D(Collider2D other)
	{
		
		//if (!touched)
		if(accept)
		{
			diaBox.ShowBox ("hello hello");
			accept = false;
		}
	}*/

	/*void OnTriggerExit2D(Collider2D other)
	{
		diaBox.Clear ();
	}*/
}
