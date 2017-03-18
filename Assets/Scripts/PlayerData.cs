using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance
    {
        get { return instance; }
        set { }
    }
    public static PlayerData instance = null;


    public int highScore;
    public int userLevel;
    public string userId;

    public byte[] b_highScore;
    public byte[] b_userLevel;


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

        highScore = 0;
        userLevel = 1;
    }

}
