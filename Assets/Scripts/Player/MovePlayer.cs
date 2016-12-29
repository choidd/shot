using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
public class MovePlayer : MonoBehaviour {

    NavMeshAgent nav;
    Gesture current;
    Ray ray;
    RaycastHit hit;
    Animator anim;
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
            Vector3 tmp = current.position;
            tmp.y += 30f;
            ray = Camera.main.ScreenPointToRay(tmp);
            Physics.Raycast(ray, out hit);
            move();
        }
        if(current.type == EasyTouch.EvtType.On_DoubleTap)
        {

        }
	}

    void move()
    {
        anim.SetBool("Walk", true);
        nav.SetDestination(hit.point);
    }
}
