using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;

    public event Action KillEnemy;

    private Transform[] _spawnPoints;
    private CinemachineVirtualCamera _camera;

    private Player _player;
    private Enemy _lastSpawnedEnemy;
    private List<Enemy> _enemyList;

    private WinConditions _winConditions;
    private LoseConditions _loseConditions;

    private Conditions _loser;
    private Conditions _winner;

    private float _respawnTime = 5f;
    private float _timer = 0;
    private Spawner _spawner;    

    public void Init(CinemachineVirtualCamera camera, Transform[] spawnPoints, LoseConditions loseConditions, WinConditions winConditions)
    {
        _camera = camera;
        _spawnPoints = spawnPoints;
        _loseConditions = loseConditions;
        _winConditions = winConditions;
    }

    private void Start()
    {
        _spawner = new Spawner(_playerPrefab, _enemyPrefab, _spawnPoints, _respawnTime);
        _enemyList = new List<Enemy>();

        _player = _spawner.SpawnPlayer();

        _camera.Follow = _player.transform;

        _spawner.OnEnemySpawn += AddEnemyToList;

        switch (_winConditions)
        {
            case WinConditions.LiveNSeconds:
                _winner = new LiveNSeconds();
                _winner.Start();
                _winner.Completed += YouWin;
                break;

            case WinConditions.KillNEnemies:
                _winner = new KillNEnemies(this);
                _winner.Start();
                _winner.Completed += YouWin;
                break;
        }

        switch (_loseConditions)
        {
            case LoseConditions.PlayerIsDead:
                _loser = new PlayerIsDead(_player);
                _loser.Start();
                _loser.Completed += YouLose;
                break;

            case LoseConditions.ToMuchEnemies:
                _loser = new ToMuchEnemies(_enemyList);
                _loser.Start();
                _loser.Completed += YouLose;
                break;
        }        
    }

    private void Update()
    {
        _timer = _spawner.SpawnEnemies(_timer, out _lastSpawnedEnemy);
    }

    private void AddEnemyToList()
    {
        _enemyList.Add(_lastSpawnedEnemy);

        Debug.Log(_enemyList.Count);
        _lastSpawnedEnemy.Killed += RemoveEnemyFromList;
    }

    private void RemoveEnemyFromList(Enemy enemy)
    {
        _enemyList.Remove(enemy);
        KillEnemy?.Invoke();
    }

    private void YouWin()
    {
        Time.timeScale = 0;
        Debug.Log("YouWin");
    }
    private void YouLose()
    {
        Time.timeScale = 0;
        Debug.Log("YouLose");
    }
}
