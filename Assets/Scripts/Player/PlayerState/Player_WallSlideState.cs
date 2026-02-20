using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_WallSlideState : PlayerState
{
    public Player_WallSlideState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        player.SetVelocity(input.moveInput.x, rb.velocity.y * player.slideSpeed);

        if (player.isGround)
        {
            stateMachine.ChangeState(player.idleState);
            player.Flip();
        }

        if (!player.isWall)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (input.jumpInput)
        {
            input.UseJumpInput();
            stateMachine.ChangeState(player.wallJumpState);
        }
    }
}
