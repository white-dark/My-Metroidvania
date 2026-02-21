using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用战斗组件
/// </summary>
public class Combat : MonoBehaviour
{
    public float damage = 10;

    [Header("Combat Details")]
    [SerializeField] protected LayerMask whatIsTarget;
    [SerializeField] private Transform targetCheck;
    [SerializeField] private float targetCheckRadius;

    /// <summary>
    /// 攻击触发逻辑
    /// 在动画挥砍帧调用
    /// </summary>
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(targetCheck.position, targetCheckRadius);
    }
}
