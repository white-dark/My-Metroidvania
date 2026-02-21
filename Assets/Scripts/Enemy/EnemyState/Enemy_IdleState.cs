using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D;
using UnityEngine;

public class Enemy_IdleState : Enemy_GroundState
{
    public Enemy_IdleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 重置Idle倒计时
        stateTimer = enemy.idleTime;
    }

    public override void Update()
    {
        base.Update();

        // 倒计时结束开始巡逻
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.moveState);
        }
    }
}
