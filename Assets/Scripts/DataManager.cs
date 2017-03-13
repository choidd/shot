using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class DataManager : MonoBehaviour{

    public static DataManager Instance
    {
        get { return instance; }
        set { }
    }
    public static DataManager instance = null;

    PlayerData playerData;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

    void Start()
    {

    }

    public void SubmitPlayerScore(int newScore)
    {
        if(playerData.highScore < newScore)
        {
            playerData.highScore = newScore;
        }
    }

    public void upgradePlayerLevel()
    {
        playerData.userLevel += 1;
    }

    public void loadData()
    {
        playerData.highScore = PlayerPrefs.GetInt("highScore");
        playerData.userLevel = PlayerPrefs.GetInt("userLevel");
    }

    public void saveData()
    {
        PlayerPrefs.SetInt("highScore", playerData.highScore);
        PlayerPrefs.SetInt("userLevel", playerData.userLevel);
    }
    
}
