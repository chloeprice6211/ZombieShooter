using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieSpawnerManager : MonoBehaviour
{
    public List<Enemy> zombies;
    public List<Transform> spawnPositions;

    float _elapsedTime = 0f;
    public float spawnRate;

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= spawnRate)
        {
            SpawnZombie();
            _elapsedTime = 0;
        }
    }

    void SpawnZombie()
    {
        Transform spawnPoint = spawnPositions[Random.Range(0, spawnPositions.Count)];
        Enemy zombie = Instantiate(zombies[Random.Range(0, zombies.Count)], spawnPoint.position, Quaternion.identity);
    }
}
