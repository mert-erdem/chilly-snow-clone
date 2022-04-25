using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class LevelGenerator : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject treePrefab;
    [SerializeField] private GameObject groundPrefab;
    [SerializeField] private Transform groundVisual;
    [SerializeField] private GameObject finishGround;
    [Header("Specs")]
    [SerializeField] [Tooltip("Ground amount")] private int parkourLength = 5;
    [SerializeField] [Tooltip("Per ground")] private int averageTreeAmount = 10;

    private float SizeX => groundVisual.localScale.x;
    private float SizeY => groundVisual.localScale.y;


    void Start() => Generate();

    private void Generate()
    {
        var spawnPosGround = new Vector2(0, -SizeY / 2 - 2);

        for (int i = 0; i < parkourLength; i++)
        {           
            Instantiate(groundPrefab, spawnPosGround, Quaternion.identity);
            GenerateTree(spawnPosGround);
            spawnPosGround.y -= groundVisual.localScale.y;
        }

        Instantiate(finishGround, spawnPosGround, Quaternion.identity);
    }

    private void GenerateTree(Vector2 spawnPosGround)
    {
        int treeAmount = averageTreeAmount - Random.Range(-3, 4);

        for (int j = 0; j < treeAmount; j++)
        {
            var spawnPosTree = Vector2.zero;
            // search a suitable pos for generating a tree
            while (true)
            {
                float posX = Random.Range(spawnPosGround.x - (SizeX / 2) + 2f, spawnPosGround.x + (SizeX / 2) - 2f);
                float posY = Random.Range(spawnPosGround.y - (SizeY / 2) + 2f, spawnPosGround.y + (SizeY / 2) - 2f);
                spawnPosTree = new Vector2(posX, posY);
                // area checking
                var nearbyObjects = Physics2D.OverlapCircleAll(spawnPosTree, 5f);

                if (!nearbyObjects.Any(x => x.CompareTag("Obstacle")))
                    break;
            }
            var tree = TreePool.Instance.GetObject();
            tree.position = spawnPosTree;
        }
    }
}
