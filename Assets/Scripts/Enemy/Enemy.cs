using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    private Health health;

    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;

    [HideInInspector] public float gameTime;

    [Header("Move Details")]
    public float moveSpeed = 1;
    public float idleTime = 2;

    [Header("Attack Details")]
    public float battleMoveSpeed = 3;
    public float attackDistance = 2;
    public float retreatDistance = 1;
    public Vector2 retreatForce;
    [HideInInspector] public float battleAnimMultiplier;

    [Header("Player Check")]
    public LayerMask whatIsPlayer;
    public Transform playerCheck;
    public float playerCheckDistance;

    protected override void Awake()
    {
        base.Awake();

        health = GetComponent<Health>();
    }

    protected override void Update()
    {
        base.Update();

        gameTime = Time.time;   // 记录游戏时间，便于脱战计时 
    }

    private void OnEnable()
    {
        health.OnHit += ReactToHit;    // 向发布者订阅，即挂载到OnHit下
    }

    private void OnDisable()
    {
        health.OnHit -= ReactToHit;    // 通俗的说，离职时擦掉电话号码，公司就再也不找你了
    }

    /// <summary>
    /// 回调方法：响应受击并反击
    /// </summary>
    /// <param name="attacker"></param>
    private void ReactToHit(Transform attacker)
    {
        float dirToAttacker = attacker.position.x - transform.position.x;

        CheckFlip(dirToAttacker);

        // 进战前锁定目标
        if(stateMachine.currentState != battleState)
        {
            battleState.SetTarget(attacker);
            stateMachine.ChangeState(battleState);
        }
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(playerCheckDistance * facingDir, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(attackDistance * facingDir, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(retreatDistance * facingDir, 0));
    }

    /// <summary>
    /// 方法：获取玩家碰撞报告
    /// </summary>
    /// <returns></returns>
    public bool TryGetPlayerHit(out RaycastHit2D hit)
    {
        // 检测墙体是为了防止敌人透墙锁人
        hit = Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);

        // 检测到玩家直接返回
        if (hit && hit.collider.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            return true;
        }

        // 否则返回空报告
        hit = default;
        return false;
    }
}

