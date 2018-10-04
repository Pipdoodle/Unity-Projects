using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PatientScript : MonoBehaviour {
    public GameObject maincam;
    int speed=5;
    public bool pause=false;
    public float timer;
    public GameObject speechbubble;
    public GameObject go;
    public GameObject canvas;
    private GameControlScript gcs;
    public bool visible = true;
    public bool collided=false;
    private float interactdist;

    //float interactdist = 1.0f;


    // Use this for initialization
    void Start () {
        maincam = GameObject.Find("Main Camera");
        canvas = GameObject.Find("CanvasMain");
        gcs = maincam.GetComponent<GameControlScript>();
        go.SetActive(true);
        collided = false;
        


    }

    void FixedUpdate()
    {  
            if (pause != true)
        {
            timer += Time.deltaTime;

            if (canvas.GetComponent<MenuPanelScript>().room == "wait")
            {
                if (visible)
                    go.GetComponent<SpriteRenderer>().enabled = (true);
                else go.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
             go.GetComponent<SpriteRenderer>().enabled = false;
            }
            if (timer > 0)
            {
                if ((go.transform.position.x > -4))
                {
                    if (go.transform.position.x < -3.5 && gcs.speechbubbleactive == false)
                    {
                        gcs.bubblesetActive();
                        //go.GetComponent<BoxCollider2D>().enabled = false;
                    }
                    if (collided == false)
                        go.transform.Translate(-1 * Time.deltaTime * speed, 0, 0);
                }
                else
                {
                    go.GetComponent<Animator>().speed = 0;
                    go.transform.position = new Vector2(-4,go.transform.position.y);
                }
            }
        }
    }
   

  
    public void setSpriteInactive()
    {
        go.SetActive(false);
    }
}
