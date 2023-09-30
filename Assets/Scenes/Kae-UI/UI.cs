using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI countDown;
    public TextMeshProUGUI points;
    public TextMeshProUGUI timer;
    public float currentTime;
    private bool isPlaying;
    private float wallCloseTime;

    private void Start()
    {
        currentTime = 3;
        isPlaying = false;
        wallCloseTime = 1f;
        points.text = "0";
    }
    private void Update()
    {
        if (isPlaying == false)
        {
            currentTime -= Time.deltaTime;
            countDown.text= currentTime.ToString("0");
            timer.enabled = false;
            points.enabled = false;

            if (currentTime <= .1)
            {
                currentTime = wallCloseTime*60;
                isPlaying = true;
                timer.enabled = true;
                points.enabled = true;
                countDown.enabled = false;

            }
        }
        if (isPlaying)
        {
            
            float minutes = Mathf.FloorToInt(currentTime / 60);
            float seconds = Mathf.FloorToInt(currentTime % 60);
            currentTime -= Time.deltaTime;
            timer.text = string.Format("{0:00}:{1:00}",minutes,seconds);  

        }


    }

    
}
