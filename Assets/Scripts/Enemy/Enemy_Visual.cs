using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Visual : Entity_Visual
{
    [Header("Attack Alert")]
    [SerializeField] GameObject counterIndicator;

    /// <summary>
    /// 方法：攻击时出现警示,"危"！
    /// </summary>
    /// <param name="enable"></param>
    public void SetCounterAlert(bool enable) => counterIndicator.SetActive(enable);

}
