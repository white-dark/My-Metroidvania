using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_DeathState : EnemyState
{
    public Enemy_DeathState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Enter()
    {
        anim.enabled = false;   // 动画定格

        rb.gravityScale = 12;
        rb.velocity = new Vector2(rb.velocity.x, 15);   // 被打飞了说是

        enemy.GetComponent<Collider2D>().enabled = false;
    }
}
