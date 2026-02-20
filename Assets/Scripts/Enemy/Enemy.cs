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

        gameTime = Time.time;
    }

    private void OnEnable()
    {
        health.OnHit += CheckBackAttack;
    }

    private void OnDisable()
    {
        health.OnHit -= CheckBackAttack;
    }

    private void CheckBackAttack(Transform attacker)
    {
        float dirToAttacker = attacker.position.x - transform.position.x;

        CheckFlip(dirToAttacker);

        if(stateMachine.currentState != battleState)
        {
            stateMachine.ChangeState(battleState);
        }
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(playerCheckDistance * facingDir, 0));
        Gizmos.color = Color.red;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(attackDistance * facingDir, 0));
        Gizmos.color = Color.green;
        Gizmos.DrawLine(playerCheck.position, playerCheck.position + new Vector3(retreatDistance * facingDir, 0));
    }

    public RaycastHit2D CheckPlayer()
    {
        var hit = 
            Physics2D.Raycast(playerCheck.position, Vector2.right * facingDir, playerCheckDistance, whatIsPlayer | whatIsGround);
        if (hit)
        {
            if(hit.collider.gameObject.layer != LayerMask.NameToLayer("Player"))
            {
                return default;
            }
        }
        return hit;
    }
}

