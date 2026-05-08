using System.IO;
using UnityEngine;

public class MainDataPersistence : MonoBehaviour
{
    public static MainDataPersistence Instance;

    public string PlayerName;
    public string BestPlayerName;
    public int HighScore;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadHighScore();
    }

    [System.Serializable]
    class SaveData
    {
        public string BestPlayerName;
        public int HighScore;
    }

    public void SaveHighScore(int score)
    {
        // Only save if it's a new high score
        if (score > HighScore)
        {
            HighScore = score;
            BestPlayerName = PlayerName;

            SaveData data = new SaveData();
            data.BestPlayerName = BestPlayerName;
            data.HighScore = HighScore;

            string json = JsonUtility.ToJson(data);
            File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
        }
    }

    public void LoadHighScore()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            BestPlayerName = data.BestPlayerName;
            HighScore = data.HighScore;
        }
    }
}