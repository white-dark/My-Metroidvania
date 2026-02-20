using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_AiredState : PlayerState
{
    public Player_AiredState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (input.moveInput.x != 0)
        {
            player.SetVelocity(input.moveInput.x * player.moveSpeed, rb.velocity.y);
            player.CheckFlip(input.moveInput.x);
        }

        if (player.isWall)
        {
            stateMachine.ChangeState(player.wallSlideState);
        }

        if (input.dashInput)
        {
            input.UseDashInput();
            stateMachine.ChangeState(player.dashState);
        }

        if (input.attackInput)
        {
            input.UseAttackInput();
            stateMachine.ChangeState(player.plungeAttackState);            
        }
    }
}
