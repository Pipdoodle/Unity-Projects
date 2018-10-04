using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiagnoseRoomScript: MonoBehaviour  {
    int x = 0;
    public Sprite background;
    public GameObject obj;
    public GameObject makeroom;
    public Sprite microbackground;
    public GameObject currentbackground;
    public List<PatientClass> patients;
    public Text symptoms;
    public GameObject checkpanel;
    bool virus,bacteria, coccus, spirillum,spirochete,picorna, flavi,herpes=false;
    public string type;
    public string shape;

    private void Start()
    {
        patients = new List<PatientClass>();
        
    }
    public void NextButtonClicker()
    {
        if (patients.Count > 0)
        {
            background = patients[0].getDisease().returnImage();
            currentbackground.GetComponent<SpriteRenderer>().sprite = patients[0].getDisease().returnImage();
            x++;
            setChecklist();
        }
        else
        {
            background = microbackground;
            checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
            checkpanel.GetComponent<ChecklistScript>().patientname = "";
            checkpanel.GetComponent<ChecklistScript>().patientname = "";
            checkpanel.GetComponent<ChecklistScript>().type = "";
            checkpanel.GetComponent<ChecklistScript>().shape = "";
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
    }
    public void DiscardButtonClicker()
    {
       
        background = microbackground;
        currentbackground.GetComponent<SpriteRenderer>().sprite = microbackground;
        patients[0].setShape(shape);
        patients[0].setType(type);
        makeroom.GetComponent<MakingRoomScript>().patients.Add(patients[0]);
        checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
        checkpanel.GetComponent<ChecklistScript>().patientname = "";
        checkpanel.GetComponent<ChecklistScript>().allsymptoms = "";
        checkpanel.GetComponent<ChecklistScript>().type = "";
        checkpanel.GetComponent<ChecklistScript>().shape = "";


        checkpanel.GetComponent<ChecklistScript>().setChecklist();
        patients.RemoveAt(0);
    }
    public void setChecklist()
    {
        checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
        checkpanel.GetComponent<ChecklistScript>().patientname = patients[0].returnName();
        string symptomcreater = "";
        for (int i = 0; i < patients[0].getDisease().getSymptoms().Count; i++)
        {
            symptomcreater = symptomcreater + patients[0].getDisease().getSymptoms()[i] + " \n ";
        }
        checkpanel.GetComponent<ChecklistScript>().allsymptoms = symptomcreater;
        checkpanel.GetComponent<ChecklistScript>().setChecklist();
    }
    public void onVirusClicked()
    {
        print("Toggled");
        virus = !virus;
        if (virus == true)
        {
            type = ("virus");
            //if (patients[0]!=null)
            //patients[0].setType(type);
            checkpanel.GetComponent<ChecklistScript>().type = type;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();

        }
  
    }
    public void onBacteriaClicked()
    {
        print("Toggled");
        bacteria = !bacteria;
        if (bacteria == true)
        {
            type = ("bacteria");
            //if (patients[0] != null)
              //  patients[0].setType(type);
            checkpanel.GetComponent<ChecklistScript>().type = type;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();

        }
       
    }
    public void onCoccusClicked()
    {
        coccus = !coccus;
        if (coccus == true)
        {
            shape = ("coccus");
            //if (patients[0] != null)
              //  patients[0].setType(shape);
            checkpanel.GetComponent<ChecklistScript>().shape = shape;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();

        }
        
    }
    public void onSpirillumClicked()
    {
        spirillum = !spirillum;
        if (spirillum == true)
        {
            shape = ("spirillum");
            //if (patients[0] != null)
             //   patients[0].setType(shape);
            checkpanel.GetComponent<ChecklistScript>().shape = shape;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
       
    }
    public void onSpirocheteClicked()
    {
        spirochete = !spirochete;
        if (spirochete == true)
        {
            shape = ("spirochete");
           // if (patients.Count>0)
              //  patients[0].setType(shape);
            checkpanel.GetComponent<ChecklistScript>().shape = shape;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
       
    }
    public void onPicornaClicked()
    {
        picorna = !picorna;
        if (bacteria == true)
        {
            shape = ("picorna");
            //if (patients.Count > 0)
              //  patients[0].setType(shape);
            checkpanel.GetComponent<ChecklistScript>().shape = shape;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
     
    }
    public void onFlaviClicked()
    {
        flavi = !flavi;
        if (flavi == true)
        {
            shape = ("flavi");
            //if (patients.Count > 0)
              //  patients[0].setType(shape);
            checkpanel.GetComponent<ChecklistScript>().shape = shape;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
        
    }
    public void onHerpesClicked()
    {
        herpes = !herpes;
        if (herpes == true)
        {
            shape = ("herpes");
            //if (patients.Count > 0)
                //patients[0].setType(shape);
            checkpanel.GetComponent<ChecklistScript>().shape = shape;
            checkpanel.GetComponent<ChecklistScript>().setChecklist();
        }
    
    }
   
}
