using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif
public class MenuUIHandler : MonoBehaviour
{

    [SerializeField] InputField inputField;

    [SerializeField] Text highscoreText;
    public GameObject errorText;

    void Start()
    {
        highscoreText.text = $"Highscore\n{DataManager.instance.highscorePlayerName}: {DataManager.instance.highscore}";
    }

    public void StartGame()
    {
        if (inputField.text != "")
        {
            DataManager.instance.playerName = inputField.text;
            SceneManager.LoadScene(1);
        }
        else
        {
            errorText.SetActive(true);
        }
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
    Application.Quit();
#endif
    }

    public void ShowHighscoreScene()
    {
        SceneManager.LoadScene(2);
    }

}
