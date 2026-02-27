using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_AnimationTrigger : MonoBehaviour
{
    private Entity entity;
    private Entity_Combat combat;

    protected virtual void Awake()
    {
        entity = GetComponentInParent<Entity>();
        combat = GetComponentInParent<Entity_Combat>();
    }

    private void AnimationFinishTrigger() => entity.AnimationFinishTrigger();

    private void AttackTrigger() => combat.PerformAttack();
}
