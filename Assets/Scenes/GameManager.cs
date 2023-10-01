using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    int points;


    private void Awake()
    {
        if (instance != this)
        {
            Destroy(this.gameObject);
        }
        instance = this;
    }

    public void ChangeScore(int ChangeAmount)
    {
        points += ChangeAmount;
    }

    public int GetPoints()
    {
        return points;
    }

}
