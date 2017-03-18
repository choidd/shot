using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
public class MainScript : MonoBehaviour {

    public Text ID;
    public Text LEVEL;
    string strID, strLEVEL;
	// Use this for initialization
	void Start ()
    {
        strID = "ID : " + PlayerData.Instance.userId;
        ID.text = strID;
        LEVEL.text = "Level : " + Convert.ToString(PlayerData.Instance.userLevel);
	}

    public void btn_start()
    {
        SceneManager.LoadScene("city");
    }

    public void btn_leaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}
