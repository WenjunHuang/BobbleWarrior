using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] spawnPoints;
    public GameObject alien;
    public int MaxAlienCount;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;

    private int _alienCount;
    private float _generatedSpawnTime;
    private float _currentSpawnTime;

    public void Start()
    {
        AdjustAliensPerSpawn();
    }

    public void Update()
    {
        AdjustSpawnTime();
        SpawnAliens();
    }

    private void SpawnAliens()
    {
        if (aliensPerSpawn > 0 && _alienCount < MaxAlienCount)
        {
            List<int> previousSpawnLocations = null;
            for (var i = 0; i < aliensPerSpawn && _alienCount < MaxAlienCount; i++, _alienCount++)
            {
                int point;
                (point, previousSpawnLocations) = GetNewAlienSpawnPoint(previousSpawnLocations);

                var newAlien = Instantiate(alien);
                newAlien.transform.position = spawnPoints[point].transform.position;
                newAlien.transform.LookAt(new Vector3(player.transform.position.x, transform.position.y,
                    player.transform.position.z));
                var alienScript = newAlien.GetComponent<Alien>();
                alienScript.Target = player.transform;

            }
        }
    }

    private (int, List<int>) GetNewAlienSpawnPoint(List<int> previousSpawnLocations)
    {
        previousSpawnLocations = previousSpawnLocations ?? new List<int>();
        int point;
        while (true)
        {
            var n = Random.Range(0, spawnPoints.Length - 1);
            if (previousSpawnLocations.Contains(n)) continue;

            previousSpawnLocations.Add(n);
            point = n;
            break;
        }
        return (point, previousSpawnLocations);
    }

    private void AdjustAliensPerSpawn()
    {
        if (aliensPerSpawn > spawnPoints.Length)
        {
            aliensPerSpawn = spawnPoints.Length - 1;
        }
        aliensPerSpawn = aliensPerSpawn > MaxAlienCount ? aliensPerSpawn - MaxAlienCount : aliensPerSpawn;
    }

    private void AdjustSpawnTime()
    {
        _currentSpawnTime += Time.deltaTime;
        if (_currentSpawnTime > _generatedSpawnTime)
        {
            _currentSpawnTime = 0;
        }

        _generatedSpawnTime = Random.Range(minSpawnTime, MaxAlienCount);
    }
}