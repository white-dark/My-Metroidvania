using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_GroundedState : PlayerState
{
    public Player_GroundedState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (input.jumpInput)
        {
            input.UseJumpInput();
            stateMachine.ChangeState(player.jumpState);
        }

        if (!player.isGround)
        {
            stateMachine.ChangeState(player.fallState);
        }

        if (input.dashInput)
        {
            input.UseDashInput();
            stateMachine.ChangeState(player.dashState);
        }

        if (input.attackInput)
        {
            input.UseAttackInput();
            stateMachine.ChangeState(player.basicAttackState);
        }
    }
}
