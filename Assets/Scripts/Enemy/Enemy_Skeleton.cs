using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class Enemy_Skeleton : Enemy, ICounterable
{
    protected override void Awake()
    {
        base.Awake();

        idleState = new Enemy_IdleState(this, stateMachine, "idle");
        moveState = new Enemy_MoveState(this, stateMachine, "move");
        attackState = new Enemy_AttackState(this, stateMachine, "attack");
        battleState = new Enemy_BattleState(this, stateMachine, "battle");
        deathState = new Enemy_DeathState(this, stateMachine, "idle");
        stunnedState = new Enemy_StunnedState(this, stateMachine, "stunned");
    }

    public bool CanBeCountered() => canBeCountered;

    public void HandleCounter()
    {
        if(stateMachine.currentState == stunnedState)
        {
            return;
        }
        stateMachine.ChangeState(stunnedState);
    }
}
