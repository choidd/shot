using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {

    public enum MONSTER_STATE {
        idle,
        walk,
        attack,
        trace,
        die
    };
    private Vector3 tarPos;
    private float minX, minZ, maxX, maxZ;
    private Animator anim;

    Ray ray;
    RaycastHit hit;
    Vector3 rayDirection;

    Transform playerTr;

    MONSTER_STATE current_state;
    
    private UnityEngine.AI.NavMeshAgent nav;

    // Use this for initialization
    void Start () {
        minX = 0.0f;
        maxX = 50.0f;

        minZ = 0.0f;
        maxZ = 30.0f;
        current_state = MONSTER_STATE.walk;
        playerTr = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        StartCoroutine(State_Control());
        anim = GetComponentInChildren<Animator>();
        GetNextPosition();

        //EventManager.Instance.AddListener(EVENT_TYPE.GAME_ENEMY_DIE, this);
    }

    void GetNextPosition()
    {
        tarPos = new Vector3(Random.Range(minX, maxX), 0.5f, Random.Range(minZ, maxZ));
    }

    // Update is called once per frame
    void Update () {
        OnDrawGizmos();
    }

    public IEnumerator State_Control() // 몬스터의 상태변화를 컨트롤합니다
    {
        while(true)
        {
            yield return new WaitForSeconds(.1f);
            switch (current_state)
            {
                case MONSTER_STATE.idle:
                    break;
                case MONSTER_STATE.walk:
                    anim.SetBool("isWalk", true);
                    rayDirection = playerTr.position - transform.position;
                    
                    if ((Vector3.Angle(rayDirection, transform.forward)) < 45) // 몬스터의 45도 시야안에 있고
                    {
                        current_state = MONSTER_STATE.trace; // 추격한다
                    }
                    if (Vector3.Distance(transform.position, tarPos) <= 0.5f)
                        GetNextPosition(); // 몬스터가 랜덤한위치로 이동완료했을때 다음위치를 찾는다
                    nav.SetDestination(tarPos);
                    break;
                case MONSTER_STATE.trace:
                    anim.SetBool("isTrace", true);
                    nav.SetDestination(playerTr.position); 
                    if(Vector3.Distance(transform.position, playerTr.position) < 5f) // 플레이어와 몬스터의 위치가 가까우면
                    {
                        current_state = MONSTER_STATE.attack; // 어택모드
                    }
                    break;
                case MONSTER_STATE.attack:
                    anim.SetBool("isAttack", true);
                    attack();
                    break;
            }
        }
    }

    void attack()
    {
        int randomNumber = Random.Range(0, 9);
        EventManager.Instance.PoistNotification(EVENT_TYPE.GAME_ENEMY_ATTACK, this);
        if (randomNumber == 0)
        {
            // 플레이어 타격 이벤트 호출
            EventManager.Instance.PoistNotification(EVENT_TYPE.GAME_PLAYER_HEALTH_CHANGE, this, 2);
            EventManager.Instance.PoistNotification(EVENT_TYPE.GAME_PLAYER_DAMAGED, this);
        }

        if (Vector3.Distance(transform.position, playerTr.position) > 14f)
        {
            current_state = MONSTER_STATE.walk;
        }
    }

    void OnDrawGizmos()
    {
        if (playerTr == null) return;
        Debug.DrawLine(transform.position, playerTr.position, Color.red);
        Vector3 frontRayPoint = transform.position + (transform.forward * 100);

        Vector3 leftRayPoint = frontRayPoint;
        leftRayPoint.x += 45 * 0.5f;

        Vector3 rightRayPoint = frontRayPoint;
        rightRayPoint.x += 45 * 0.5f;

        Debug.DrawRay(transform.position, frontRayPoint, Color.green);
        Debug.DrawRay(transform.position, leftRayPoint, Color.green);
        Debug.DrawRay(transform.position, rightRayPoint, Color.green);
    }
    
}
