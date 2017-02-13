using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour {

    public float dist = 10.0f;
    public float height = 3.0f;
    public float dampTrace = 20.0f;
    
    public Transform playerTr;
	
	// Update is called once per frame
	void LateUpdate () {
        transform.position = Vector3.Lerp(transform.position,
            playerTr.position - (playerTr.forward * dist) + (Vector3.up * height),
            Time.deltaTime * dampTrace);
        transform.LookAt(playerTr.position);

	}
}
