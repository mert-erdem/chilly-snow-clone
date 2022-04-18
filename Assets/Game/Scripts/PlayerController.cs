using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class PlayerController : MonoBehaviour
{
    [SerializeField] public float speed = 5f, rotSpeed = 5f;
    private float currentRotZ = 40f;


    void Start()
    {

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
}
