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

        stateTimer = player.dashDuration;
        player.SetVelocity(player.dashSpeed * player.facingDir, 0);
        gravity = rb.gravityScale;
        rb.gravityScale = 0;
    }

    public override void Update()
    {
        base.Update();

        CancleDashIfNeed();

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
    }
    public override void Exit()
    {
        base.Exit();

        player.SetVelocity(rb.velocity.x / 2, rb.velocity.y);
        rb.gravityScale = gravity;
    }

    private void CancleDashIfNeed()
    {
        if (player.isWall)
        {
            if(player.isGround)
            {
                stateMachine.ChangeState(player.idleState);
            }
            else
            {
                stateMachine.ChangeState(player.wallSlideState);
            }
        }
    }
}
