using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTrigger : Entity_AnimationTrigger
{
    private Enemy enemy;
    private Enemy_Visual visual;

    protected override void Awake()
    {
        base.Awake();

        enemy = GetComponentInParent<Enemy>();
        visual = GetComponentInParent<Enemy_Visual>();
    }

    private void EnableCountered()
    {
        enemy.SetCounterable(true);
        visual.SetCounterAlert(true);
    }

    private void DisableCountered()
    {
        enemy.SetCounterable(false);
        visual.SetCounterAlert(false);
    }
}
