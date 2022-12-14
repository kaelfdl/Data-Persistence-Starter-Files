using System.IO;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class DataManager : MonoBehaviour
{

    public static DataManager instance;

    public string playerName;
    public string highscorePlayerName;
    public int highscore;
    public List<Player> playerHighscores;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        playerHighscores = new List<Player>();
        LoadHighscore();
    }

    void Start()
    {
    }

    [System.Serializable]
    public class Player
    {
        public string name;
        public int score;

    }
    [System.Serializable]
    public class HighscoreData
    {
        public Player[] scores;
    }

    public void SaveHighscore()
    {
        Player player = new Player();
        player.name = highscorePlayerName;
        player.score = highscore;
        HighscoreData data = new HighscoreData();
        data.scores = new Player[10];
        if (playerHighscores.Count > 0)
        {

            data.scores = playerHighscores.OrderByDescending(c => c.score).Take(10).ToArray();
        }
        data.scores[9] = player;

        string json = JsonHelper.ToJson(data.scores);
        Debug.Log(json);
        File.WriteAllText(Application.persistentDataPath + "/highscores.json", json);
    }

    public void LoadHighscore()
    {
        string path = Application.persistentDataPath + "/highscores.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);

            Player[] data = JsonHelper.FromJson<Player>(json);
            // Debug.Log(data);
            if (data != null)
            {
                playerHighscores = data.OrderByDescending(c => c.score).ToList();
                highscorePlayerName = playerHighscores[0].name;
                highscore = playerHighscores[0].score;
            }
            else
            {
                highscorePlayerName = "-";
                highscore = 0;
            }

        }
    }
}

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    class Wrapper<T>
    {
        public T[] Items;
    }
}