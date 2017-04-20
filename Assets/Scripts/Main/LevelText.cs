using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
public class LevelText : MonoBehaviour {
    
    TextMeshProUGUI level;
	// Use this for initialization
	void Start () {
        level = GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update () {
        level.SetText("LEVEL : {0}",PlayerData.Instance.level);
    }
}
