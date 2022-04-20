using TMPro;
using UnityEngine;

public class CanvasController : Singleton<CanvasController>
{
    [SerializeField] private GameObject panelMenu, panelInGame;
    [SerializeField] private TextMeshProUGUI textHighScore, textCurrentScore, textGameOver;


    private void Start()
    {
        GameManager.ActionGameStart += SetInGameUi;
        GameManager.ActionGameOver += SetGameOverUi;

        SetHighScoreText();
    }

    private void SetInGameUi()
    {
        panelMenu.SetActive(false);
        panelInGame.SetActive(true);
    }

    private void SetGameOverUi()
    {
        textGameOver.enabled = true;
    }

    private void SetHighScoreText()
    {
        int highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);
        textHighScore.text = $"Best: {highScore}";
    }

    public void SetCurrentScoreText(int currentScore)
    {
        textCurrentScore.text = $"Score: {currentScore}";
    }

    #region UI Button's Methods

    public void ButtonStartPressed()
    {
        GameManager.ActionGameStart?.Invoke();
    }

    #endregion

    private void OnDisable()
    {
        GameManager.ActionGameStart -= SetInGameUi;
        GameManager.ActionGameOver -= SetGameOverUi;
    }
}
