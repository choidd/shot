using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {
    
	// Update is called once per frame
	void Update () {
    }

    public void btn_01()
    {
        PlayerData.Instance.level++;
    }

    public void save()
    {
        PlayerData.Instance.saveLocal();
    }

    public void btn_stage01()
    {
        SceneManager.LoadScene("stage02");
    }
}
