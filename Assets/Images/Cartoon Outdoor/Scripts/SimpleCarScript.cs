using UnityEngine;
using System.Collections;

public class SimpleCarScript : MonoBehaviour {

	public Vector3 startPoint;
	public Vector3 endPoint;
	public float Speed = 3.0f;

	private Vector3 direction;
	private Transform _transform;

	// Use this for initialization
	void Start () {
		_transform = (Transform)GetComponent<Transform> ();
		direction = endPoint - startPoint;
		direction.Normalize ();
	}
	
	// Update is called once per frame
	void Update () {
		_transform.position += direction * Speed * Time.deltaTime; 

		if(Vector3.Distance(_transform.position, endPoint) < 1.0f) 
			_transform.position = startPoint;
	}
}
