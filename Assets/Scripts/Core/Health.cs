using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public System.Action<Transform> OnHit;

    [Header("Health Details")]
    [SerializeField] protected float maxHp = 100;
    [SerializeField] protected float currentHp;

    private void Awake()
    {
        currentHp = maxHp;
    }

    public void TakeDamage(float damage, Transform attacker)
    {
        currentHp -= damage;

        OnHit?.Invoke(attacker);

        if (currentHp <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Debug.Log("Entity dead£¡");
        return;
    }
}
