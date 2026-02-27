using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    private PlayerInputSet input;

    public Vector2 moveInput { get; private set; }
    public bool jumpInput {  get; private set; }
    public bool dashInput {  get; private set; }
    public bool attackInput {  get; private set; }
    public bool counterAttackInput { get; private set; }

    private void Awake()
    {
        input = new PlayerInputSet();
    }

    private void OnEnable()
    {
        input.Enable();

        input.Player.Movement.performed += OnMove;
        input.Player.Movement.canceled += OnMove;

        input.Player.Jump.started += OnJump;
        input.Player.Jump.canceled += OnJump;

        input.Player.Dash.started += OnDash;
        input.Player.Dash.canceled += OnDash;

        input.Player.Attack.started += OnAttack;
        input.Player.Attack.canceled += OnAttack;

        input.Player.CounterAttack.started += OnCounterAttack;
        input.Player.CounterAttack.canceled += OnCounterAttack;

    }

    private void OnDisable()
    {
        input.Player.Movement.performed -= OnMove;
        input.Player.Movement.canceled -= OnMove;

        input.Player.Jump.started -= OnJump;
        input.Player.Jump.canceled -= OnJump;

        input.Player.Dash.started -= OnDash;
        input.Player.Dash.canceled -= OnDash;

        input.Player.Attack.started -= OnAttack;
        input.Player.Attack.canceled -= OnAttack;

        input.Player.CounterAttack.started -= OnCounterAttack;
        input.Player.CounterAttack.canceled -= OnCounterAttack;

        input.Disable();
    }

    public void UseJumpInput() => jumpInput = false;
    public void UseDashInput() => dashInput = false;
    public void UseAttackInput() => attackInput = false;
    public void UseCounterAttackInput() => counterAttackInput = false;

    private void OnMove(InputAction.CallbackContext ctx)
    {
        moveInput = ctx.ReadValue<Vector2>();
    }

    private void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.started) jumpInput = true;
        else if (ctx.canceled) jumpInput = false;
    }

    private void OnDash(InputAction.CallbackContext ctx)
    {
        if (ctx.started) dashInput = true;
        else if (ctx.canceled) dashInput = false;
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started) attackInput = true;
        else if (ctx.canceled) attackInput = false;
    }

    private void OnCounterAttack(InputAction.CallbackContext ctx)
    {
        if (ctx.started) counterAttackInput = true;
        else if (ctx.canceled) counterAttackInput = false;
    }
}