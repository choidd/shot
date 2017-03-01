using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class Gamemanager : MonoBehaviour, IListener {

    public Transform[] monsterpoints;
    public GameObject monsterPrefab;

    float createTime = 2.0f;
    public int maxMonster = 10;
    bool isGameOver = false;

    int killMonster = 0;
	// Use this for initialization
    
	void Start () {
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_ENEMY_DIE, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_STATE_WIN, this);
        EventManager.Instance.AddListener(EVENT_TYPE.GAME_STATE_LOSE, this);
        monsterpoints = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        if (monsterpoints.Length > 0)
        {
            StartCoroutine(CreateMonster());
        }
           
	}
	
    void Update()
    {
        if(killMonster == maxMonster)
        {
            EventManager.Instance.PoistNotification(EVENT_TYPE.GAME_STATE_WIN, this);
        }
    }

    IEnumerator CreateMonster()
    {
        while(!isGameOver)
        {
            int monsterCount = (int)GameObject.FindGameObjectsWithTag("Enemy").Length;

            if(monsterCount < maxMonster)
            {
                yield return new WaitForSeconds(createTime);

                int idx = Random.Range(1, monsterpoints.Length);
                Instantiate(monsterPrefab, monsterpoints[idx].position, monsterpoints[idx].rotation);
            }
            else
            {
                yield return null;
            }
        }
    }

    public void OnEvent(EVENT_TYPE Event_Type, Component Sender, object Param = null)
    {
        switch (Event_Type)
        {
            case EVENT_TYPE.GAME_ENEMY_DIE:
                Debug.Log(killMonster);
                killMonster++;
                break;
        }
    }
}
