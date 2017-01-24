using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HedgehogTeam.EasyTouch;

public class PlayerManager : MonoBehaviour {

    Gesture current; // 현재 터치상태
    Ray ray;
    RaycastHit hit;
    UnityEngine.AI.NavMeshAgent nav;
    Transform ThisTransform;
	// Use this for initialization
	void Start () {
        ThisTransform = transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
    }
	
	// Update is called once per frame
	void Update () {
        current = EasyTouch.current;
        switch(current.type)
        {
            case EasyTouch.EvtType.On_SimpleTap:
                move();
                break;
        }		
	}

    void move()
    {
        ray = Camera.main.ScreenPointToRay(Camera.main.transform.position);
        Physics.Raycast(ray, out hit);
        nav.Move(hit.point);
    }
}
