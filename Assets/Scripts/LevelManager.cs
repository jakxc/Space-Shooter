using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] float sceneLoadDelay = 2f;
    
    ScoreKeeper scoreKeeper;

    string GAME_PLAY = "Gameplay";
    string MAIN_MENU = "MainMenu";
    string GAME_OVER = "GameOver";

    void Awake()
    {
        scoreKeeper = FindObjectOfType<ScoreKeeper>();
    }

    public void LoadGame()
    {
        // Reset player score to 0 when gameplay is loaded in
        scoreKeeper.ResetScore();
        
        SceneManager.LoadScene(GAME_PLAY);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(MAIN_MENU);
    }

    public void LoadGameOver()
    {
        StartCoroutine(WaitAndLoad(GAME_OVER, sceneLoadDelay));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    IEnumerator WaitAndLoad(string sceneName, float delay)
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene(sceneName);
    }
}
