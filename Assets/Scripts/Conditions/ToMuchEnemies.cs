using System;
using System.Collections.Generic;
using UnityEngine;

public class ToMuchEnemies : Conditions
{
    public override event Action Completed;

    private List<Enemy> _enemyList;

    private int _enemyMaxCount = 5;

    public ToMuchEnemies(List<Enemy> enemyList)
    {
        _enemyList = enemyList;
    }

    public override void Start()
    {
        Debug.Log("ToMuchEnemies");
    }

    public override void Disable()
    {
        if (_enemyList.Count >= _enemyMaxCount)
            Completed?.Invoke();
    }
}
