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

        if (isPaused == true && isPlaying == true)
        {
            pauseUI.SetActive(true);



        }
        if (isPaused == true && isPlaying == false)
        {

            CountdownUi.SetActive(false);
            pauseUI.SetActive(true);
            play.SetActive(false);
            pointText.text = GameManager.instance.GetPoints().ToString();
            GameManager.instance.Pause();
            if (GameManager.instance.GetPoints() > 5) { 

                
                 }
         else {
                

                gameOver.enabled = true; }

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

    public void SetPause()
    {
        isPaused = true;
        isPlaying = true;

        pauseUI.SetActive(true);
        CountdownUi.SetActive(false);


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

}
