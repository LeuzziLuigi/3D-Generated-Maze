using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.UI;

public class PlayFabManager : MonoBehaviour
{
    
    public GameObject nameWindow;
    public GameObject leaderboardWindow;
    public InputField nameInput;
    
    public GameObject rowPrefab;
    public Transform rowsParent;
    private int gems { get; set; }
    private string displayName { get; set; } = null;

    void Start()
    {
        Login();
    }

    void Login()
    {
        var request = new LoginWithCustomIDRequest
        {
            CustomId = SystemInfo.deviceUniqueIdentifier,
            CreateAccount = true,
            InfoRequestParameters = new GetPlayerCombinedInfoRequestParams
            {
                GetPlayerProfile = true
            }
        };
        PlayFabClientAPI.LoginWithCustomID(request, OnSuccess, OnError);
    }

    void OnSuccess(LoginResult result)
    {
        Debug.Log("Succesfull login/account create!");
        LoadGems();
        string name = null;
        if(result.InfoResultPayload.PlayerProfile != null)
            name = result.InfoResultPayload.PlayerProfile.DisplayName;

        if (name != null)
            displayName = name;            
    }

    void OnError(PlayFabError error)
    {
        Debug.Log("Error while loggin in/creating account!");
        Debug.Log(error.GenerateErrorReport());
    }

    public void SendLeaderbord(int score)
    {
        var request = new UpdatePlayerStatisticsRequest
        {
            Statistics = new List<StatisticUpdate>
            {
                new StatisticUpdate
                {
                    StatisticName = "ScoreStandard",
                    Value = score
                }
            }
        };
        PlayFabClientAPI.UpdatePlayerStatistics(request, OnLeaderboardUpdate, OnError);
    }

    public void AddGems(int gemsNumber)
    {
        gems += gemsNumber;
        var request = new UpdateUserDataRequest
        {
            Data = new Dictionary<string, string>
            {
                {"Gems",  gems.ToString()}
            }
        };
        PlayFabClientAPI.UpdateUserData(request, OnDataSend, OnError);
    }

    public void SubmitNameButton()
    {
        var request = new UpdateUserTitleDisplayNameRequest
        {
            DisplayName = nameInput.text
        };
        displayName = nameInput.text;
        PlayFabClientAPI.UpdateUserTitleDisplayName(request, OnDisplayNameUpdate, OnError);
    }

    public void GetLeaderboard()
    {
        if(displayName == null)
        {
            nameWindow.SetActive(true);
            leaderboardWindow.SetActive(false);
        }
        else
        {
            nameWindow.SetActive(false);
            leaderboardWindow.SetActive(true);
            var request = new GetLeaderboardRequest
            {
                StatisticName = "ScoreStandard",
                StartPosition = 0,
                MaxResultsCount = 8
            };
            PlayFabClientAPI.GetLeaderboard(request, OnLeaderboardGet, OnError);
        }
        
    }

    public void LoadGems()
    {
        PlayFabClientAPI.GetUserData(new GetUserDataRequest(), OnDataRecieved, OnError);
    }

    public void OnDataRecieved(GetUserDataResult result)
    {
        if(result.Data != null && result.Data.ContainsKey("Gems"))
        {
            gems = int.Parse(result.Data["Gems"].Value);
            Debug.Log("Gems: " + gems);
        }
        else
        {
            Debug.Log("Could not load gems");
        }
    }

    void OnLeaderboardGet(GetLeaderboardResult result)
    {
        foreach (var item in result.Leaderboard)
        {
            GameObject newGO = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGO.GetComponentsInChildren<Text>();
            texts[0].text = (item.Position+1).ToString();
            if(item.DisplayName == null)
                texts[1].text = item.PlayFabId;
            else
                texts[1].text = item.DisplayName;
            texts[2].text = item.StatValue.ToString();
            //Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
    }

    void OnDataSend(UpdateUserDataResult request)
    {
        Debug.Log("Data Sent Correctly!");
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult request)
    {
        Debug.Log("Sucesfull leaderboard sent");
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Name updated");
        GetLeaderboard();
    }

}
