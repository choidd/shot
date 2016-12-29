using UnityEngine;
using System.Collections;

public class Perspective : MonoBehaviour {

    public Transform player;
    Animator anim;
    Ray ray;
    RaycastHit hit;
    Vector3 rayDirection;
	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        rayDirection = player.position - transform.position;

        if((Vector3.Angle(rayDirection, transform.forward)) < 45)
        {
            if(Physics.Raycast(transform.position, rayDirection, out hit))
            {
                if(hit.collider.name == "Player")
                {
                }
            }
        }
	}
}
