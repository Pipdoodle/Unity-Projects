using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakingRoomScript : MonoBehaviour {
    public List<string> treatmentcreated;
    public List<PatientClass> patients;
    public GameObject obj;
    public int x;
    public GameObject sendroom;
    public List<Image> takenitem;

    public GameObject checkpanel;
	// Use this for initialization
	void Start () {
     
        patients = new List<PatientClass>();
        treatmentcreated = new List<string>();
        for (int i=0;i<takenitem.Count;i++)
        {
            takenitem[i].enabled = false;
        }
    }

    
    public void setChecklist()
    {
        checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
        checkpanel.GetComponent<ChecklistScript>().patientname = "";
        checkpanel.GetComponent<ChecklistScript>().allsymptoms = "";
        checkpanel.GetComponent<ChecklistScript>().type = "";
        checkpanel.GetComponent<ChecklistScript>().shape = "";
        checkpanel.GetComponent<ChecklistScript>().patientname = patients[0].returnName();
        string symptomcreater = "";
        for (int i = 0; i < patients[0].getDisease().getSymptoms().Count; i++)
        {
            symptomcreater = symptomcreater + patients[0].getDisease().getSymptoms()[i] + "\n ";
        }
        checkpanel.GetComponent<ChecklistScript>().allsymptoms = symptomcreater;
        checkpanel.GetComponent<ChecklistScript>().type = patients[0].getType();
        checkpanel.GetComponent<ChecklistScript>().shape = patients[0].getShape();
        checkpanel.GetComponent<ChecklistScript>().setChecklist();

    }
    public void discardBottle()
    {
        for (int i = 0; i < takenitem.Count; i++)
        {
            takenitem[i].enabled = false;
        }
        treatmentcreated = null;
        treatmentcreated = new List<string>();
    }
    public void continueBottle()
    {
        for (int i = 0; i < takenitem.Count; i++)
        {
            takenitem[i].enabled = false;
        }
        if (patients.Count > 0)
        { 
            
            patients[0].setTreatment(treatmentcreated);
            
            sendroom.GetComponent<SendRoomScript>().patients.Add(patients[0]);
           
            patients.RemoveAt(0);
            checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
            checkpanel.GetComponent<ChecklistScript>().patientname = "";
            checkpanel.GetComponent<ChecklistScript>().allsymptoms = "";
            checkpanel.GetComponent<ChecklistScript>().type = "";
            checkpanel.GetComponent<ChecklistScript>().shape = "";
            if (patients.Count > 0)
            {
                setChecklist();
            }
            else
            {
                checkpanel.GetComponent<ChecklistScript>().setChecklist();
            }
        }
        treatmentcreated = new List<string>();
    }
    public void penClicked()
    {
        if (takenitem[0].enabled == false)
        { 
        takenitem[0].enabled = true;
        treatmentcreated.Add("penicillin");
        }
    }
    public void amoClicked()
    {
        if (takenitem[1].enabled == false)
        {
            takenitem[1].enabled = true;
            treatmentcreated.Add("amoxicillin");
        }
    }
    public void ibuClicked()
    {
        if (takenitem[2].enabled == false)
        {
            takenitem[2].enabled = true;
            treatmentcreated.Add("ibuprofen");
        }
    }
    public void acyClicked()
    {
        if (takenitem[3].enabled == false)
        {
            takenitem[3].enabled = true;
            treatmentcreated.Add("acyclovir");
        }
    }
    public void pegClicked()
    {
        if (takenitem[4].enabled == false)
        {
            takenitem[4].enabled = true;
            treatmentcreated.Add("peginterferon");
        }
    }
    public void adClicked()
    {
        if (takenitem[5].enabled == false)
        {
            takenitem[5].enabled = true;
            treatmentcreated.Add("advil");
        }
    }
    public void metClicked()
    {
        if (takenitem[6].enabled == false)
        {
            takenitem[6].enabled = true;
            treatmentcreated.Add("metronidazole");
        }
    }
    public void calClicked()
    {
        if (takenitem[7].enabled == false)
        {
            takenitem[7].enabled = true;
            treatmentcreated.Add("calamine lotion");
        }
    }
    public void noseClicked()
    {
        if (takenitem[8].enabled == false)
        {
            takenitem[8].enabled = true;
            treatmentcreated.Add("nasal spray");
        }
    }
    
}
