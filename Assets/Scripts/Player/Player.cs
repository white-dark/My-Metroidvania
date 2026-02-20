using UnityEditor.Tilemaps;
using UnityEngine;

public class Player : Entity
{
    public InputHandler input { get; private set; }

    public Player_IdleState idleState { get; private set; }
    public Player_MoveState moveState { get; private set; }
    public Player_JumpState jumpState { get; private set; }
    public Player_FallState fallState { get; private set; }
    public Player_WallSlideState wallSlideState { get; private set; }
    public Player_WallJumpState wallJumpState { get; private set; }
    public Player_DashState dashState { get; private set; }
    public Player_BasicAttackState basicAttackState { get; private set; }
    public Player_PlungeAttackState plungeAttackState { get; private set; }

    [Header("Move Details")]
    public float moveSpeed;
    public float dashSpeed;
    public float dashDuration;

    [Header("Jump Details")]
    public float jumpForce;
    public float slideSpeed;
    public Vector2 wallJumpForce;

    [Header("Attack Details")]
    public float[] attackOffset;

    protected override void Awake()
    {
        base.Awake();

        input = GetComponent<InputHandler>();
        
        idleState = new Player_IdleState(this, stateMachine, "idle");
        moveState = new Player_MoveState(this, stateMachine, "move");
        jumpState = new Player_JumpState(this, stateMachine, "jump");
        fallState = new Player_FallState(this, stateMachine, "jump");
        wallSlideState = new Player_WallSlideState(this, stateMachine, "wallSlide");
        wallJumpState = new Player_WallJumpState(this, stateMachine, "jump");
        dashState = new Player_DashState(this, stateMachine, "dash");
        basicAttackState = new Player_BasicAttackState(this, stateMachine, "basicAttack");
        plungeAttackState = new Player_PlungeAttackState(this, stateMachine, "AirAttack");
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }
}