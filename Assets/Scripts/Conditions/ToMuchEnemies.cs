using System;
using System.Collections.Generic;
using UnityEngine;

public class ToMuchEnemies : Conditions
{
    public override event Action Completed;

    private int _enemyCount = 0;
    private int _enemyMaxCount = 5;

    private GameManager _gameManager;

    public ToMuchEnemies(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override void Start()
    {
        Debug.Log("ToMuchEnemies");

        _gameManager.SpawnNewEnemy += OnSpawnEnemy;
    }

    private void OnSpawnEnemy()
    {
        _enemyCount++;

        if (_enemyCount > _enemyMaxCount)
            Completed?.Invoke();
    }

    public override void Disable()
    {
        _gameManager.SpawnNewEnemy -= OnSpawnEnemy;
    }
}
