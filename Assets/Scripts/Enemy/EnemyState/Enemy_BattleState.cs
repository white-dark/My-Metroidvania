using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Enemy_BattleState : EnemyState
{
    public Enemy_BattleState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    private Transform player;
    private float battleDuration = 3;
    private float lastTime;

    public override void Enter()
    {
        base.Enter();

        var hit = enemy.CheckPlayer();

        if (hit.collider != null)
        {
            player = hit.transform;
            lastTime = Time.time;
        }
        else
        {
            stateMachine.ChangeState(enemy.idleState);
            return;
        }

        anim.SetFloat("battleAnimMultiplier", enemy.battleMoveSpeed / enemy.moveSpeed);

        CheckRetreat();
    }


    public override void Update()
    {
        base.Update();

        if (enemy.CheckPlayer())
        {
            lastTime = Time.time;
        }

        if (IsOver())
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        if (WithInAttackRange() && enemy.CheckPlayer())
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            enemy.CheckFlip(DirToPlayer());
            enemy.SetVelocity(enemy.battleMoveSpeed * DirToPlayer(), rb.velocity.y);
        }
    }

    private float DistanceToPlayer()
    {
        if (player == null)
            return int.MaxValue;
        return Mathf.Abs(player.position.x - enemy.transform.position.x);
    }

    private int DirToPlayer()
    {
        if (player == null)
            return 0;
        return player.position.x > enemy.transform.position.x ? 1 : -1;
    }

    private void CheckRetreat()
    {
        if (DistanceToPlayer() < enemy.retreatDistance)
        {
            enemy.CheckFlip(DirToPlayer());
            enemy.SetVelocity(enemy.retreatForce.x * -DirToPlayer(), enemy.retreatForce.y);
        }
    }

    private bool IsOver() => enemy.gameTime > lastTime + battleDuration;

    private bool WithInAttackRange() => DistanceToPlayer() < enemy.attackDistance;
}
