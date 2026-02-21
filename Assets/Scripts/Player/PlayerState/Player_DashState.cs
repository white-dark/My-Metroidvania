using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_DashState : PlayerState
{
    public Player_DashState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    private float gravity;

    public override void Enter()
    {
        base.Enter();

        stateTimer = player.dashDuration;   // 重置dash倒计时
        player.SetVelocity(player.dashSpeed * player.facingDir, 0);
        gravity = rb.gravityScale;  // 记录当前重力
        rb.gravityScale = 0;        // dash期间取消重力
    }

    public override void Update()
    {
        base.Update();

        // Dash倒计时结束
        if (stateTimer < 0) 
        {
            if (player.isGround)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.fallState);
            }
        }

        // Dash遇到墙
        if (player.isWall)
        {
            if (player.isGround)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }

    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(rb.velocity.x / 2, rb.velocity.y);   // dash结束后缓冲一下
        rb.gravityScale = gravity;  // 重力回归
    }
}
