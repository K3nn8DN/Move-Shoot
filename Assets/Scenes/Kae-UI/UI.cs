using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI : MonoBehaviour
{
    public TextMeshProUGUI countDown;
    public TMP_Text pause;
    public TMP_Text points;
    public TMP_Text timer;
    public float currentTime;
    private bool isPlaying;
    private float wallCloseTime;

    private void Start()
    {
        currentTime = 3;
        isPlaying = false;
        wallCloseTime = 5;
    }
    private void Update()
    {
        if (isPlaying == false)
        {
            currentTime -= Time.deltaTime;
            countDown.text= currentTime.ToString("0");
            if (currentTime <= .1)
            {
                currentTime = wallCloseTime;
                isPlaying = true;
                
            }
        }
        if (isPlaying)
        {
            currentTime -= Time.deltaTime;
            timer.text = currentTime.ToString("0");
            
        }
    }

    
}
