using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class PlayerState:EntityState
{
    protected Player player;
    protected InputHandler input;

    public PlayerState(Player player, StateMachine stateMachine, string animBoolName): base(stateMachine, animBoolName)
    {
        this.player = player;

        rb = player.rb;
        anim = player.anim;
        input = player.input;
    }

    public override void Update()
    {
        base.Update();

        anim.SetFloat("velocity.y", rb.velocity.y);
    }
}
