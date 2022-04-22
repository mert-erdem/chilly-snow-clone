using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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

        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            currentRotZ *= -1;

        Move();
    }

    private void Move()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, currentRotZ), rotSpeed * Time.deltaTime);
        transform.Translate(speed * Time.deltaTime * -transform.up, Space.World);
    }

    private void ChangeMoveState()
    {
        canMove = !canMove;
    }

    private bool IsPointerOverUIObject()
    {
        var eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };
        var results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 1;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Obstacle") || collision.CompareTag("Edge"))
        {
            // game over
            GameManager.ActionGameOver?.Invoke();
        }

        if (collision.CompareTag("PointArea"))
            PointManager.Instance.AddPoints(transform.position);

        if (collision.CompareTag("Finish"))
            GameManager.ActionLevelPassed?.Invoke();
    }

    private void OnDisable()
    {
        GameManager.ActionGameStart -= ChangeMoveState;
        GameManager.ActionGameOver -= ChangeMoveState;
    }
}
