using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public int damage = 20;
    public float speed = 2000.0f;
    
	// Use this for initialization
	void Start () {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
	}
	
	// Update is called once per frame
	void OnTriggerEnter(Collider coll)
    {
       // EventManager.Instance.AddListener(EVENT_TYPE.GAME_ENEMY_DIE, coll.gameObject.);
    }
}
