using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationTrigger : MonoBehaviour
{
    private Entity entity;
    private EntityCombat combat;

    private void Awake()
    {
        entity = GetComponentInParent<Entity>();
        combat = GetComponentInParent<EntityCombat>();
    }

    private void AnimationFinishTrigger() => entity.AnimationFinishTrigger();

    private void AttackTrigger() => combat.AttackTrigger();
}
