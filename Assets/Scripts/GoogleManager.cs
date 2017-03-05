using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;

public class GoogleManager : MonoBehaviour {
    
	// Use this for initialization
	void Awake () {
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
	}
	
    public void login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                SceneManager.LoadScene("main");
            }
            else
                Debug.Log("login error");
        });
    }
}
