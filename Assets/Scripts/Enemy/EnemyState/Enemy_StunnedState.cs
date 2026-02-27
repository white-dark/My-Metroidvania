using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_StunnedState : EnemyState
{
    public Enemy_StunnedState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        enemy.SetCounterable(false);

        stateTimer = enemy.stunnedDuration;
        // 让反击直接打飞对手
        enemy.SetVelocity(enemy.stunnedForce.x * -enemy.facingDir, enemy.stunnedForce.y);
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
