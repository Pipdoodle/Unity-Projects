using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChecklistScript : MonoBehaviour {
    public string patientname="";
    public GameObject checklistpanel;
    bool checklistopen = false;
    public string allsymptoms="";
    public Text insertname;
    public Text symptoms;
    public string shape;
    public string type;
    public List<GameObject> toggle;
   
	// Use this for initialization
	void Start () {
        Button checklistbutton = this.GetComponent<Button>();
        
        //checklistbutton.onClick.AddListener(TaskOnClick);

        checklistpanel.SetActive(false);
    }
	
    public void TaskOnClick()
    {
        if (!checklistopen)
        {
            checklistpanel.SetActive(true);
            checklistopen = true;
            clearAllToggles();
            setChecklist();

        }
        else if (checklistopen)
        {
            checklistpanel.SetActive(false);
            checklistopen = false;
            
        }
    }
	// Update is called once per frame
	void Update () {
		//ADD CODE TO RESTRICT USERS ONLY SELECT ONE TOGGLE PER CATEGORY
	}

    public void clearAllToggles()
    {
        toggle[0].GetComponent<Toggle>().isOn = false;
        toggle[1].GetComponent<Toggle>().isOn = false;
        toggle[2].GetComponent<Toggle>().isOn = false;
        toggle[3].GetComponent<Toggle>().isOn = false;
        toggle[4].GetComponent<Toggle>().isOn = false;
        toggle[5].GetComponent<Toggle>().isOn = false;
        toggle[6].GetComponent<Toggle>().isOn = false;
        toggle[7].GetComponent<Toggle>().isOn = false;
    }

    public void setChecklist()
    {





        insertname.text = patientname;

        symptoms.text = allsymptoms;
        //bacteria and virus toggle set



        /*if (type.Count > 0)
        {
            for (int i = 0; i < type.Count; i++)
            {*/
        if (type == "bacteria")
        {
            toggle[0].GetComponent<Toggle>().isOn = true;
            toggle[1].GetComponent<Toggle>().isOn = false;
        }
        if (type == "virus")
        {
            toggle[1].GetComponent<Toggle>().isOn = true;
            toggle[0].GetComponent<Toggle>().isOn = false;
        }
        //  }
        //}

        //shape toggle set
        int toggleindex = 0;
        if (shape == "coccus")
        {
            toggleindex = 2;
        }
        else if (shape == "spirillum")
        {
            toggleindex = 3;
        }
        else if (shape == "spirochete")
        {
            toggleindex = 4;
        }
        else if (shape == "picorna")
        {
            toggleindex = 5;
        }
        else if (shape == "flavi")
        {
            toggleindex = 6;
        }
        else if (shape == "herpes")
        {
            toggleindex = 7;
        }

        for (int i = 2; i < 8; i++)
        {
            if (i == toggleindex)
            {
                toggle[i].GetComponent<Toggle>().isOn = true;
            }
            else
            {
                toggle[i].GetComponent<Toggle>().isOn = false;
            }

        }

    }
}
