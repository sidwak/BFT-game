using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using GameAnalyticsSDK;

public class JSONSave : MonoBehaviour
{
    public PlayerData playerdata;
    public string dataFileName;

    public float lastCoins = 0f;

    private string path = "";
    private string persistentPath = "";

    void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
        

        SetPaths();
        //SavaData();
        if (File.Exists(persistentPath))            //change when deploying to android
        {
            Debug.Log("True");
            LoadData();
        }
        else
        {
            CreatePlayerData();
            SavaData();
            Debug.Log("False");
        }

        //SavaData();
        //LoadData();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void CreatePlayerData()
    {
        playerdata = new PlayerData(false, false, false, false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false, false, false, false,
            false, false, false, false, false, false, false, false, false, false, false, 0.025f, 61, 0, true, true);
    }

    private void SetPaths()
    {
        path = Application.dataPath + Path.AltDirectorySeparatorChar + dataFileName;
        persistentPath = Application.persistentDataPath + Path.AltDirectorySeparatorChar + dataFileName;
        Debug.Log(persistentPath);
    }

    public void SavaData()
    {
        string savePath = persistentPath;  //replace with peristentPath when deploying to android

        string json = JsonUtility.ToJson(playerdata);
        //Debug.Log(json);

        StreamWriter writer = new StreamWriter(savePath);
        writer.Write(json);
        writer.Close();

        if (playerdata.totalCoins - lastCoins == 0)
        {

        }
        else if (playerdata.totalCoins > lastCoins)
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Source, "Coins", (float)playerdata.totalCoins - lastCoins, "Buy", "1");
        }
        else
        {
            GameAnalytics.NewResourceEvent(GAResourceFlowType.Sink, "Coins", lastCoins - (float)playerdata.totalCoins, "Buy", "1");
        }
        
        lastCoins = playerdata.totalCoins;
    }

    public void LoadData()
    { 
        StreamReader reader = new StreamReader(persistentPath);   //replace with peristentPath when deploying to android
        string json = reader.ReadToEnd();
        reader.Close();
        PlayerData data = JsonUtility.FromJson<PlayerData>(json);
        playerdata = data;
        Debug.Log(data.ToString());

        lastCoins = playerdata.totalCoins;
    }
}
