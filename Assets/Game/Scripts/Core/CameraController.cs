using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [Header("Specs")]
    [SerializeField] private float followOffset = 7f;

    private void LateUpdate()
    {
        var newPos = transform.position;
        newPos.y = player.position.y - followOffset;
        transform.position = newPos;
    }
}
