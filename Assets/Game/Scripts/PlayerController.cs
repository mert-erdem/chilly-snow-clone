using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 5f, rotSpeed = 5f;
    private float currentRotZ = 40f;
    private bool canMove = false;


    private void Start()
    {
        GameManager.ActionGameStart += ChangeMoveState;
        GameManager.ActionGameOver += ChangeMoveState;        
    }

    void Update()
    {
        if (!canMove) return;

        if (Input.GetMouseButtonDown(0))
            currentRotZ *= -1;

        Move();
    }

    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, currentRotZ), rotSpeed * Time.deltaTime);
        transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);
    }

    private void ChangeMoveState()
    {
        canMove = !canMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Edge"))
        {
            // game over
            GameManager.ActionGameOver?.Invoke();
        }

        if (collision.CompareTag("PointArea"))
            PointManager.Instance.AddPoints();

        if (collision.CompareTag("Finish"))
            GameManager.ActionLevelPassed?.Invoke();
    }

    private void OnDisable()
    {
        GameManager.ActionGameStart -= ChangeMoveState;
        GameManager.ActionGameOver -= ChangeMoveState;
    }
}
