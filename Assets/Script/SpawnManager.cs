using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enemyPrefabs;
    public float enemyDist;
    private Vector2 position;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnWave", 1, 5);
    }

    public void SpawnWave()
    {
        position = new Vector2(Random.Range(-8, 8), -7.625f);
        Instantiate(enemyPrefabs, position, Quaternion.identity);
    }
}
