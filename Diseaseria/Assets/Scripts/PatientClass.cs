using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientClass {
    public string patientname = "";
    public int bmi;
    public GameObject person;
    public DiseaseClass disease;
    public int prescriptiongiven;
    public List<string> treatmentgiven;
    bool active;
    public string type;
    public string shape;
    public float timer = 0f;
    public bool pause;
    public GameObject speechbubble;
    bool speechbubbleactive;
    int speed=100;
    public GameObject maincam;
    public PatientClass(string name, DiseaseClass disease, GameObject person, bool active)
    {
        patientname = name;
        this.person = person;
        this.disease = disease;
        this.active = active;

    }
   
    

    public void setPause (bool p)
    {
        this.pause = p;
    }
    public void setPrescription(int p)
    {
        this.prescriptiongiven = p;
    }
    public void setTreatment(List<string> treatment)
    {
        treatmentgiven = treatment;
    }
    public float returnTimer()
    {
        return timer;
     }
    public GameObject returnPerson()
    {
        return person;
    }
    public string returnName()
    {
        return patientname;
    }
    public DiseaseClass getDisease()
    {
        return disease;
    }
    public bool returnActive()
    {
        return active;
    }
    public void setActive()
    {
        active = false;
    }
    public List<string> getTreatment()
    {
        return treatmentgiven;
    }
    
    public void setType(string t)
    {
       type = t;
    }

    public void setShape(string s)
    {
        shape = s;
    }
    public string getShape()
    {
        return shape;
    }
    public string getType()
    {
        return type;
    }

}
