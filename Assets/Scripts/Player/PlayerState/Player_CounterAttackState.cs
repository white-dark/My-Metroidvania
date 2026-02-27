using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CounterAttackState : PlayerState
{
    private Player_Combat combat;

    public Player_CounterAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
        combat = player.GetComponent<Player_Combat>();
    }

    public override void Enter()
    {
        base.Enter();

        player.SetVelocity(0, 0);
        stateTimer = .5f;

        if (combat.PerformCounterAttack())
        {
            anim.SetBool("counterAttackPerformed", true);
            stateTimer = 1.5f;
        }
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        anim.SetBool("counterAttackPerformed", false);
    }
}
