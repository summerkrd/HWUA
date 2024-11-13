using System;
using UnityEngine;

public class LiveNSeconds : Conditions
{
    public override event Action Completed;

    private float _timer = 0;
    private float _timeToNeedForWin = 30f;

    public override void Start()
    {
        while (_timer <= _timeToNeedForWin)
        {

            _timer += Time.deltaTime;

            Debug.Log(_timer);
        }
    }

    public override void Disable()
    {
        if (_timer >= _timeToNeedForWin)
            Completed?.Invoke();
    }
}
