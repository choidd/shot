using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScript : MonoBehaviour {

    public Text ID;
    string strID;
	// Use this for initialization
	void Start ()
    {
        strID = "Google ID : " + Social.localUser.userName;
	}

    public void btn_start()
    {
        //SceneManager.LoadScene("city");
        
        //GoogleManager.Instance.PostingScoreToLeaderBoard(200);
    }
}
