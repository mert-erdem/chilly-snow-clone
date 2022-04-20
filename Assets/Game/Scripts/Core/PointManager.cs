using UnityEngine;

public class PointManager : Singleton<PointManager>
{    
    [SerializeField] private int pointPerTree = 100, pointDelta = 150, pointIdle = 5;
    private int currentPoint = 0;
    private float deltaPointIdle = 1f, timePointIdle = 0f;


    private void Start()
    {
        GameManager.ActionGameOver += ClearCurrentPoint;
        GameManager.ActionLevelPassed += SaveCurrentPoint;

        currentPoint = PlayerPrefs.GetInt("CURRENT_POINT", 0);
    }

    void Update()
    {
        if(Time.time > timePointIdle)
        {
            timePointIdle += deltaPointIdle;
            currentPoint += pointIdle;
            // update ui
            CanvasController.Instance.SetCurrentScoreText(currentPoint);
        }      
    }

    public void AddPoints()
    {
        currentPoint += pointPerTree;
        pointPerTree += pointDelta;
        // update ui
        CanvasController.Instance.SetCurrentScoreText(currentPoint);
    }

    // level completed action
    private void SaveCurrentPoint()
    {
        PlayerPrefs.SetInt("CURRENT_POINT", currentPoint);

        int highScore = PlayerPrefs.GetInt("HIGH_SCORE", 0);

        if (currentPoint > highScore)
            PlayerPrefs.SetInt("HIGH_SCORE", currentPoint);
    }

    // game over action
    private void ClearCurrentPoint()
    {
        PlayerPrefs.SetInt("CURRENT_POINT", 0);
        this.enabled = false;
    }

    private void OnDisable()
    {
        GameManager.ActionGameOver -= ClearCurrentPoint;
        GameManager.ActionLevelPassed -= SaveCurrentPoint;
    }
}
