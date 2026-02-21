using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用生命值组件
/// 采用观察者模式实现受击反馈
/// </summary>
public class Health : MonoBehaviour
{
    public System.Action<Transform> OnHit;  // "委托"，俗称广播

    [Header("Health Details")]
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    /// <summary>
    /// 受伤逻辑
    /// </summary>
    /// <param name="damage">受到的原始伤害数值</param>
    /// <param name="attacker">攻击者的transform，用于位置判定</param>
    public void TakeDamage(float damage, Transform attacker)
    {
        currentHp -= damage;

        OnHit?.Invoke(attacker);    // 向订阅者广播收到的参数

        if (currentHp <= 0)
        {
            Die();
        }
    }

    /// <summary>
    /// 死亡逻辑
    /// TODO:未来需要完善死亡动画等
    /// </summary>
    private void Die()
    {
        Debug.Log("Entity dead！");
        return;
    }
}
