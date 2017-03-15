using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    public Text ID;
    public Text LEVEL;
    string strID, strLEVEL;
	// Use this for initialization
	void Start ()
    {
        strID = "ID : " + Social.localUser.userName;
        ID.text = strID;
        LEVEL.text = strLEVEL;
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
