using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    public GameObject[] spawnPoints;
    public GameObject alien;
    public GameObject upgradePrefab;
    public Gun gun;
    public float upgradeMaxTimeSpawn = 7.5f;
    public int MaxAlienCount;
    public float minSpawnTime;
    public float maxSpawnTime;
    public int aliensPerSpawn;

    private int _alienCount;
    private float _generatedSpawnTime;
    private float _currentSpawnTime;
    private bool _spawnedUpgrade = false;
    private float _actualUpgradeTime = 0;
    private float _currentUpgradeTime = 0;

    public void Start()
    {
        AdjustAliensPerSpawn();

        _actualUpgradeTime = Random.Range(upgradeMaxTimeSpawn - 3.0f,
            upgradeMaxTimeSpawn);
        _actualUpgradeTime = Mathf.Abs(_actualUpgradeTime);

    }

    public void Update()
    {
        _currentUpgradeTime += Time.deltaTime;
        if (_currentUpgradeTime > _actualUpgradeTime)
        {
            if (!_spawnedUpgrade)
            {
                var r = Random.Range(0, spawnPoints.Length - 1);
                var spawnLocation = spawnPoints[r];
                var upgrade = Instantiate(upgradePrefab);
                var upgradeScript = upgrade.GetComponent<Upgrade>();
                upgradeScript.gun = gun;
                upgrade.transform.position = spawnLocation.transform.position;

                _spawnedUpgrade = true;
                SoundManager.Instance.PlayOneShot(SoundManager.Instance.PowerUpAppear);
            }
        }
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