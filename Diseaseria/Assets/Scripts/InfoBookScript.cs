using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoBookScript : MonoBehaviour {

    public Image infobook;
    public List<Sprite> infopglist = new List<Sprite>();


    int index = 0;
    bool open = false;

    // Use this for initialization
    void Start () {
        Button infobookbutton = this.GetComponent<Button>();
        infobookbutton.onClick.AddListener(TaskOnClick);
        infobook.enabled=false;
      
    }
	
    void TaskOnClick()
    {  
        if (!open)
        {
            open = true;
            infobook.enabled=true;
            infobook.sprite = infopglist[index];
        }
        else if (open)
        {
            open = false;
            infobook.enabled=false;
        }
    }

	// Update is called once per frame
	void Update () {
        if (open)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))//right
            {
                //print("right");
                if (index < (infopglist.Count - 1))
                {
                    index++;
                }

                infobook.sprite = infopglist[index];

            }
            if (Input.GetKeyDown(KeyCode.LeftArrow))//left
            {
                //print("left");
                if (index > 0)
                {
                    index--;
                }
                infobook.sprite = infopglist[index];

            }
        }
        }
}
