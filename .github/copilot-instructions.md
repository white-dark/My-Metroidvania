# Metroidvania AI Coding Instructions

## Project Architecture

This is a **Unity 2D Metroidvania game** using a **Hierarchical State Machine** pattern for entity behavior (Player, Enemy, Boss). The architecture has three key layers:

1. **Entity Base Layer** ([Entity.cs](../Assets/Scripts/Entity.cs)): Provides collision detection (`isGround`, `isWall`), physics (`rb`, `anim`), and movement utilities (`SetVelocity`, `CheckFlip`).
2. **State Machine Layer** ([StateMachine.cs](../Assets/Scripts/StateMachine.cs)): Minimal controller managing state transitions via `ChangeState()`.
3. **State Implementation Layer** ([PlayerState/](../Assets/Scripts/PlayerState/), [EnemyState/](../Assets/Scripts/EnemyState/)): Individual state classes inheriting from `EntityState`, overriding `Enter()`, `Update()`, `PhysicsUpdate()`, `Exit()`.

**Key Data Flow**: Input (InputHandler) → Player states check conditions → StateMachine.ChangeState() → Animations triggered via Animator bool parameters.

## Critical Developer Patterns

### State Machine Implementation
- **Base class**: All states inherit from `EntityState(StateMachine, animBoolName)` 
- **Lifecycle**: `Enter()` sets animator bool, `Update()` checks transitions, `PhysicsUpdate()` applies physics, `Exit()` clears animator bool
- **Hierarchy pattern**: Intermediate classes like `Player_GroundedState` share common logic (jump/dash/attack checks) for grounded states (Idle, Move, Attack)
- **Example**: [Player_GroundedState](../Assets/Scripts/PlayerState/Player_GroundedState.cs) centralizes ground-state transitions to Fall/Jump/Dash/Attack

### Input Consumption Pattern
- InputHandler stores raw input state; states **must consume** via `input.UseJumpInput()`, `input.UseDashInput()`, `input.UseAttackInput()` after reading
- Prevents double-processing in same frame (e.g., jump pressed → used by JumpState → no longer available to other states)
- See [InputHandler.cs](../Assets/Scripts/InputHandler.cs) for pattern

### Physics Utilities
- Use `Entity.SetVelocity(x, y)` to set Rigidbody2D velocity directly (not AddForce)
- Use `Entity.CheckFlip(dir)` to flip sprite when direction changes (keeps `facingDir` in sync)
- Ground/wall checks use raycasts from check positions; inspect via `OnDrawGizmos()` debug lines

### Animator Parameter Convention
- Each state has an animator bool parameter (e.g., "idle", "move", "dash", "wallSlide")
- Set `true` in `Enter()`, `false` in `Exit()` automatically (base class handles this)
- Animation finish signals via `AnimationFinishTrigger()` (called from animation event) → state can react in next `Update()`

## Entity Behavior Patterns

### Player Architecture
- **Main class**: [Player.cs](../Assets/Scripts/Player.cs) aggregates all state instances in Awake
- **States**: Idle → Move (on input) → Jump/Dash/Attack → Fall (if airborne) → WallSlide/WallJump (on wall contact)
- **Combat**: BasicAttack (grounded), PlungeAttack (airborne), with `attackOffset[]` array for hit detection positioning

### Enemy Architecture  
- **Base**: [Enemy.cs](../Assets/Scripts/Enemy.cs) with patrol/battle/attack logic
- **Subclass example**: [Enemy_Skeleton.cs](../Assets/Scripts/Enemy_Skeleton.cs) creates specific state instances
- **Detection**: `CheckPlayer()` raycasts to detect player; Gizmo debug lines show ranges (yellow=detect, red=attack, green=retreat)
- **Battle states**: Idle → Move (patrol) → Battle (on player detect) → Attack/Retreat based on distance

## Build & Project Structure

- **Project files**: `My Metroidvania.sln`, `Assembly-CSharp.csproj`
- **Assets organization**: `/Graphics` (sprites), `/Animations` (animation clips), `/Scripts` (organized by character type)
- **Scene management**: Scenes in `Assets/Scenes/` (not explicitly shown; check for scene files if debugging level/menu flow)
- **Build command**: Standard Unity—use editor or `dotnet build` for C# compilation

## When Adding New Features

1. **New Player State**: Create class in `PlayerState/` inheriting `PlayerState` (or `Player_GroundedState`/`Player_AiredState`); register in `Player.Awake()`
2. **New Enemy Type**: Subclass `Enemy` like `Enemy_Skeleton`, instantiate states in Awake, init in Start
3. **New Attack**: Add animator parameter, create attack state checking distance/cooldown, integrate into grounded/aired state conditions
4. **Collision/Detection**: Add raycasts via `OnDrawGizmos()` for debugging; follow Entity check pattern (LayerMask + Transform positions)

## Common Gotchas

- **Input not working**: Verify InputHandler is on Player GameObject and PlayerInputSet is active
- **Animation not playing**: Check animator bool parameter name matches state's `animBoolName` exactly
- **Physics feels wrong**: Ensure state uses `SetVelocity()` not `AddForce()`; check gravity in Physics2D settings
- **State not transitioning**: Verify `ChangeState()` called and previous state's `Exit()` runs (base class handles animator cleanup)
