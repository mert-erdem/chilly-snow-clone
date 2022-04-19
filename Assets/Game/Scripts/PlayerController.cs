using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 5f, rotSpeed = 5f;
    private float currentRotZ = 40f;


    private void Start()
    {
        GameManager.ActionGameOver += DisableThis;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            currentRotZ *= -1;

        Move();
    }

    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, currentRotZ), rotSpeed * Time.deltaTime);
        transform.Translate(-transform.up * speed * Time.deltaTime, Space.World);
    }

    private void DisableThis()
    {
        this.enabled = false;
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
        GameManager.ActionGameOver -= DisableThis;
    }
}
