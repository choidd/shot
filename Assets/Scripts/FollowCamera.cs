using UnityEngine;
using System.Collections;

public class FollowCamera : MonoBehaviour {
    public Transform targetTr;

    float distanceUp = 4f;
    float distanceAway = 7f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = targetTr.position + Vector3.up * distanceUp -
            Vector3.forward;
	}
}
