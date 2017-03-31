using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Stat
{
    id,
    level,
    heart_cnt
}

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance
    {
        get { return instance; }
        set { }
    }
    public static PlayerData instance = null;

    
    private string savedData;

    private string id;
    private int level;
    private int heart_cnt;


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
    }
    
    public void loadLocal()
    {
        id = PlayerPrefs.GetString("id");
        level = PlayerPrefs.GetInt("level");
        heart_cnt = PlayerPrefs.GetInt("heart_cnt");
    }

    public void saveLocal()
    {
        PlayerPrefs.SetString("id", id);
        PlayerPrefs.SetInt("level", level);
        PlayerPrefs.SetInt("heart_cnt", heart_cnt);
    }
}
