using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
public class MovePlayer : MonoBehaviour {

    NavMeshAgent nav;
    Gesture current;
    Ray ray;
    RaycastHit hit;
    Animator anim;
    float dist;
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
        anim.SetBool("Walk", true);
        nav.SetDestination(hit.point);
    }
}
