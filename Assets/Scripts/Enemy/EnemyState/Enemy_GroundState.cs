using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_GroundState : EnemyState
{
    public Enemy_GroundState(Enemy enemy, StateMachine stateMachine, string animBoolName) : base(enemy, stateMachine, animBoolName)
    {
    }

    public override void Update()
    {
        base.Update();

        // 检测到玩家，执行"锁定目标"+"切换状态"
        if (enemy.TryGetPlayerHit(out var hit))
        {
            enemy.battleState.SetTarget(hit.transform);
            stateMachine.ChangeState(enemy.battleState);
        }
    }
}
