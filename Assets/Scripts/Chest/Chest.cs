using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : MonoBehaviour, IDamageable
{
    private Animator anim => GetComponentInChildren<Animator>();
    private Rigidbody2D rb => GetComponent<Rigidbody2D>();
    private Entity_Visual visual => GetComponent<Entity_Visual>();

    [Header("Jump Settings")]
    [SerializeField] private Vector2 popForce = new Vector2(1, 4); // 弹跳的力度
    [SerializeField] private float spinForce = 4f;              // 旋转的力度

    private bool isOpened = false;  // “死亡锁”：防止开箱后继续被打得乱飞

    public void TakeDamage(float damage, Transform attacker)
    {
        if (isOpened) return; // 如果已经开了，就别再跳了

        isOpened = true;

        // 受伤了就播放"开箱"动画
        anim.SetBool("open", true);

        // 计算受击方向
        float dir = transform.position.x > attacker.position.x ? 1 : -1;

        // 旋转跳
        rb.velocity = new Vector2(popForce.x * dir, popForce.y); // 给一个向上的冲量
        rb.AddTorque(-dir * spinForce, ForceMode2D.Impulse);   // 增加旋转扭矩
    }
}
