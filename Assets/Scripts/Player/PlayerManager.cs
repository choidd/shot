using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

public class PlayerManager : MonoBehaviour {
    public enum PLAYER_STATE
    {
        IDLE, WALK, ATTACK, CHASE
    };

    PLAYER_STATE current_state = PLAYER_STATE.IDLE;

    Gesture current; // 현재 터치상태
    Ray ray;
    RaycastHit hit;
    UnityEngine.AI.NavMeshAgent nav;

    Animator anim;

	// Use this for initialization
	void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        
        StartCoroutine(State_Control());
    }
	
	// Update is called once per frame
	void Update () {
        current = EasyTouch.current;
        switch (current.type)
        {
            case EasyTouch.EvtType.On_SimpleTap:
                current_state = PLAYER_STATE.WALK;

                ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                Physics.Raycast(ray, out hit);
                nav.SetDestination(hit.point);
                break;
        }
    }
   
    public IEnumerator State_Control()
    {
        while(true)
        {
            yield return new WaitForSeconds(.1f);
            switch (current_state)
            {
                case PLAYER_STATE.IDLE:
                    anim.SetBool("isWalking", false);
                    break;
                case PLAYER_STATE.WALK:
                    anim.SetBool("isWalking", true);
                    if (nav.remainingDistance <= nav.stoppingDistance)
                        current_state = PLAYER_STATE.IDLE;
                    break;
                case PLAYER_STATE.CHASE:
                    break;
                case PLAYER_STATE.ATTACK:
                    break;
            }
            
        }

    }
    
}
