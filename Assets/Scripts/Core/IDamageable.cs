using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 接口：能被打
/// </summary>
public interface IDamageable
{
    // 接口只写有什么功能，不写具体实现
    // 这里就是"受伤"功能
    void TakeDamage(float damage, Transform attacker);
}
