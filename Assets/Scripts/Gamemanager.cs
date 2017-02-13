using UnityEngine;
using System.Collections;
using HedgehogTeam.EasyTouch;

public class Gamemanager : MonoBehaviour {

    public Transform[] monsterpoints;
    public GameObject monsterPrefab;

    float createTime = 2.0f;
    int maxMonster = 10;
    bool isGameOver = false;
	// Use this for initialization
	void Start () {
        monsterpoints = GameObject.Find("SpawnPoint").GetComponentsInChildren<Transform>();

        if (monsterpoints.Length > 0)
        {
            StartCoroutine(CreateMonster());
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
}
