using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour, IListener {

    public Slider hpBar;
    public Image attackedBlood;
    public GameObject success;
    Color flashColour = new Color(1f, 0f, 0f, 0.5f);


    public GameObject setupWindow;
    bool issetupWindow = false;


    // Use this for initialization
    void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_HEALTH_CHANGE, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_OPTION_CLICK, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_STATE_WIN, this);
    }

    public void btn_setup()
    {
        EventManager.Instance.PoistNotification(EVENT_TYPE.GAME_OPTION_CLICK, this);
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {
            case EVENT_TYPE.GAME_PLAYER_HEALTH_CHANGE:
                hpBar.value -= (int)Param;
                attackedBlood.color = flashColour;
                attackedBlood.color = Color.Lerp(attackedBlood.color, Color.clear, 5 * Time.deltaTime);
                break;
            case EVENT_TYPE.GAME_ENEMY_DIE: // 점수올라가야하고.. Kill! 뭐 이런 이펙트하나 있어야지?
                break;
            case EVENT_TYPE.GAME_OPTION_CLICK:
                if (issetupWindow == false)
                {
                    setupWindow.SetActive(true);
                    issetupWindow = true;
                }
                else
                {
                    setupWindow.SetActive(false);
                    issetupWindow = false;
                }
                break;
            case EVENT_TYPE.GAME_STATE_WIN:
                success.SetActive(true);
                break;
        }
    }
}
