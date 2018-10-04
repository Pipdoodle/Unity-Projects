using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
public class SendRoomScript : MonoBehaviour {
    // Use this for initialization
    public GameObject gamecontrol;
    List<string> patienttreatment;
    List<string> truetreatment;
    public Text banner;
    public List<PatientClass> patients;
    public GameObject checkpanel;
    public GameObject package;
    int maxtime;
    void Start () {
        patients = new List<PatientClass>();
        

    }
    private void Update()
    {
        if (patients != null)
        { 
            if (patients.Count > 0)
            {
                package.SetActive(true);
            }
            else package.SetActive(false);
        }
    }

    public void onSent()
    {
        if (patients.Count>0)
        {
            float time = patients[0].returnPerson().GetComponent<PatientScript>().timer;
            int level = gamecontrol.GetComponent<GameControlScript>().level;
           List<string> patienttreatment = patients[0].getTreatment();
            List <string> truetreatment = patients[0].getDisease().getTreatment();
            patienttreatment.Sort();
            for (int i=0;i<patienttreatment.Count;i++)
                print(patienttreatment[i]);
            truetreatment.Sort();
            for (int i = 0; i < truetreatment.Count; i++)
                print(truetreatment[i]);
            switch (level)
            {
                case 1:
                    {
                        maxtime = 180;
                    }
                break;
                case 2:
                    {
                        maxtime = 140;
                    }
                 break;
                case 3:
                    {
                        maxtime = 100;
                    }
                   break;
                case 4:
                    {
                        maxtime = 60;
                    }
                    break;
                case 5:
                    {
                        maxtime = 40;
                    }
                    break;
                case 6:
                    {
                        maxtime = 30;
                    }
                    break;
            }


            if (time < maxtime)
            {
                if (truetreatment.SequenceEqual(patienttreatment))
                {
                    banner.text = "Success! " + patients[0].returnName() + " is now well and happy.";
                    gamecontrol.GetComponent<GameControlScript>().patientsuccess++;
                }
                else
                {
                    gamecontrol.GetComponent<GameControlScript>().patientdied++;
                    banner.text = patients[0].returnName() + " has died from incorrect medication.";
                }
            }
            else
            {
                banner.text = patients[0].returnName() + " is already dead. You are delivering to a dead, empty house. Next time be faster.";
                gamecontrol.GetComponent<GameControlScript>().patientdied++;
            }
            
            
            //Destroy(patients[0].returnPerson());
            patients.RemoveAt(0);
            if (patients.Count > 0)
            {
                package.SetActive(true);
                setChecklist();
            }
            else
            {
                checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
                checkpanel.GetComponent<ChecklistScript>().patientname = "";
                checkpanel.GetComponent<ChecklistScript>().allsymptoms = "";
                checkpanel.GetComponent<ChecklistScript>().type = "";
                checkpanel.GetComponent<ChecklistScript>().shape = "";


                checkpanel.GetComponent<ChecklistScript>().setChecklist();
                package.SetActive(false);
            }
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
                symptomcreater = symptomcreater + patients[0].getDisease().getSymptoms()[i] + " \n ";
            }
        checkpanel.GetComponent<ChecklistScript>().allsymptoms = symptomcreater;
        checkpanel.GetComponent<ChecklistScript>().type = patients[0].getType();
        checkpanel.GetComponent<ChecklistScript>().shape = patients[0].getShape();
        checkpanel.GetComponent<ChecklistScript>().setChecklist();
    }
    void sent()
    {

    }
}
