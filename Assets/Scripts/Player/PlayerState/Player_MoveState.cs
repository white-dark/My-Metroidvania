using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player_MoveState : Player_GroundedState
{
    public Player_MoveState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        if (input.moveInput.x == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        player.CheckFlip(input.moveInput.x);
    }

    public override void PhysicsUpdate()
    {
        base.PhysicsUpdate();

        player.SetVelocity(input.moveInput.x * player.moveSpeed, rb.velocity.y);
    }
}
