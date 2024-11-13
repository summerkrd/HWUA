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
        _player.OnDead += Disable;

        Debug.Log("PlayerIsDead");
    }

    public override void Disable()
    {
        Completed?.Invoke();
    }

}
