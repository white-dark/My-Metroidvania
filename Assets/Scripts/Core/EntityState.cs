using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EntityState
{
    protected StateMachine stateMachine;
    protected string animBoolName;

    protected Rigidbody2D rb;
    protected Animator anim;

    protected float stateTimer;
    protected bool triggerCalled;

    public EntityState(StateMachine stateMachine, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.animBoolName = animBoolName;
    }

    public virtual void Enter()
    {
        anim.SetBool(animBoolName, true);
        triggerCalled = false;      // 用于攻击动作
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;   // 用于状态倒计时，例如Dash
    }

    public virtual void PhysicsUpdate()
    {

    }

    public virtual void Exit()
    {
        anim.SetBool(animBoolName, false);
    }

    /// <summary>
    /// 动画结束触发器
    /// 一般用在攻击动作结束那一帧上
    /// </summary>
    public void AnimationFinishTrigger()
    {
        triggerCalled = true;
    }
}
