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
    public GameObject CountdownUi;
    public GameObject pauseUI;

    public TextMeshProUGUI jumpCount;

    private bool isNotDone = true;
   
    
    

    private void Start()
    {
        currentTime = 4;
        isPlaying = false;
        isPaused = false;
        isNotDone = true;
        pointText.text = "0";
        pauseUI.SetActive(false);
        CountdownUi.SetActive(true);
        gameOver.enabled = false;


        Next.gameObject.SetActive(false);

    }

    public void UpdateJumps(int i)
    {
        jumpCount.text = "x" + i.ToString();
    }

    private void FixedUpdate()
    {
        if (isPlaying == false && isPaused == false)
        {
            currentTime -= .06f; // time.fixedDetla
            countDown.text = currentTime.ToString("0");
            timer.enabled = false;
            pointText.enabled = false;


            if (currentTime <= .1)
            {
                currentTime = 0;
                SetPlay();
                GameManager.instance.Play();

            }
        }

        if (isPlaying && isPaused == false)
        {

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            currentTime += Time.fixedDeltaTime;
            timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            pointText.text = GameManager.instance.GetPoints().ToString() + " / " + GameManager.instance.GetTotalEnemies();
            if (currentTime <= 0)
            {
                SetEnd();

            }

        }

        if (isNotDone && GameManager.instance.GetPoints() == GameManager.instance.GetTotalEnemies())
        {
            isNotDone = false;
            isPlaying = false;
            timer.gameObject.SetActive(false);

            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            Next.text = Next.text + " " + string.Format("{0:00}:{1:00}", minutes, seconds);
            InvokeRepeating(nameof(BlinkNext), .25f, 0.25f);
        }

        /*
        if (isPaused == true && isPlaying == false)
        {

            CountdownUi.SetActive(true);
            pauseUI.SetActive(true);
            
            pointText.text = GameManager.instance.GetPoints().ToString();
            countDown.enabled = true;
            GameManager.instance.Pause();
            
            currentTime -= Time.fixedDeltaTime;
            countDown.text = currentTime.ToString("0");

            if (GameManager.instance.GetPoints() > GameManager.instance.GetTotalEnemies()) {
                gameOver.enabled = false;
                Next.enabled = true;
                Next.text = "Next Level in...";
                
                
                if(currentTime<= 0)
                {
                    GameManager.instance.LoadNew(GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<LoadNewScene>().sceneName);
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
        */




    }

    void BlinkNext()
    {
        Next.gameObject.SetActive(!Next.gameObject.activeInHierarchy);
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
}
