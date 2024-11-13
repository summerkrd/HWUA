using System;
using UnityEngine;

public class PlayerIsDead : Conditions
{
    public override event Action Completed;

    private Player _player;

    public PlayerIsDead(Player player)
    {
        _player = player;
    }

    public override void Start()
    {
        _player.OnDead += OnPlayerDead;

        Debug.Log("PlayerIsDead");
    }
    private void OnPlayerDead() => Completed?.Invoke();

    public override void Disable()
    {
        _player.OnDead -= OnPlayerDead;
    }

}
