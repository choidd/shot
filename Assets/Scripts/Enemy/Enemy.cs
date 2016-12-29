using UnityEngine;
using System.Collections;

// 적의 이동 및 인공지능
public class Enemy : MonoBehaviour {

    private Vector3 tarPos;
    private NavMeshAgent nav;
    private float minX, minZ, maxX, maxZ;
    private Animator anim;
	// Use this for initialization
	void Start () {
        minX = -20.0f;
        maxX = 20.0f;

        minZ = -20.0f;
        maxZ = 20.0f;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
	}
	
    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

	// Update is called once per frame
    
	void Update () {

        if (Vector3.Distance(transform.position, tarPos) <= 0.5f)
            GetNextPosition();
        else
            nav.SetDestination(tarPos);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name.Equals("attackDistance"))
        {
            die();
        }
    }

    void die()
    {
        anim.SetTrigger("Death");
        nav.Stop();

    }
}
