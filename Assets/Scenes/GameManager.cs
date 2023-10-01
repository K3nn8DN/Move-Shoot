using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int points;
     Behaviour wallMoveScript;
    
    public GameObject player;
    public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;
    public string scene;



    private void Awake()
    {
        instance = this;
        if (instance != this)
        {
            Destroy(this.gameObject);
        }
        

    }
    private void Start()
    {
        GameManager.instance.OnSceneLoad(scene);
    }


    public void ChangeScore(int ChangeAmount)
    {
        points += ChangeAmount;
    }

    public int GetPoints()
    {
        return points;
    }
    public void SetPoints(int Amount)
    {
        points = Amount;
    }

    public void OnSceneLoad(string sceneName)
    {
        if (sceneName == "First Level" || sceneName == "Level 2")

        {

            GameObject.Find("Input");
            //Time.timeScale = 0f;

            
            wallMoveScript= wall1.GetComponent<wallMovement>();
            wallMoveScript.enabled = false;
            wallMoveScript = wall2.GetComponent<wallMovement>();
            wallMoveScript.enabled = false;
            wallMoveScript = wall3.GetComponent<wallMovement>();
            wallMoveScript.enabled = false;
            wallMoveScript = wall4.GetComponent<wallMovement>();
            wallMoveScript.enabled = false;
            

            

        }
        
    
    
    }

    public void StartGame()
    {
        wallMoveScript.enabled = true;
        
    }





}
