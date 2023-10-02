using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int points;
     Behaviour wallMoveScript;
     
    
    //public GameObject player;
    /*public GameObject wall1;
    public GameObject wall2;
    public GameObject wall3;
    public GameObject wall4;*/
   // public string scene;
    private GameObject input;
    private int m_totalEnemies;

    private GameObject[] m_items;

    private AudioSource m_source;

    private bool resetting = false;
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
        m_source = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoad;
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoad;
    }

    public void ChangeScore(int ChangeAmount)
    {
        points += ChangeAmount;

        if (!resetting && points == m_totalEnemies)
        {
            NextLevel();
        }
    }

    private void NextLevel()
    {
        m_source?.Play();

        foreach (var item in m_items)
        {
            var behavior = item.GetComponent<wallMovement>();
            if (behavior)
                behavior.enabled = false;
        }

        var deathTriggers = GameObject.FindGameObjectsWithTag("DeathTrigger");
        foreach (var item in deathTriggers)
            item.SetActive(false);

        Invoke(nameof(LoadNext), 1f);
    }

    public void ResetLevel()
    {
        resetting = true;
        LoadNew(SceneManager.GetActiveScene().name);
    }

    void LoadNext()
    {
        LoadNew(GameObject.FindGameObjectWithTag("SceneLoader").GetComponent<LoadNewScene>().sceneName);
    }

    public int GetPoints()
    {
        return points;
    }

    public int GetTotalEnemies()
    {
        return m_totalEnemies;
    }

    public void SetPoints(int Amount)
    {
        points = Amount;
    }

    public void OnSceneLoad(Scene scene, LoadSceneMode mode)
    {
        input = GameObject.Find("Input");

        if (input == null)
            return;

        Pause();
        points = 0;

        m_totalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;

        resetting = false;
    }

    public void Pause()
    {
        input?.SetActive(false);


        m_items = GameObject.FindGameObjectsWithTag("Wall");
        foreach (var item in m_items)
        {
            var behavior = item.GetComponent<wallMovement>();
            if (behavior)
                behavior.enabled = false;
        }
/*
        wallMoveScript = wall1.GetComponent<wallMovement>();
        wallMoveScript.enabled = false;
        wallMoveScript = wall2.GetComponent<wallMovement>();
        wallMoveScript.enabled = false;
        wallMoveScript = wall3.GetComponent<wallMovement>();
        wallMoveScript.enabled = false;
        wallMoveScript = wall4.GetComponent<wallMovement>();
        wallMoveScript.enabled = false;*/
    }

    public void Play()
    {
        input?.SetActive(true);

        foreach (var item in m_items)
        {
            var behavior = item.GetComponent<wallMovement>();
            if (behavior)
                behavior.enabled = true;
        }

        /*
                wallMoveScript = wall1.GetComponent<wallMovement>();
                wallMoveScript.enabled = true;
                wallMoveScript = wall2.GetComponent<wallMovement>();
                wallMoveScript.enabled = true;
                wallMoveScript = wall3.GetComponent<wallMovement>();
                wallMoveScript.enabled = true;
                wallMoveScript = wall4.GetComponent<wallMovement>();
                wallMoveScript.enabled = true;*/

    }

    public void LoadNew(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
