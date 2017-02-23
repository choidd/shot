using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IListener {

    public Slider hpBar;
    public Image attackedBlood;
    Color flashColour = new Color(1f, 0f, 0f, 0.1f);


    // Use this for initialization
    void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_HEALTH_CHANGE, this);
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {
            case EVENT_TYPE.GAME_PLAYER_HEALTH_CHANGE:
                hpBar.value -= (int)Param;
                attackedBlood.color = flashColour;
                attackedBlood.color = Color.Lerp(attackedBlood.color, Color.clear, 50 * Time.deltaTime);
                break;
        }
    }
}
