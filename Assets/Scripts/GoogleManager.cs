using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;

public class GoogleManager : MonoBehaviour {

    public Text debug;
    public Text id;
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
                id.text = Social.localUser.id;
            else
                Debug.Log("login error");
        });
    }
}
