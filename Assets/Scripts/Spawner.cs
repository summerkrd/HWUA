using System;
using UnityEngine;
using Action = System.Action;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

public class Spawner
{
    public event Action OnEnemySpawn;

    private Player _playerPrefab;
    private Enemy _enemyPrefab;

    private Transform[] _spawnPoints;

    private float _respawnTime;

    public Spawner(Player playerPrefab, Enemy enemyPrefab, Transform[] spawnPoints, float respawnTime)
    {
        _playerPrefab = playerPrefab;
        _enemyPrefab = enemyPrefab;
        _spawnPoints = spawnPoints;
        _respawnTime = respawnTime;
    }

    public Player SpawnPlayer()
    {
        Player player = Object.Instantiate(_playerPrefab, new Vector3(0f, 0.3f, 0), Quaternion.identity);
        return player;
    }

    public float SpawnEnemies(float timer, out Enemy enemy)
    {
        enemy = null;
        timer += Time.deltaTime;

        if (timer >= _respawnTime)
        {
            enemy = Object.Instantiate(_enemyPrefab, RandomSpawnPoint(), Quaternion.identity);

            OnEnemySpawn?.Invoke();

            timer = 0;
        }

        return timer;
    }

    private Vector3 RandomSpawnPoint()
    {
        int randomIndex = Random.Range(0, _spawnPoints.Length);
        return _spawnPoints[randomIndex].position;
    }
}
