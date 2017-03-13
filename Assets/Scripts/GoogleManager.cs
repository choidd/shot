using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class GoogleManager : MonoBehaviour {
    
    public static GoogleManager Instance
    {
        get { return instance; }
        set { }
    }

    public static GoogleManager instance = null;
    
    // Use this for initialization
    void Awake () {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            DestroyImmediate(this);
        }

        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
            .EnableSavedGames()
            .Build();
        PlayGamesPlatform.InitializeInstance(config);

        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
	}
	
    public void login()
    {        
        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                SceneManager.LoadScene("main");
            }
            else
            { 
                Debug.Log("login error");
            }
        });
    }

    private bool CheckLogin()
    {
        return Social.localUser.authenticated;
    }


    private void UnlockingAchievement() //업적달성
    {
        if(CheckLogin())
        {
            PlayGamesPlatform.Instance.IncrementAchievement("CgkIqZHOwegXEAIQAQ", 5, (bool success) =>
            {
                // handle success or failure
            });
        }
        else
        {
            Debug.Log("login error");
        }
    }

    public void PostingScoreToLeaderBoard(int score)
    {
        Social.ReportScore(score, "CgkIqZHOwegXEAIQBg", (bool success) =>
        {
            if (success)
            { 
                Debug.Log("leader board success");
                ((PlayGamesPlatform)Social.Active).ShowLeaderboardUI("CgkIqZHOwegXEAIQBg");
                //Social.ShowLeaderboardUI();
            }
        });
    }
    

    public void SaveToCloud()
    {
        if(!CheckLogin())
        {
            return;
        }
        else
        {
            OpenSavedGame("savefile", true);
        }
    }

    void OpenSavedGame(string filename, bool bSave)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        if(bSave) // 세이브
        {
            savedGameClient.OpenWithAutomaticConflictResolution(filename,
                DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                OnSavedGameOpenedToSave);
        }
        else // 로드
        {
            savedGameClient.OpenWithAutomaticConflictResolution(filename,
                DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                OnSavedGameOpenedToRead);
        }
    }

    void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            //게임 저장 수행
            SaveGame(game, 바이트배열, DateTime.Now.TimeOfDay);
        }
        else
        {
            // 파일 열기 실패
        }
    }

    void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

    }
    
    /// <summary>
    /// savegame
    /// </summary>
    /// <param name="game"></param>
    /// <param name="savedData"></param>
    /// <param name="totalPlaytime"></param>
    /// 
    
    public void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);

        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }
    /*
    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("saved complete");
            // handle reading or writing of saved game.
        }
        else {
            // handle error
        }
    }

    public void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
        }
        else {
            // handle error
        }
    }
    */

}