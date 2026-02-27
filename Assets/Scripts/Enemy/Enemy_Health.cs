using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Health : Entity_Health
{
    private Enemy enemy => GetComponent<Enemy>();

    private void OnEnable()
    {
        OnHit += ReactToHit;    // 向发布者订阅，即挂载到OnHit下
    }

    private void OnDisable()
    {
        OnHit -= ReactToHit;    // 通俗的说，离职时擦掉电话号码，公司就再也不找你了
    }

    /// <summary>
    /// 回调方法：响应受击并回头反击
    /// </summary>
    /// <param name="attacker"></param>
    private void ReactToHit(Transform attacker)
    {
        float dirToAttacker = attacker.position.x - transform.position.x;

        enemy.CheckFlip(dirToAttacker);

        // 进战前锁定目标
        if (enemy.stateMachine.currentState != enemy.battleState)
        {
            enemy.battleState.SetTarget(attacker);
            enemy.stateMachine.ChangeState(enemy.battleState);
        }
    }
}