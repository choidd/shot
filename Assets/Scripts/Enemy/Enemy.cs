using UnityEngine;
using System.Collections;

// 적의 이동 및 인공지능
public class Enemy : MonoBehaviour {

    public enum MonsterState { idle, walk, attack, trace, die };
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
    public MonsterState monsterstate = MonsterState.idle;

    bool CanSeePlayer = false;
    public GameObject blood1effect;

    Transform ThisTransform;
	// Use this for initialization
	void Start () {
        minX = -20.0f;
        maxX = 20.0f;

        minZ = -20.0f;
        maxZ = 20.0f;
        nav = GetComponent<NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        monsterstate = MonsterState.idle;
        ThisTransform = transform;
        GetNextPosition();
        StartCoroutine(State_Walk());
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

	// Update is called once per frame
    
	void Update () {
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
        Vector3 decalPos = ThisTransform.position + (Vector3.up * 0.05f);
        Quaternion decalRot = Quaternion.Euler(90, 0, Random.Range(0, 360));

        GameObject blood1 = (GameObject)Instantiate(blood1effect, decalPos, decalRot);
        float scale = Random.Range(1.5f, 3.5f);
        blood1.transform.localScale = Vector3.one * scale;

        Destroy(blood1, 3.0f);
    }

    IEnumerator State_Walk()
    {
        monsterstate = MonsterState.walk;
        anim.SetTrigger("Walk");
        nav.SetDestination(tarPos);
        while (monsterstate == MonsterState.walk)
        {
            rayDirection = player.transform.position - ThisTransform.position;

            Debug.DrawRay(ThisTransform.position, rayDirection);
            if ((Vector3.Angle(rayDirection, ThisTransform.forward)) < 45)
            {
                if (Physics.Raycast(ThisTransform.position, ThisTransform.forward, out hit))
                {
                    if (hit.collider.name == "Player")
                    {
                        monsterstate = MonsterState.trace;
                        StartCoroutine(State_Chase());
                        yield break;
                    }
                }
            }

            if (Vector3.Distance(ThisTransform.position, tarPos) <= 0.5f)
                GetNextPosition();
        }
        yield return null;
    }

    IEnumerator State_Chase()
    {
        monsterstate = MonsterState.trace;
        anim.SetTrigger("Chase");
        while (monsterstate == MonsterState.trace)
        {

        }
        nav.SetDestination(tarPos);
        yield return null;
    }
}
