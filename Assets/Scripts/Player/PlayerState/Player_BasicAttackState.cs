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

    private int comboCounter;       // 攻击段数
    private int comboStart = 0;
    private int comboLimit = 2;

    private float lastAttackedTime;     // 上次攻击的游戏时间
    private float comboWindow = .4f;    // 衔接攻击的时间窗口
    
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

        // 每次攻击结束连段数增加，同时记录时间
        comboCounter++;
        lastAttackedTime = Time.time;
    }

    /// <summary>
    /// 方法：重置攻击段数
    /// 当连段超过限制或者连段超过攻击窗口则重置连段
    /// </summary>
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

    /// <summary>
    /// 方法：设置攻击位移
    /// 每一段攻击都有一个小位移，看着更舒服
    /// </summary>
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
