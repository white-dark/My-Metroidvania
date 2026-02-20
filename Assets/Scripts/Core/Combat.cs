using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour
{
    public float damage = 10;

    [Header("Combat Details")]
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;

    public void AttackTrigger()
    {
        var targetColliders = Physics2D.OverlapCircleAll(targetCheck.position, targetCheckRadius, whatIsTarget);

        foreach (var tar in targetColliders)
        {
            var enemyHealth = tar.GetComponent<Health>();

            if(enemyHealth != null)
            {
                enemyHealth.TakeDamage(damage, transform);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
