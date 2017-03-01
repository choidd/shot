using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE
{
    GAME_INIT,
    GAME_END,
    GAME_PLAYER_HEALTH_CHANGE,
    GAME_PLAYER_DIE,
    GAME_PLAYER_ATTACK,
    GAME_PLAYER_DAMAGED,
    GAME_ENEMY_ATTACK,
    GAME_ENEMY_DIE,
    GAME_OPTION_CLICK,
    GAME_STATE_WIN,
    GAME_STATE_LOSE
};

public interface IListener
{
    void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);
}


public class EventInterface : MonoBehaviour {
    
}
