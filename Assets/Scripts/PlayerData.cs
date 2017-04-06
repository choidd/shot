using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using GooglePlayGames;
public enum Stat
{
    id,
    level,
    heart_cnt
}

public class PlayerData : MonoBehaviour {

    public static PlayerData Instance
    {
        get { return instance; }
        set { }
    }
    public static PlayerData instance = null;

    
    private string savedData;

    public string id;
    public int level;
    public int heart_cnt;

    bool isSaving = false;

    public float LastSave { set; get; }
    public float PlayTimeSinceSave { get; set; }
    public float totalPlayTime { get
        {return (Time.time - LastSave) + PlayTimeSinceSave;} }

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
    
    public void loadLocal()
    {
        loadCloud();
        PlayTimeSinceSave = PlayerPrefs.GetFloat("totalPlayTime");
        id = PlayerPrefs.GetString("id");
        level = PlayerPrefs.GetInt("level");
        heart_cnt = PlayerPrefs.GetInt("heart_cnt");
    }

    public void saveLocal()
    {
        PlayerPrefs.SetFloat("totalPlayTime", totalPlayTime);
        LastSave = Time.time;
        PlayerPrefs.SetString("SaveString", getSavingString());
        saveCloud();
        //PlayerPrefs.SetString("id", id);
        //PlayerPrefs.SetInt("level", level);
        //PlayerPrefs.SetInt("heart_cnt", heart_cnt);
    }

    public void loadCloud()
    {
        isSaving = false;
        ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution
                ("KillerSave",
                GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                SavedGameOpened);
    }

    public void saveCloud()
    {
        if(Social.localUser.authenticated)
        {
            isSaving = true;
            ((PlayGamesPlatform)Social.Active).SavedGame.OpenWithAutomaticConflictResolution
                ("KillerSave",
                GooglePlayGames.BasicApi.DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime,
                SavedGameOpened);
        }
        else
        {
            //로그인이나 해라
        }
    }

    public void SavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            if(isSaving) // writting data
            {
                byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes(getSavingString());
                TimeSpan playedTime = TimeSpan.FromSeconds(totalPlayTime);
                SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder()
                    .WithUpdatedPlayedTime(playedTime)
                    .WithUpdatedDescription("Saved Game at " + DateTime.Now);

                SavedGameMetadataUpdate update = builder.Build();
                ((PlayGamesPlatform)Social.Active).SavedGame.CommitUpdate(game, update, data, SavedGameWritten);
            }
            else // reading data
            {
                ((PlayGamesPlatform)Social.Active).SavedGame.ReadBinaryData(game, SavedGameLoaded);
            }
        }
        else
        {

        }
    }

    public void SavedGameLoaded(SavedGameRequestStatus status, byte[] data)
    {
        if(status == SavedGameRequestStatus.Success)
        {
            LoadfromString(System.Text.ASCIIEncoding.ASCII.GetString(data));
        }
        else
        {
            
        }
    }
    
    public void SavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        Debug.Log(status);
    }
    private void LoadfromString(string sdata)
    {
        if (sdata == "")
            return;

        string[] tmp = sdata.Split('%');
        id = tmp[0];
        level = Convert.ToInt32(tmp[1]);
        heart_cnt = Convert.ToInt32(tmp[2]);
    }

    private string getSavingString()
    {
        string sData = "";

        // time
        sData += id.ToString() + '%' + level.ToString() + '%' + heart_cnt.ToString();

        return sData;
    }
}
