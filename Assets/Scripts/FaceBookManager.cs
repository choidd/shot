using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Facebook.Unity;
using UnityEngine.SceneManagement;
public class FaceBookManager : MonoBehaviour {

	// Use this for initialization
	void Awake()
    {
        if(!FB.IsInitialized)
        {
            FB.Init();
        }
        else
        {
            FB.ActivateApp();
        }
    }

    public void Login()
    {
        FB.LogInWithReadPermissions(callback: OnLogin);
    }

    private void OnLogin(ILoginResult result)
    {
        if (FB.IsLoggedIn)
        {
            SceneManager.LoadScene("MainMenu");
        }
        else
        {
            Debug.Log("error");
        }
    }
}
