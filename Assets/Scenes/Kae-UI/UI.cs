using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI pointText;
    public TextMeshProUGUI timer;
    public TextMeshProUGUI gameOver;
    public float currentTime;
    private bool isPlaying;
    private bool isPaused;
    private float wallCloseTime;
    public GameObject CountdownUi;
    public GameObject pauseUI;
    public GameObject play;
    public GameObject pause;
    public GameObject mainMenu;
    private float points;
    

    private void Start()
    {
        currentTime = 3;
        isPlaying = false;
        isPaused = false;
        wallCloseTime = 1f;
        pointText.text = "0";
        pauseUI.active = false;
        CountdownUi.active = true;
        gameOver.enabled = false;
        points = 2;
        

    }
    private void Update()
    {
        if (isPlaying == false && isPaused==false)
        {
            currentTime -= Time.deltaTime;
            countDown.text= currentTime.ToString("0");
            timer.enabled = false;
            pointText.enabled = false;
            pause.active = false;

            if (currentTime <= .1)
            {
                currentTime = wallCloseTime*60;
                SetPlay();

            }
        }

        if (isPlaying && isPaused==false)
        {
            
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            currentTime -= Time.deltaTime;
            timer.text = string.Format("{0:00}:{1:00}",minutes,seconds);  
            if (currentTime <= 0)
            {
                SetEnd();
                
            }

        }

        if (isPaused == true && isPlaying==true)
        {
            pauseUI.active = true;

            

        }
        if (isPaused==true && isPlaying == false)
        {
            
            CountdownUi.active = false;
            gameOver.enabled = true;
            pauseUI.active = true;
            play.active = false;
            

            
            pointText.text = points.ToString();

        }

         
            

    }
    public void SetPlay()
    {
        isPaused = false;
        isPlaying = true;


        pauseUI.active = false;
        CountdownUi.active = true;
        isPlaying = true;
        pause.active = true;
        timer.enabled = true;
        pointText.enabled = true;
        countDown.enabled = false;
    }

    public void SetPause()
    {
        isPaused = true;
        isPlaying = true;

        pauseUI.active = true;
        CountdownUi.active = false;


    }
    public void SetEnd()
    {
        isPaused = true;
        isPlaying = false;

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    {
       // SceneManager.LoadScene(MainMenuScene);
    }

}
