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


    private float lastTime;             // 进战游戏时间
    private float battleDuration = 3;   // 脱战倒计时

    public override void Enter()
    {
        base.Enter();

        lastTime = Time.time;

        anim.SetFloat("battleAnimMultiplier", enemy.battleMoveSpeed / enemy.moveSpeed);

        // 如果玩家离得太近就后撤一下
        if (DistanceToPlayer() < enemy.retreatDistance)
        {
            enemy.CheckFlip(DirToPlayer());
            enemy.SetVelocity(enemy.retreatForce.x * -DirToPlayer(), enemy.retreatForce.y);
        }
    }


    public override void Update()
    {
        base.Update();

        // 持续检测目标
        if (enemy.TryGetPlayerHit(out var hit))
        {
            // player = hit.transform; // 持续刷新目标
            lastTime = Time.time;
        }

        // 目标脱离时间过长，切回idle
        if (enemy.gameTime > lastTime + battleDuration)
        {
            stateMachine.ChangeState(enemy.idleState);
        }

        // 在攻击范围内并且可以检测到玩家则发起攻击
        if (DistanceToPlayer() < enemy.attackDistance && enemy.TryGetPlayerHit(out hit))
        {
            stateMachine.ChangeState(enemy.attackState);
        }
        else
        {
            // 若不满足，则触发追踪逻辑
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

    /// <summary>
    /// 方法：可以在外部设置目标
    /// </summary>
    /// <param name="target"></param>
    public void SetTarget(Transform target) => player = target;
}
