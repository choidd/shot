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
                PopupManager.Instance.ShowPopup("success", "google");
//PlayerData.Instance.loadLocal();
                SceneManager.LoadScene("MainMenu");
            }
            else
            {
                Debug.Log("login error");
                PopupManager.Instance.ShowPopup("error", "google");
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
    
    // 파일 오픈
    void OpenSavedGame(string filename)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename,
            DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime,
            OnSavedGameOpened);
    }

    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success) // open success
        {
        }
        else // open fail
        {
            
        }
    }

    //세이브
    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
       
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
        }
        else {
            // handle error
        }
    }

    //로드
    void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle processing the byte array data
        }
        else {
            // handle error
        }
    }
    
}