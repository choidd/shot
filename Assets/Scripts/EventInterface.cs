using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EVENT_TYPE
{
    GAME_INIT,
    GAME_END,
    GAME_PLAYER_HEALTH_CHANGE,
    GAME_PLAYER_DIE,
    GAME_PLAYER_WALK,
    GAME_ENEMY_ATTACK,
};

public interface IListener
{
    void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null);
}


public class EventInterface : MonoBehaviour {
    
}
