using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.IO;

public class JSONManager : MonoBehaviour
{
    private struct PlayerData
    {
        public string name;
        public float score;
    }
    private List<PlayerData> leaderboardData = new List<PlayerData>();
    private string filePath;
    public Text Leaderboardtxt;
    void Start()
    {
        filePath = Application.dataPath + "/Save/PlayerData.csv";
        LoadPlayerData();
        UpdateLeaderboard();
    }

    public void AddPlayer(string name, float score)
{
    PlayerData newPlayer = new PlayerData();
    newPlayer.name = name;
    newPlayer.score = score;
    leaderboardData.Add(newPlayer);
    SavePlayerData(newPlayer);
    UpdateLeaderboard();
}


    public void UpdateLeaderboard()
    {
        leaderboardData.Sort((x, y) => y.score.CompareTo(x.score)); 
        Leaderboardtxt.text = "";
        for (int i = 0; i < Mathf.Min(10, leaderboardData.Count); i++)
        {
            Debug.Log("Rank " + (i + 1) + ": " + leaderboardData[i].name + " - " + leaderboardData[i].score);
            Leaderboardtxt.text += "Rank " + (i + 1) + ": " + leaderboardData[i].name + " - " + leaderboardData[i].score +" m" +"\n";
        }
    }

    private void LoadPlayerData()
    {
        if (File.Exists(filePath))
        {
            string[] lines = File.ReadAllLines(filePath);
            foreach (string line in lines)
            {
                string[] data = line.Split(',');
                if (data.Length == 2)
                {
                    PlayerData player = new PlayerData();
                    player.name = data[0];
                    player.score = float.Parse(data[1]);
                    leaderboardData.Add(player);
                }
            }
        }
    }

    private void SavePlayerData(PlayerData newPlayer)
{
    using (StreamWriter writer = new StreamWriter(filePath, true)) 
    {
        Debug.Log("" + newPlayer.name + " " + newPlayer.score + " m");
        writer.WriteLine(newPlayer.name + "," + newPlayer.score);
    }
}

}
