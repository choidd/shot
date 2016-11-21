using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
public class MovePlayer : MonoBehaviour {

    NavMeshAgent nav;
    Gesture current;
    Camera MC; // main camera
    Ray ray;
    RaycastHit hit;
	// Use this for initialization
	void Start () {
        nav = GetComponent<NavMeshAgent>();
        MC = GetComponentInChildren<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
        current = EasyTouch.current;
        if(current.type == EasyTouch.EvtType.On_SimpleTap)
        {
            ray = Camera.main.ScreenPointToRay(current.position);
            Physics.Raycast(ray, out hit);
            move();
        }
	}

    void move()
    {
        nav.SetDestination(hit.point);
    }
}
