using UnityEngine;
using System.Collections;

public class WindmillRotation : MonoBehaviour {

	public float rotationSpeed = 50.0f;

	private Transform _transform;
	// Use this for initialization
	void Start () {
		_transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		_transform.Rotate (new Vector3 (-rotationSpeed*Time.deltaTime, 0.0f, 0.0f));
	}
}
