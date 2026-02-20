using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_BasicAttackState : PlayerState
{
    public Player_BasicAttackState(Player player, StateMachine stateMachine, string animBoolName) : base(player, stateMachine, animBoolName)
    {
    }

    private float attackDir;
    private float[] attackOffset => player.attackOffset;

    private int comboCounter;
    private int comboStart = 0;
    private int comboLimit = 2;

    private float lastAttackedTime;
    private float comboWindow = .4f;
    
    public override void Enter()
    {
        base.Enter();

        ResetCombo();

        anim.SetInteger("comboCounter", comboCounter);

        SetAttackOffset();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastAttackedTime = Time.time;
    }

    private void ResetCombo()
    {
        if (comboCounter > comboLimit)
        {
            comboCounter = comboStart;
        }

        if(Time.time > lastAttackedTime + comboWindow)
        {
            comboCounter = comboStart;
        }
    }

    private void SetAttackOffset()
    {
        attackDir = player.facingDir;
        if (input.moveInput.x != 0)
        {
            attackDir = input.moveInput.x;
        }
        player.SetVelocity(attackOffset[comboCounter] * attackDir, rb.velocity.y);
    }
}
