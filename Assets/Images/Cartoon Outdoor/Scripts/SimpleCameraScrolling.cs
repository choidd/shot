using UnityEngine;
using System.Collections;

public class SimpleCameraScrolling : MonoBehaviour {

	public float panSpeed = 60.0f;
	public Vector3 MaxScrollingPosition;
	public Vector3 MinScrollingPosition;

	Vector3 mouseClickPos;
	bool isPanning;
	Transform _transform;


	// Use this for initialization
	void Start () {
		isPanning = false;
		_transform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetMouseButtonDown(0)) {
			mouseClickPos = Input.mousePosition;
			isPanning = true;
		}
		if(Input.GetMouseButtonUp(0)) {
			isPanning = false;
		}


		if(isPanning) {

			Vector3 pos = Camera.main.ScreenToViewportPoint(Input.mousePosition - mouseClickPos);
			
			Vector3 moveX = new Vector3(pos.x * panSpeed, 0, 0); //Local
			Vector3 moveZ = new Vector3(pos.y * panSpeed, 0, 0); //World

			_transform.Translate(moveX*Time.deltaTime, Space.Self);
			_transform.Translate(-moveZ*Time.deltaTime, Space.World);

			if(_transform.position.z > MaxScrollingPosition.z)
				_transform.position = new Vector3(_transform.position.x, _transform.position.y, MaxScrollingPosition.z);
			else if(_transform.position.z < MinScrollingPosition.z)
				_transform.position = new Vector3(_transform.position.x, _transform.position.y, MinScrollingPosition.z);

			if(_transform.position.x > MaxScrollingPosition.x)
				_transform.position = new Vector3(MaxScrollingPosition.x, _transform.position.y, _transform.position.z);
			else if(_transform.position.x < MinScrollingPosition.x)
				_transform.position = new Vector3(MinScrollingPosition.x, _transform.position.y, _transform.position.z);

		}
	}
}
