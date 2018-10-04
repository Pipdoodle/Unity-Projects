using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiseaseClass {
    public List<string> symptoms;
    public Sprite microscopeimage;
    public List<string> treatment;
    public int prescription;
    public bool virus;

   public DiseaseClass(List<string> symptoms, Sprite microscopeimage,List<string> treatment, int prescription, bool virus)
    {
        this.symptoms = symptoms;
        this.microscopeimage = microscopeimage;
        this.treatment = treatment;
        this.prescription = prescription;
        this.virus = virus;
    }
    public Sprite returnImage()
    {
        return microscopeimage;
    }
    public List<string> getTreatment()
    {
        return treatment;
    }
    public List<string> getSymptoms()
    {
        return symptoms;
    }
}
