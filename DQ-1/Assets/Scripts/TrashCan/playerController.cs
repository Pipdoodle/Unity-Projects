using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class playerController : MonoBehaviour
{
    int mood = 40;
    float speed = 5;
    string text1;
    public Text bottom;
    public Canvas black;
    bool talking = false;
    public GameObject background;
    // Use this for initialization
    void Start()
    {
        GetComponent<SpriteRenderer>().color = Color.red;
        black.enabled = false;
        background.GetComponent<SpriteRenderer>().color = new Color(0.2F, 0.3F, 0.4F, 0.5F);
    }

    // Update is called once per frame
    void Update()
    {
        if (!talking)
        {
            Vector2 direction = new Vector2(0, 0);
            if (Input.GetKey(KeyCode.DownArrow))
                direction.y -= 1;
            if (Input.GetKey(KeyCode.UpArrow))
                direction.y += 1;
            if (Input.GetKey(KeyCode.RightArrow))
                direction.x += 1;
            if (Input.GetKey(KeyCode.LeftArrow))
                direction.x -= 1;
            direction = direction.normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Return))
        {
            talking = false;
            bottom.text = "";
            black.enabled = false;
        }


    }

    string returnText(string input)
    {
        switch (input)
        {
            case "bed":
                {
                    if (mood <= 25)
                        return "Why can’t I just go back to sleep? I don’t want to go to school. Better not get into bed though, otherwise mom will come in. ";
                    else return "At least I’ll always have my bed to return to later. ";
                }
            case "bookcase":
                {
                    if (mood <= 25)
                        return "All these books that I’ll never end up reading.";
                    else return "Hopefully I’ll get to read these books oneday";
                }
            default: return "none";

        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //if (Input.GetKey(KeyCode.Return))
        Debug.Log("Trigger");
        bottom.text = returnText(other.gameObject.tag);
        black.enabled = true;
        //}
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        //Debug.Log(other.gameObject.tag);
        talking = true;
        bottom.text = returnText(other.gameObject.tag);
        black.enabled = true;
    }
}
