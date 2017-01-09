using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class AttackPlayer : MonoBehaviour {

    Gesture current;
    Animator anim;
    void Start()
    {
        anim = GetComponentInChildren<Animator>();
    }

    void Update()
    {
        current = EasyTouch.current;
        if (current.type == EasyTouch.EvtType.On_Drag)
        {
            attack();
        }
    }

    void attack()
    {
        anim.SetTrigger("Attack");
    }
       
}