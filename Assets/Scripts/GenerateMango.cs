using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateMango : MonoBehaviour
{
    [SerializeField] private GameObject _mangoPrefab;
    private static int _mangoCount;
    private float _spawnTime;
    //private SpriteRenderer _treeRenderer;

    void Start()
    {
        _mangoCount = 0;
        _spawnTime = 0;
        for (int i = 0; i < 10; i++)
        {
            SpawnMango();
        }
    }

    void Update()
    {
        _spawnTime += Time.deltaTime;
        if(_spawnTime >= 1 && _mangoCount <= 10)
        {
            SpawnMango();
        }
    }


    private void SpawnMango()
    {
        Vector2 position = new Vector2(Random.Range(-4.65f, 4.4f), Random.Range(-1.06f, 2.59f));
        Instantiate(_mangoPrefab, position, Quaternion.identity);
        _mangoCount++;
        _spawnTime = 0;
    }

    /// <summary>
    /// Subtracts the number of mangos in the tree
    /// </summary>
    public static void SubtractMango()
    {
        _mangoCount--;
    }
}
