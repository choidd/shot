using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames;
using UnityEngine.UI;
using UnityEngine.SocialPlatforms;
using UnityEngine.SceneManagement;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;


public class GoogleManager : MonoBehaviour {
    
	// Use this for initialization
	void Awake () {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().EnableSavedGames().Build();
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
                Debug.Log("login error");
        });
    }

    private bool CheckLogin()
    {
        return Social.localUser.authenticated;
    }

    void SaveToCloud()
    {
        if(!CheckLogin())
        {
            login();
        }


    }

    private void OpenSavedGame(string filename, bool bSave)
    {
        ISavedGameClient saveGameClient = PlayGamesPlatform.Instance.SavedGame;

        if (bSave) // 저장
        {
            saveGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToSave);

        }
        else // 로딩
        {
            saveGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
                ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpenedToRead);
        }
    }

    static void OnSavedGameOpenedToSave(SavedGameRequestStatus status, ISavedGameMetadata game)
    {

        if (status == SavedGameRequestStatus.Success)
        {
            //파일이 준비되었습니다. 실제 게임 저장을 수행합니다.
            //저장할데이터바이트배열에 저장하실 데이터의 바이트 배열을 지정합니다.
            SaveGame(game, "저장할데이터바이트배열", DateTime.Now.TimeOfDay);
        }
        else
        {
            Debug.Log("save error");
        }
    }

    static void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();

        builder = builder

            .WithUpdatedPlayedTime(totalPlaytime)

            .WithUpdatedDescription("Saved game at " + DateTime.Now);


        /*

        if (savedImage != null)

        {

            // This assumes that savedImage is an instance of Texture2D

            // and that you have already called a function equivalent to

            // getScreenshot() to set savedImage

            // NOTE: see sample definition of getScreenshot() method below

            byte[] pngData = savedImage.EncodeToPNG();

            builder = builder.WithUpdatedPngCoverImage(pngData);

        }*/


        SavedGameMetadataUpdate updatedMetadata = builder.Build();

        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);

    }
    static void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)

    {




        if (status == SavedGameRequestStatus.Success)

        {

            //데이터 저장이 완료되었습니다.

        }

        else

        {

            //데이터 저장에 실패 했습니다.

        }

    }


    //----------------------------------------------------------------------------------------------------------------

    //클라우드로 부터 파일읽기

    public static void LoadFromCloud()

    {

        if (!CheckLogin())

        {

            //로그인되지 않았으니 로그인 루틴을 진행하던지 합니다.

            return;

        }


        //내가 사용할 파일이름을 지정해줍니다. 그냥 컴퓨터상의 파일과 똑같다 생각하시면됩니다.

        OpenSavedGame("사용할파일이름", false);

    }



    static void OnSavedGameOpenedToRead(SavedGameRequestStatus status, ISavedGameMetadata game)

    {

        if (status == SavedGameRequestStatus.Success)

        {

            // handle reading or writing of saved game.

            LoadGameData(game);

        }

        else

        {

            //파일열기에 실패 한경우, 오류메시지를 출력하던지 합니다.

        }

    }


    //데이터 읽기를 시도합니다.

    static void LoadGameData(ISavedGameMetadata game)

    {

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);

    }


    static void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)

    {

        if (status == SavedGameRequestStatus.Success)

        {

            // handle processing the byte array data


            //데이터 읽기에 성공했습니다.

            //data 배열을 복구해서 적절하게 사용하시면됩니다.

        }

        else

        {

            //읽기에 실패 했습니다. 오류메시지를 출력하던지 합니다.

        }

    }

}
