using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : Singleton<CanvasController>
{
    [SerializeField] private GameObject panelMenu, panelInGame;
    [SerializeField] private TextMeshProUGUI textHighScore, textCurrentScore, textGameOver;
    [Header("Pause Button")]
    [SerializeField] private Sprite spritePause;
    [SerializeField] private Sprite spriteResume;
    [Header("InGame Effects")]
    [SerializeField] private GameObject floatingPoint;


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

    public void PerformFloatingPoint(int point, Vector2 position)
    {
        var pointEffect = FloatingPointPool.Instance.GetObject();
        pointEffect.transform.position = position;
        pointEffect.floatingText.text = $"+{point}";
        FloatingPointPool.Instance.BringObject(pointEffect, 0.4f);// after the animation finishes
    }

    #region UI Button's Methods

    public void ButtonStartPressed()
    {
        GameManager.ActionGameStart?.Invoke();
    }

    public void ButtonPausePressed(Button button)
    {
        if(button.image.sprite == spritePause)
        {
            GameManager.Instance.Pause();
            button.image.sprite = spriteResume;

            return;
        }

        GameManager.Instance.Resume();
        button.image.sprite = spritePause;
    }

    #endregion

    private void OnDisable()
    {
        GameManager.ActionGameStart -= SetInGameUi;
        GameManager.ActionGameOver -= SetGameOverUi;
    }
}
