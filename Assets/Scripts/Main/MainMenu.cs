using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class MainMenu : MonoBehaviour {

    public Text tab01;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        tab01.text = Convert.ToString(PlayerData.Instance.level);
    }

    public void btn_01()
    {
        PlayerData.Instance.level++;
    }

    public void save()
    {
        PlayerData.Instance.saveLocal();
    }
}
