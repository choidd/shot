using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
public class MovePlayer : MonoBehaviour {

    public enum AI_PLAYER_STATE
    {
        WALK, ATTACK, CHASE
    };

    AI_PLAYER_STATE current_state = AI_PLAYER_STATE.WALK;
    NavMeshAgent nav;
    Gesture current;
    Ray ray;
    RaycastHit hit;
    Animator anim;
    float dist;

    bool CanSeePlayer = false;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        current = EasyTouch.current;
        if(current.type == EasyTouch.EvtType.On_SimpleTap)
        {
            move();
        }
        dist = Vector3.Distance(transform.position, hit.point);
        if (dist < 0.1f)
            anim.SetBool("Walk", false);
	}

    void move()
    {
        Vector3 tmp = current.position;
        tmp.y += 30f;
        ray = Camera.main.ScreenPointToRay(tmp);
        Physics.Raycast(ray, out hit);
        nav.SetDestination(hit.point);
    }

    public IEnumerator State_Walk()
    {
        current_state = AI_PLAYER_STATE.WALK;

        anim.SetTrigger("Walk");
        while(current_state == AI_PLAYER_STATE.WALK)
        {
            if(CanSeePlayer)
            {
                StartCoroutine(State_Chase());
                yield break;
            }
        }
        yield return null;
    }

    public IEnumerator State_Chase()
    {
        yield return null;
    }
}
