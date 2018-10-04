using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenuScript : MonoBehaviour {
    public Image storyboard;
    public List<Sprite> slides;
    public AudioSource sound;
    public AudioSource music;
	// Use this for initialization
	void Start () {
        storyboard.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void onStartGame()
    {
        print("yea");
        storyboard.enabled = true;
        storyboard.sprite = slides[0];
        StartCoroutine(ExecuteAfterTime(3));
        sound.Stop();
        music.Play();

    }
    public void onQuitApplication()
    {
        Application.Quit();
    } 
  
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        storyboard.sprite = slides[1];
        yield return new WaitForSeconds(time);
        storyboard.sprite = slides[2];
        yield return new WaitForSeconds(time+2.5f);
        storyboard.sprite = slides[3];
        yield return new WaitForSeconds(time+2.5f);
        storyboard.sprite = slides[4];
        yield return new WaitForSeconds(time);
        storyboard.sprite = slides[5];
        yield return new WaitForSeconds(time+2f);
        SceneManager.LoadScene("MainGame");
        // Code to execute after the delay
    }
    

}
