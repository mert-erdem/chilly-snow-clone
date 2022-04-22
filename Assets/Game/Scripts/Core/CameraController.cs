using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform player;
    [Header("Specs")]
    [SerializeField] private float followOffset = 7f;


    private void Start()
    {
        GameManager.ActionLevelPassed += StopFollowing;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    private void FollowPlayer()
    {
        var newPos = transform.position;
        newPos.y = player.position.y - followOffset;
        transform.position = newPos;
    }

    private void StopFollowing()
    {
        this.enabled = false;
    }

    private void OnDisable()
    {
        GameManager.ActionLevelPassed -= StopFollowing;
    }
}
