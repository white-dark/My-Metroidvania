using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_PlungeAttackState : PlayerState
{
    public Player_PlungeAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    private bool hasGrounded;

    public override void Enter()
    {
        base.Enter();

        hasGrounded = false;
    }

    public override void Update()
    {
        base.Update();

        if (player.isGround && hasGrounded == false)
        {
            hasGrounded = true;
            anim.SetTrigger("AirAttackTrigger");
            player.SetVelocity(0, rb.velocity.y);
        }

        if (triggerCalled && hasGrounded)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
