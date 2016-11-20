using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class Gamemanager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        Gesture current = EasyTouch.current;
        if(current.type == EasyTouch.EvtType.On_SimpleTap) // 한번 터치시
        {

        }
	}
}
