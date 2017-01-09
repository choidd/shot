using UnityEngine;
using System.Collections;

// 적의 이동 및 인공지능
public class Enemy : MonoBehaviour {

    public enum MonsterState { idle, attack, trace, die };
    private Vector3 tarPos;
    private NavMeshAgent nav;
    private float minX, minZ, maxX, maxZ;
    private Animator anim;
    private bool isDie = false;

    Ray ray;
    RaycastHit hit;
    Vector3 rayDirection;

    GameObject player;
    private float attackDist = 2.0f;
    public MonsterState monsterstate;

    public GameObject blood1effect;
	// Use this for initialization
	void Start () {
        minX = -20.0f;
        maxX = 20.0f;

        minZ = -20.0f;
        maxZ = 20.0f;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        monsterstate = MonsterState.idle;

        StartCoroutine(CheckMonsterState());
        StartCoroutine(MonsterAction());
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

	// Update is called once per frame
    
	void Update () {
        rayDirection = player.transform.position - transform.position;

        if ((Vector3.Angle(rayDirection, transform.forward)) < 45)
        {
            if (Physics.Raycast(transform.position, rayDirection, out hit))
            {
                if (hit.collider.name == "Player")
                {
                    monsterstate = MonsterState.trace;
                }
            }
        }
        Debug.Log(monsterstate);
    }

    void OnTriggerEnter(Collider coll)
    {
        if (coll.name.Equals("attackDistance"))
        {
            CreateBloodEffect();
            die();
        }
    }

    void die()
    {
        anim.SetTrigger("Death");
        nav.Stop();
        isDie = true;
        Destroy(gameObject, 2.0f);
    }

    void CreateBloodEffect()
    {
        Vector3 decalPos = transform.position + (Vector3.up * 0.05f);
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));

        GameObject blood1 = (GameObject)Instantiate(blood1effect, decalPos, decalRot);
        float scale = Random.Range(1.5f, 3.5f);
        blood1.transform.localScale = Vector3.one * scale;

        Destroy(blood1, 3.0f);
    }

    IEnumerator CheckMonsterState()
    {
        while(!isDie)
        {
            yield return new WaitForSeconds(0.2f);
            float dist = Vector3.Distance(transform.position, player.transform.position);
            if(dist <= attackDist)
            {
                monsterstate = MonsterState.attack;
            }
        }
    }

    IEnumerator MonsterAction()
    {
        while(!isDie)
        {
            switch (monsterstate)
            {
                case MonsterState.idle:
                    if (Vector3.Distance(transform.position, tarPos) <= 0.5f)
                        GetNextPosition();
                    else
                        nav.SetDestination(tarPos);
                    break;
                case MonsterState.attack:
                    break;
                case MonsterState.trace:
                    nav.SetDestination(player.transform.position);
                    break;
            }
            yield return null;
        }
    }
}
