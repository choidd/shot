using UnityEngine;
using System.Collections;

public class SimpleBoatScript : MonoBehaviour {

	public float rollingIntensity = 3.0f;
	public float rollingSpeed = 5.0f;

	Transform _transform;
	float angle1;
	float angle2;
	
	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform> ();	
		angle1 = 0;
		angle2 = 30;
	}
	
	// Update is called once per frame
	void Update () {
		angle1 +=  rollingSpeed * 10.0f * Time.deltaTime;
		if(angle1 > 360)
			angle1 = 0;

		angle2 +=  rollingSpeed * 12.0f * Time.deltaTime;
		if(angle2 > 360)
			angle2 = 0;


		Quaternion newRotation = _transform.localRotation;
		newRotation.eulerAngles = new Vector3 (Mathf.Sin (angle1 * Mathf.Deg2Rad) * rollingIntensity, newRotation.eulerAngles.y, Mathf.Sin (angle2 * Mathf.Deg2Rad) * rollingIntensity);
		_transform.localRotation = newRotation;


	}
}
