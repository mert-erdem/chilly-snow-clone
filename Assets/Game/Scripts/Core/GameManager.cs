using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public static UnityAction ActionGameStart, ActionGameOver, ActionLevelPassed;

    private void Start()
    {
        ActionGameOver += RestartLevel;
        ActionLevelPassed += LoadNextLevel;
    }

    #region Action Methods

    private void RestartLevel()
    {
        StartCoroutine(RestartLevelRoutine());
    }
    private IEnumerator RestartLevelRoutine()
    {
        yield return new WaitForSeconds(1.5f);
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void LoadNextLevel()
    {
        StartCoroutine(LoadNextLevelRoutine());
    }
    private IEnumerator LoadNextLevelRoutine()
    {
        int nextLevel = PlayerPrefs.GetInt("LEVEL", 1);
        nextLevel++;
        PlayerPrefs.SetInt("LEVEL", nextLevel);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(nextLevel);
    }

    #endregion

    private void OnDisable()
    {
        ActionGameOver -= RestartLevel;
        ActionLevelPassed -= LoadNextLevel;
    }
}
