using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuScript : MonoBehaviour {
    public Canvas menu;
    public Canvas game;
    public GameObject obj;
    public AudioSource backgroundmusic;
    public AudioSource sadmusic;
    public GameObject person;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void quitGame()
    {
        Application.Quit();
    }
    public void returnToGame()
    {
        sadmusic.Stop();
        menu.enabled = false;
        game.enabled = true;
        obj.GetComponent<GameControlScript>().pause = false;
        for (int i = 0; i < obj.GetComponent<GameControlScript>().patientlist.Count; i++)
        {
            obj.GetComponent<GameControlScript>().patientlist[i].setPause(false);
            if (obj.GetComponent<GameControlScript>().patientlist[i].returnActive())
                obj.GetComponent<GameControlScript>().patientlist[i].returnPerson().GetComponent<Animator>().speed = 1;
            if (obj.GetComponent<GameControlScript>().patientlist[i].returnPerson() != null)
                obj.GetComponent<GameControlScript>().patientlist[i].returnPerson().GetComponent<PatientScript>().pause = false;
        }
        backgroundmusic.Play();
        person.GetComponent<Animator>().speed=1;
    }
}
