using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuPanelScript : MonoBehaviour {
    public Canvas menu;
    public Canvas game;
    public GameObject background;
    public GameObject foreground;
    public GameObject midground;
    public Sprite normalbackground;
    public Canvas diagnosecanvas;
    public Canvas makecanvas;
    public Canvas prescribecanvas;
    public Canvas waitcanvas;
    public GameObject obj;
    public GameObject cam;
    public string room;
    public List<Sprite> allbackground;
    public GameObject checkpanel;
    public AudioSource backgroundmusic;
    public AudioSource sadmusic;
    public GameObject person;
    public Canvas ending;
    public Text end;
    public Scene Menu;
    
	// Use this for initialization
	void Start () {
        game.enabled = true;
        menu.enabled = false;
        diagnosecanvas.enabled = false;
        makecanvas.enabled = false;
        prescribecanvas.enabled = false;
        waitcanvas.enabled = true;
        room = "wait";
        ending.enabled = false;
    }
    private void Update()
    {
       if ( cam.GetComponent<GameControlScript>().game!=1)
        {
            game.enabled = true;
            menu.enabled = false;
            diagnosecanvas.enabled = false;
            makecanvas.enabled = false;
            prescribecanvas.enabled = false;
            waitcanvas.enabled = false;
            ending.enabled = true;
            if (cam.GetComponent<GameControlScript>().game == 2)
            {
                end.text = "You have failed. \n Too many patients have died. \n Mama's Panacea is now closed.";
                backgroundmusic.Stop();
                sadmusic.Play();
                

            }
            else
            {
                end.text = "You have proven your capabilities! \n Mama's Panacea will live on!";
            }
        }
    }

    public void openWait()
    {
        float scalex = 3.0f;
        float scaley = 1.67f;
        waitcanvas.enabled = true;
        diagnosecanvas.enabled = false;
        makecanvas.enabled = false;
        prescribecanvas.enabled = false;
        ending.enabled = false;
        background.GetComponent<SpriteRenderer>().sprite = allbackground[0];
        background.transform.localScale = new Vector2(scalex, scaley);
        midground.SetActive(true);
        foreground.SetActive(true);
        room = "wait";
        

        setChecklist();
    }
    public void openDiagnose()
    {
        waitcanvas.enabled = false;
        diagnosecanvas.enabled = true;
        makecanvas.enabled = false;
        prescribecanvas.enabled = false;
        ending.enabled = false;
        background.GetComponent<SpriteRenderer>().sprite = obj.GetComponent<DiagnoseRoomScript>().background;
        float scalex = 2.5f;
        float scaley = 1.8f;
        foreground.SetActive(false);
        midground.SetActive(false);
        background.transform.localScale = new Vector2(scalex, scaley);
        room = "diagnose";
        
        setChecklist();
        if (diagnosecanvas.GetComponent<DiagnoseRoomScript>().patients.Count > 0)
        { 
            checkpanel.GetComponent<ChecklistScript>().patientname = diagnosecanvas.GetComponent<DiagnoseRoomScript>().patients[0].returnName();
            string symptomcreater = "";
            for (int i = 0; i < diagnosecanvas.GetComponent<DiagnoseRoomScript>().patients[0].getDisease().getSymptoms().Count; i++)
            {
                symptomcreater = symptomcreater + diagnosecanvas.GetComponent<DiagnoseRoomScript>().patients[0].getDisease().getSymptoms()[i] + " \n ";
            }
            checkpanel.GetComponent<ChecklistScript>().allsymptoms = symptomcreater;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }


    }
    public void openCreate()
    {
        float scalex = 3.0f;
        float scaley = 1.67f;
        background.GetComponent<SpriteRenderer>().sprite = allbackground[1];
        background.transform.localScale = new Vector2(scalex, scaley);
        waitcanvas.enabled = false;
        diagnosecanvas.enabled = false;
        makecanvas.enabled = true;
        prescribecanvas.enabled = false;
        ending.enabled = false;
        foreground.SetActive(false);
        midground.SetActive(false);
        room = "create";   
        setChecklist();
        if (makecanvas.GetComponent<MakingRoomScript>().patients.Count > 0)
        {
            checkpanel.GetComponent<ChecklistScript>().patientname = makecanvas.GetComponent<MakingRoomScript>().patients[0].returnName();
            string symptomcreater = "";
            for (int i = 0; i < makecanvas.GetComponent<MakingRoomScript>().patients[0].getDisease().getSymptoms().Count; i++)
            {
                symptomcreater = symptomcreater + makecanvas.GetComponent<MakingRoomScript>().patients[0].getDisease().getSymptoms()[i] + " \n ";
            }
            checkpanel.GetComponent<ChecklistScript>().allsymptoms = symptomcreater;
          
            checkpanel.GetComponent<ChecklistScript>().type = makecanvas.GetComponent<MakingRoomScript>().patients[0].getType();
            checkpanel.GetComponent<ChecklistScript>().shape = makecanvas.GetComponent<MakingRoomScript>().patients[0].getShape();
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
    }
    public void openLabel()
    {
        float scalex = 3.0f;
        float scaley = 1.67f;
        background.GetComponent<SpriteRenderer>().sprite = allbackground[2];
        background.transform.localScale = new Vector2(scalex, scaley);
        waitcanvas.enabled = false;
        diagnosecanvas.enabled = false;
        makecanvas.enabled = false;
        ending.enabled = false;
        prescribecanvas.enabled = true;
        foreground.SetActive(false);
        midground.SetActive(false);
        room = "label";
        
        setChecklist();
        if (prescribecanvas.GetComponent<SendRoomScript>().patients.Count > 0)
        {
            checkpanel.GetComponent<ChecklistScript>().patientname = prescribecanvas.GetComponent<SendRoomScript>().patients[0].returnName();
            string symptomcreater = "";
            for (int i = 0; i < prescribecanvas.GetComponent<SendRoomScript>().patients[0].getDisease().getSymptoms().Count; i++)
            {
                symptomcreater = symptomcreater + prescribecanvas.GetComponent<SendRoomScript>().patients[0].getDisease().getSymptoms()[i] + " \n ";
            }
            checkpanel.GetComponent<ChecklistScript>().allsymptoms = symptomcreater;
            checkpanel.GetComponent<ChecklistScript>().type = prescribecanvas.GetComponent<SendRoomScript>().patients[0].getType();
            checkpanel.GetComponent<ChecklistScript>().shape = prescribecanvas.GetComponent<SendRoomScript>().patients[0].getShape();
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
    }
    public void openMenu()
    {
        backgroundmusic.Pause();
        sadmusic.Play();
        menu.enabled = true;
        game.enabled = false;
        ending.enabled = false;
        cam.GetComponent<GameControlScript>().pause = true;
        for (int i=0;i<cam.GetComponent<GameControlScript>().patientlist.Count;i++)
        {
            cam.GetComponent<GameControlScript>().patientlist[i].setPause(true);
            if (cam.GetComponent<GameControlScript>().patientlist[i].returnActive())
                cam.GetComponent<GameControlScript>().patientlist[i].returnPerson().GetComponent<Animator>().speed = 0;
            if (cam.GetComponent<GameControlScript>().patientlist[i].returnPerson()!=null)
            cam.GetComponent<GameControlScript>().patientlist[i].returnPerson().GetComponent<PatientScript>().pause = true;
        }
        person.GetComponent<Animator>().speed=0;
    }
    public void setChecklist()
    {
        checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
        checkpanel.GetComponent<ChecklistScript>().type = "";
        checkpanel.GetComponent<ChecklistScript>().shape = "";
        checkpanel.GetComponent<ChecklistScript>().patientname = "";
        checkpanel.GetComponent<ChecklistScript>().allsymptoms = "";
        checkpanel.GetComponent<ChecklistScript>().setChecklist();
    }
    public void endClick()
    {
        SceneManager.LoadScene("Menu");
        print("Yas");
    }
}
