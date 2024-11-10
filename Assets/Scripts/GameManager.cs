using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private Enemy _enemyPrefab;

    private Transform[] _spawnPoints;
    private CinemachineVirtualCamera _camera;

    private Player _player;
    private Enemy _lastSpawnedEnemy;
    private List<Enemy> _enemyList;

    private WinConditions _winConditions;
    private LoseConditions _loseConditions;

    private float _respawnTime = 5f;
    private float _timer = 0;
    private Spawner _spawner;

    private float _globalTimer = 0;

    private bool _playerIsDead;
    private int _killedEnemies = 0;

    private bool _isWin;
    private bool _isLose;

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
        _player.OnDead += () => _playerIsDead = true;
    }

    private void Update()
    {
        _globalTimer += Time.deltaTime;

        _timer = _spawner.SpawnEnemies(_timer, out _lastSpawnedEnemy);

        CheckWinAndLoseConditions();

        if (_isWin)
        {
            Time.timeScale = 0f;
            Debug.Log("You WIN");
        }

        if (_isLose)
        {
            Time.timeScale = 0f;
            Debug.Log("You LOSE");
        }

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
        _killedEnemies++;        
    }        

    private void CheckWinAndLoseConditions()
    {
        switch (_winConditions)
        {
            case WinConditions.LiveNSeconds:
                if (_globalTimer > 30) 
                    _isWin = true;
                Debug.Log("_globalTimer " + _globalTimer);
                break;

            case WinConditions.KillNEnemies:
                if (_killedEnemies >= 5)
                    _isWin = true;
                Debug.Log("killedEnemies " + _killedEnemies);
                break;
        }

        switch(_loseConditions)
        {
            case LoseConditions.PlayerIsDead:
                if (_playerIsDead)
                    _isLose = true;
                break;

            case LoseConditions.ToMuchEnemies:
                if (_enemyList.Count >= 5)
                    _isLose = true;
                Debug.Log("Количество врагов " + _enemyList.Count);
                break;
        }
    }            
}
