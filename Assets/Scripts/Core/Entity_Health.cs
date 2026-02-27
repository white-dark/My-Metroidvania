using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 通用生命值组件
/// 采用观察者模式实现受击反馈
/// </summary>
public class Entity_Health : MonoBehaviour, IDamageable
{
    public System.Action<Transform> OnHit;  // "委托"，俗称广播

    private Entity entity;
    private Entity_Visual virsual;

    [Header("Health Details")]
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float currentHp;

    [Header("On Damage Knockback")]
    [SerializeField] private Vector2 knockbackForce = new Vector2(2, 2); // 击退位移
    [SerializeField] private float knockbackDuration = .2f;
    [SerializeField] private float heaveyDamageThreshold = .3f;
    [SerializeField] private Vector2 heavyKnockbackForce = new Vector2(6, 6);
    [SerializeField] private float heavyKnockbackDuration = .6f;

    protected virtual void Awake()
    {
        entity = GetComponent<Entity>();
        virsual = GetComponent<Entity_Visual>();

        currentHp = maxHp;
    }

    /// <summary>
    /// 方法：受到攻击后的逻辑
    /// </summary>
    /// <param name="damage">受到的原始伤害数值</param>
    /// <param name="attacker">攻击者的transform，用于位置判定</param>
    public void TakeDamage(float damage, Transform attacker)
    {
        if (IsDead()) return;   // 防止鞭尸

        currentHp -= damage;

        OnHit?.Invoke(attacker);    // 向订阅者广播收到的参数

        virsual?.PlayHitFlash();     // 受到攻击闪一下

        // 挨打后撤
        var knockback = CalculateKnockback(damage, attacker);
        var duration = CalculateKnockbackDuration(damage);
        entity?.ReceiveKnockback(knockback, duration);  // 受到攻击后退

        if (IsDead()) Die();
    }

    /// <summary>
    /// 方法：死亡后的逻辑
    /// TODO:未来需要完善死亡动画等
    /// </summary>
    protected virtual void Die() => entity.EntityDeath();

    private Vector2 CalculateKnockback(float damage, Transform attacker)
    {
        var dir = transform.position.x > attacker.position.x ? 1 : -1;
        var knockback = IsHeaveyDamage(damage) ? heavyKnockbackForce : knockbackForce;

        knockback.x = knockback.x * dir;
        return knockback;
    }

    private float CalculateKnockbackDuration(float damage)
    {
        return IsHeaveyDamage(damage) ? heavyKnockbackDuration : knockbackDuration;
    }

    private bool IsHeaveyDamage(float damage) => damage / maxHp > heaveyDamageThreshold;

    private bool IsDead() => currentHp <= 0;
}
