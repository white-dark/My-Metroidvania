using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DeathState : PlayerState
{
    public Player_DeathState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        rb.simulated = false;
    }
}
