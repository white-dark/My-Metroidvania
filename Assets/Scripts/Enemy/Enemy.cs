using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : Entity
{
    public Enemy_IdleState idleState;
    public Enemy_MoveState moveState;
    public Enemy_AttackState attackState;
    public Enemy_BattleState battleState;
    public Enemy_DeathState deathState;
    public Enemy_StunnedState stunnedState;

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

    [Header("Stunned Details")]
    public float stunnedDuration = .8f;
    public Vector2 stunnedForce = new Vector2(6, 6);
    protected bool canBeCountered;

    [Header("Player Check")]
    public LayerMask whatIsPlayer;
    public Transform playerCheck;
    public float playerCheckDistance;

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();

        gameTime = Time.time;   // 记录游戏时间，便于脱战计时 
    }

    public void SetCounterable(bool enable) => canBeCountered = enable;

    private void OnEnable()
    {
        Player.OnPlayerDeath += ReactToPlayerDeath;
    }

    private void OnDisable()
    {
        Player.OnPlayerDeath -= ReactToPlayerDeath;
    }

    private void ReactToPlayerDeath()
    {
        stateMachine.ChangeState(idleState);
    }

    public override void EntityDeath()
    {
        base.EntityDeath();

        stateMachine.ChangeState(deathState);
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
}

