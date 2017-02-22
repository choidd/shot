using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IListener {

    Image healthbar;
    int hp = 100;
	// Use this for initialization
	void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_HEALTH_CHANGE, this);
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {

        }
    }
}
