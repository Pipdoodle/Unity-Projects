using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControlScript : MonoBehaviour {

    public GameObject movepatient;
    public GameObject speechbubble;
    public GameObject obj;
    int speed = 2;
    public float timer=-3;
    public float waitTime = 30f;
    public bool speechbubbleactive;
    public List<PatientClass> patientlist;
    public List<DiseaseClass> diseaselist;
    List<string> names;
    public List<GameObject> patientsprites;
    public List<Sprite> slidesprites;
    public GameObject diagnoseroom;
    public GameObject checkpanel;
    bool active=true;
    int x=-1;
    public  PatientClass patient;
    public int level=1;
    public bool pause;
    public int patientsuccess=0;
    public int maxpatientneeded=1;
    public int patientscreated=0;
    public float timeinbetween=90;
    public int patientdied = 0;
    public int game=1;
    public Text leveltext;
   
    // Use this for initialization
    void Start () {
        timer = -3;
        Screen.SetResolution(1540, 700, false); //i used ratio of 22:10 and multiply by 80, if change, change line in update() too
        List<string> symptoms = new List<string> {"runny nose","sneezing","coughing","fever","headache"};
        List<string> treatments = new List<string> { "nasal spray","ibuprofen","advil"};
        DiseaseClass cold = new DiseaseClass(symptoms, slidesprites[0], treatments, 3, true);

        symptoms = new List<string> { "fever", "headache", "neck stiffness", "photophobia" };
        treatments = new List<string> { "advil", "ibuprofen" };
        DiseaseClass asepticmeningitis = new DiseaseClass(symptoms, slidesprites[2], treatments, 3, true);

        symptoms = new List<string> { "fever", "headache",  "dark urine" };
        treatments = new List<string> { "peginterferon" };
        DiseaseClass hepatitis = new DiseaseClass(symptoms, slidesprites[4], treatments, 3, true);

        symptoms = new List<string> { "fever" };
        treatments = new List<string> { "advil", "ibuprofen" };
        DiseaseClass dengue = new DiseaseClass(symptoms, slidesprites[1], treatments, 3, true);

        symptoms = new List<string> { "facial sores","asymptotic" };
        treatments = new List<string> { "acyclovir" };
        DiseaseClass herpes = new DiseaseClass(symptoms, slidesprites[5], treatments, 3, true);

        symptoms = new List<string> { "localized rash" };
        treatments = new List<string> { "acyclovir", "calamine lotion" };
        DiseaseClass shingles = new DiseaseClass(symptoms, slidesprites[8], treatments, 3, true);

        symptoms = new List<string> { "sore throat", "fever", "swollen lymph nodes in neck" };
        treatments = new List<string> { "amoxicillin", "penicillin" };
        DiseaseClass streptococcuspyogenes = new DiseaseClass(symptoms, slidesprites[11], treatments, 3, false);

        symptoms = new List<string> { "boils", "blisters" };
        treatments = new List<string> { "penicillin" };
        DiseaseClass staphylococcusaureus = new DiseaseClass(symptoms, slidesprites[9], treatments, 3, false);

        symptoms = new List<string> { "diarrhea", "severe abdominal pain", "vomiting" };
        treatments = new List<string> { "advil" };
        DiseaseClass foodpoisoning = new DiseaseClass(symptoms, slidesprites[3], treatments, 3, false);

        symptoms = new List<string> { "abdominal pain", "vomiting" };
        treatments = new List<string> { "amoxicillin", "metronidazole" };
        DiseaseClass stomachulcers = new DiseaseClass(symptoms, slidesprites[10], treatments, 3, false);

        symptoms = new List<string> { "red eyes", "high fever", "vomiting", "muscle pain" };
        treatments = new List<string> { "amoxicillin", "penicillin" };
        DiseaseClass leptospirosis = new DiseaseClass(symptoms, slidesprites[6], treatments, 3, false);

        symptoms = new List<string> { "bulls-eye rash" };
        treatments = new List<string> { "amoxicillin" };
        DiseaseClass lymedisease = new DiseaseClass(symptoms, slidesprites[7], treatments, 3, false);



        diseaselist = new List<DiseaseClass> { cold, asepticmeningitis, dengue, herpes,streptococcuspyogenes,shingles,staphylococcusaureus,foodpoisoning,stomachulcers,leptospirosis,lymedisease,hepatitis };
        speechbubble.SetActive(false);
        names = new List<string> { "Doot", "Nat", "Christine", "Leese", "Sak", "Liz", "Irene", "Saum", "Al", "Kap", "Jan", "Anan" };
        patientlist = new List<PatientClass> { };
        timeinbetween = 60;
        patientsuccess = 0;
        maxpatientneeded = 2;
        patientscreated = 0;
        patientdied = 0;
        level = 1;
        timer = 0;

    
}

    // Update is called once per frame
    void Update()
    {

        if (Screen.fullScreen || Camera.main.aspect != 1)
        {
            Screen.SetResolution(1540, 700, false); //i used ratio of 22:10 and multiply by 80, if change, change line in start() too
        }
        if (game==1)
        {
            if (pause != true)
            {
                if (timer == 0)
                {
                    patient = new PatientClass(names[Random.Range(0, names.Count)], diseaselist[Random.Range(0, diseaselist.Count)], Instantiate(patientsprites[Random.Range(0, patientsprites.Count)]), active);
                    patientlist.Add(patient);
                    patientscreated++;
                    x++;
                }
                levelInfo();

                timer += Time.deltaTime;
                if (timer > timeinbetween)
                {
                    timer = 0;
                }
            }
        }
	}

    public void bubblesetActive()
    {

        if (speechbubbleactive == false)
        {
            speechbubble.SetActive(true);
            speechbubbleactive = true;

        }
        else
        {
            speechbubble.SetActive(false);
            speechbubbleactive = false;
        }
    }
    public void patientAccepted()
    {
        checkpanel.GetComponent<ChecklistScript>().clearAllToggles();
        if (patientlist.Count > 0)
        { 

            for (int i = 0; i < patientlist.Count; i++)
            {
                if (patientlist[i].returnActive() == true)
                {
                    checkpanel.GetComponent<ChecklistScript>().patientname = patientlist[i].returnName();
                    string symptomcreater = "";
                    for (int j = 0; j < patientlist[i].getDisease().getSymptoms().Count; j++)
                    {
                        symptomcreater = symptomcreater +  patientlist[i].getDisease().getSymptoms()[j]+" \n " ;
                    }
                    checkpanel.GetComponent<ChecklistScript>().allsymptoms = symptomcreater;
                    checkpanel.GetComponent<ChecklistScript>().setChecklist();
                    diagnoseroom.GetComponent<DiagnoseRoomScript>().patients.Add(patientlist[i]);
                    patientlist[i].returnPerson().GetComponent<SpriteRenderer>().enabled=false;
                    patientlist[i].returnPerson().GetComponent<PatientScript>().visible = false;
                    patientlist[i].returnPerson().GetComponent<BoxCollider2D>().enabled = false;
               
                    patientlist[i].setActive();
                   
                    break;
                }
            }
        }
        
        speechbubble.SetActive(false);
        speechbubbleactive = false;
        
    }

   void levelInfo()
    {
        switch (level)
        {
            case 1:
                {
                    timeinbetween = 60f;
                    if ((patientsuccess + patientdied) > 4)
                    {
                        game = 2;
                        pause = true;
                        
                    }
                    if (patientsuccess >= maxpatientneeded)
                    {
                        level++;
                        maxpatientneeded = 6;
                        leveltext.text = "Rating: 2";

                    }
                }
                break;
            case 2:
                {
                    timeinbetween = 40f;
                    if ((patientsuccess + patientdied) > 11)
                    {
                        game = 2;
                        pause = true;

                    }
                    if (patientsuccess >= maxpatientneeded)
                    {
                        level++;
                        leveltext.text = "Rating: 3";
                        maxpatientneeded = 12;
                       


                    }
                }
                break;
            case 3:
                {
                    timeinbetween = 30f;
                    if ((patientsuccess + patientdied) > 20)
                    {
                        game = 2;
                        pause = true;
                    }
                    if (patientsuccess >= maxpatientneeded)
                    {
                        level++;
                        maxpatientneeded = 20;
                        leveltext.text = "Rating: 4";


                    }
                }
             break;
            case 4:
                {
                    timeinbetween = 25f;
                    if ((patientsuccess + patientdied) > 30)
                    {
                        game = 2;
                        pause = true;
                    }
                    if (patientsuccess >= maxpatientneeded)
                    {
                        level++;
                        maxpatientneeded = 30;
                        leveltext.text = "Rating: 5";


                    }
                }
                break;
            case 5:
                {
                    timeinbetween = 15f;
                    if ((patientsuccess + patientdied) > 45)
                    {
                        game = 2;
                        pause = true;
                    }
                    if (patientsuccess >= maxpatientneeded)
                    {
                        level++;
                        maxpatientneeded = 42;
                        leveltext.text = "Rating: 6";

                    }
                }
               break;
            case 6:
                {
                    if (patientdied>25)
                    {
                        game = 2;
                        pause = true;
                    }
                    timeinbetween = 10f;
                    if(patientsuccess>=maxpatientneeded)
                    {
                        game = 3;
                    }
                }
                break;
        }
    }

}