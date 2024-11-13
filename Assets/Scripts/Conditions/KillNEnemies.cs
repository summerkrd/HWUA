using System;
using UnityEngine;

public class KillNEnemies : Conditions
{
    public override event Action Completed;

    private int _needKillEnemies = 5;
    private int _killedEnemies = 0;

    private GameManager _gameManager;
     
    public KillNEnemies(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    public override void Start()
    {
        _gameManager.KillEnemy += () => _killedEnemies++;

        Debug.Log("KillNEnemies");
    }

    private void OnKilledNeedsEnemies()
    {
        if (_killedEnemies > _needKillEnemies)
            Completed?.Invoke();
    }

    public override void Disable()
    {
        _gameManager.KillEnemy -= () => _killedEnemies++;
    }
}
