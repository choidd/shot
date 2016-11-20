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
        transform.position = Vector3.Lerp(transform.position,
            targetTr.position - (targetTr.forward * distanceAway) +
            (Vector3.up * distanceUp),
            Time.deltaTime * 30f);
        transform.LookAt(targetTr.position);
	}
}
