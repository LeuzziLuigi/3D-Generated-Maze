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
    public GameObject loadingLeaderBoard;
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
        foreach (Transform child in rowsParent)
        {
            GameObject.Destroy(child.gameObject);
        }

        if (displayName == null)
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
        result.Leaderboard.Reverse();
        int position = 1;
        foreach (var item in result.Leaderboard)
        {
            GameObject newGO = Instantiate(rowPrefab, rowsParent);
            Text[] texts = newGO.GetComponentsInChildren<Text>();
            texts[0].text = (position).ToString();
            if(item.DisplayName == null)
                texts[1].text = item.PlayFabId;
            else
                texts[1].text = item.DisplayName;
            texts[2].text = StatValueToTime(item.StatValue);
            position++;
            //Debug.Log(item.Position + " " + item.PlayFabId + " " + item.StatValue);
        }
        Debug.Log("Leaderboard loaded");
    }

    private string StatValueToTime(int statValue)
    {
        string n = statValue.ToString();
        string result = "00:00:00";
        switch (n.Length)
        {
            case 1:
                result = "00:00:0" + n[0];
                break;
            case 2:
                result = "00:00:" + n[0] + n[1];
                break;
            case 3:
                result = "00:0" + n[0] + ":" + n[1] + n[2];
                break;
            case 4:
                result = "00:" + n[0] + n[1] + ":" + n[2] + n[3];
                break;
            case 5:
                result = "0" + n[0] + ":" + n[1] + n[2] + ":" + n[3] + n[4];
                break;
            case 6:
                result = n[0] + n[1] + ":" + n[2] + n[3] + ":" + n[4] + n[5];
                break;
        }
        return result;
    }

    void OnDataSend(UpdateUserDataResult request)
    {
        Debug.Log("Data Sent Correctly!");
    }

    void OnLeaderboardUpdate(UpdatePlayerStatisticsResult request)
    {
        Debug.Log("Sucesfull leaderboard sent");
        StartCoroutine(GetLeaderBoardDelay());
    }

    void OnDisplayNameUpdate(UpdateUserTitleDisplayNameResult result)
    {
        Debug.Log("Name updated");
        StartCoroutine(GetLeaderBoardDelay());
    }

    IEnumerator GetLeaderBoardDelay()
    {
        loadingLeaderBoard.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        loadingLeaderBoard.SetActive(false);
        GetLeaderboard();
    }

}
