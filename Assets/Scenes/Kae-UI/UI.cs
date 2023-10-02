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
    public TextMeshProUGUI Next;
    public float currentTime;
    private bool isPlaying;
    private bool isPaused;
    private float wallCloseTime;
    public GameObject CountdownUi;
    public GameObject pauseUI;
    
    
    
   
    
    

    private void Start()
    {
        currentTime = 3;
        isPlaying = false;
        isPaused = false;
        wallCloseTime = 1f;
        pointText.text = "0";
        pauseUI.SetActive(false);
        CountdownUi.SetActive(true);
        gameOver.enabled = false;
        
        

    }
    private void FixedUpdate()
    {
        if (isPlaying == false && isPaused == false)
        {
            currentTime -= Time.fixedDeltaTime;
            countDown.text = currentTime.ToString("0");
            timer.enabled = false;
            pointText.enabled = false;


            if (currentTime <= .1)
            {
                currentTime = wallCloseTime * 60;
                SetPlay();
                GameManager.instance.Play();

            }
        }

        if (isPlaying && isPaused == false)
        {

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            currentTime -= Time.fixedDeltaTime;
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            pointText.text = GameManager.instance.GetPoints().ToString();
            if (currentTime <= 0)
            {
                SetEnd();

            }

        }

        
        if (isPaused == true && isPlaying == false)
        {

            CountdownUi.SetActive(true);
            pauseUI.SetActive(true);
            
            pointText.text = GameManager.instance.GetPoints().ToString();
            countDown.enabled = true;
            GameManager.instance.Pause();
            
            currentTime -= Time.fixedDeltaTime;
            countDown.text = currentTime.ToString("0");

            if (GameManager.instance.GetPoints() > 2) {
                gameOver.enabled = false;
                Next.enabled = true;
                Next.text = "Next Level in...";
                
                
                if(currentTime<= 0)
                {

                    if (GameManager.instance.scene =="First Level")
                    {
                        GameManager.instance.LoadNew("Level 2");
                    }
                    else { GameManager.instance.LoadNew("Title Screen"); }


                }

            }
         else {
                

                gameOver.enabled = true;
                Next.text = "to main menu in...";
                Next.enabled = true;
                if (currentTime <= 0)
                {
                      GameManager.instance.LoadNew("Title Screen"); 

                }

            }

        }

         
            

    }

    public void SetPlay()
    {
        isPaused = false;
        isPlaying = true;


        pauseUI.SetActive(false);
        CountdownUi.SetActive(true);
        isPlaying = true;
        
        timer.enabled = true;
        pointText.enabled = true;
        countDown.enabled = false;
    }

  
    public void SetEnd()
    {
        isPaused = true;
        isPlaying = false;
        currentTime = 5;

    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
