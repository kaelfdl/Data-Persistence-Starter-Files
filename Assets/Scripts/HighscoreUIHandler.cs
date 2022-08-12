using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class HighscoreUIHandler : MonoBehaviour
{

    [SerializeField] Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (DataManager.instance != null)
        {
            foreach (DataManager.Player player in DataManager.instance.playerHighscores)
            {
                highscoreText.text += $"\n{player.name} - {player.score}";
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Back()
    {
        SceneManager.LoadScene(0);
    }
}
