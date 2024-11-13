using System;
using System.Collections;
using UnityEngine;

public class LiveNSeconds : Conditions
{
    public override event Action Completed;

    private GameManager _gameManager;

    public LiveNSeconds(GameManager gameManager)
    {
        _gameManager = gameManager;
    }

    private float _timeToNeedForWin = 30f;

    public override void Start()
    {
        _gameManager.StartCoroutine(OnCheckTime());
    }

    private IEnumerator OnCheckTime()
    {
        yield return new WaitForSecondsRealtime(_timeToNeedForWin);

        Completed?.Invoke();
    }

    public override void Disable()
    {

    }
}
