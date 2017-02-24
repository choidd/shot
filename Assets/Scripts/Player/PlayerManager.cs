using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour, IListener {
    public enum PLAYER_STATE
    {
        IDLE, WALK, ATTACK, CHASE
    };

    PLAYER_STATE current_state = PLAYER_STATE.IDLE;
    
    Ray ray;
    RaycastHit hit;
    UnityEngine.AI.NavMeshAgent nav;

    float h, v;
    public float moveSpeed = 10.0f;
    public float rotSpeed = 100.0f;

    Animator anim;

    public GameObject Bullet;
    private Transform gun;
    // Use this for initialization
    void Start () {
        nav = GetComponent<UnityEngine.AI.NavMeshAgent>();
        anim = GetComponentInChildren<Animator>();
        gun = GameObject.Find("attackArea").transform;
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_DAMAGED, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_PLAYER_ATTACK, this);

        StartCoroutine(State_Control());
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetFloat("Speed", (h * h + v * v));
        if (h != 0f && v != 0f)
        {
            transform.Translate(Vector3.forward * moveSpeed * v * Time.deltaTime, Space.Self);
            transform.Rotate(Vector3.up * Time.deltaTime * rotSpeed * h);
        }
    }
   
    public IEnumerator State_Control()
    {
        while(true)
        {
            yield return new WaitForSeconds(.1f);
            switch (current_state)
            {
                case PLAYER_STATE.IDLE:
                    break;
                case PLAYER_STATE.WALK:
                    break;
                case PLAYER_STATE.CHASE:
                    break;
                case PLAYER_STATE.ATTACK:
                    break;
            }
            
        }

    }
    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch(Event_Type)
        {
            case EVENT_TYPE.GAME_PLAYER_DAMAGED:
                //anim.SetTrigger("damaged");
                break;
            case EVENT_TYPE.GAME_PLAYER_ATTACK:
                Instantiate(Bullet, gun.position, gun.rotation);
                break;
        }
    }

    public void OnStickChanged(Vector2 stickPos)
    {
        h = stickPos.x;
        v = stickPos.y;
    }
}
