using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class countdown_timer : MonoBehaviour
{
    float currentTime = 1f;
    float startingTime = 11f;
    public bool timerActive = false;
    public GameObject loseTextObject;
    public Text countdownText;
    public GameObject other;

    public AudioSource audioSource;
    public AudioClip lossMusic;
    


    void Start()
    {
        currentTime = startingTime;
        loseTextObject.SetActive(false);
    }

    void Update()
    {
        if(timerActive == true)
        {
            currentTime -= 1 * Time.deltaTime;
            //lose
            if(currentTime <= 0 && playerMovement.count < 10)
            {
                timerActive = false;
                loseTextObject.SetActive(true);
                Destroy(other);
                audioSource.PlayOneShot(lossMusic);                
            }

            //win
            else if(playerMovement.count == 10)
            {
                timerActive = false;
            }
        }
        TimeSpan time = TimeSpan.FromSeconds(currentTime);
        countdownText.text = time.Seconds.ToString();

        if(Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(Input.GetKey(KeyCode.R))
        {
            playerMovement.count = 0;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }

    public void startTimer()
    {
        timerActive = true;
    }
}
