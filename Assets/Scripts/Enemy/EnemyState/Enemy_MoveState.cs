using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_MoveState : Enemy_GroundState
{
    public Enemy_MoveState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        // 前面不是地面或者前面有墙就该转身再移动
        if (!enemy.isGround || enemy.isWall)
        {
            enemy.Flip();
        }
    }

    public override void Update()
    {
        base.Update();

        // 前面没路了或者前面有墙就该歇会了
        if (!enemy.isGround || enemy.isWall)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        enemy.SetVelocity(enemy.moveSpeed * enemy.facingDir, rb.velocity.y);
    }
}
