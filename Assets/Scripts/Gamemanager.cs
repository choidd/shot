using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;


public class Gamemanager : MonoBehaviour, IListener {

    public Transform[] monsterpoints;
    public GameObject monsterPrefab;

    float createTime = 2.0f;
    public int maxMonster = 10;
    bool isGameOver = false;
    
    int killMonster = 0;

    public static Gamemanager Instance
    {
        get { return instance; }
        set { }
    }

    public static Gamemanager instance = null;

    // Use this for initialization
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }
    }

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
            Debug.Log("success");
            EventManager.Instance.PoistNotification(EVENT_TYPE.GAME_STATE_WIN, this);
            killMonster = 0;
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
                killMonster++;
                break;
            case EVENT_TYPE.GAME_STATE_WIN:
                //GoogleManager.Instance.PostingScoreToLeaderBoard(killMonster * 100);
                SceneManager.LoadScene("main");
                break;
        }
    }
}
