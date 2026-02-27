using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Visual : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("PlayHitFlash Visual")]
    [SerializeField] float flashDuration = .1f;
    [SerializeField] Material flashMaterial;
    private Material defaultMaterial;

    protected virtual void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();

        defaultMaterial = sr.material; // 获取初始默认材料
    }

    public void PlayHitFlash()
    {
        // 不管多少工人在干活，先停下离开
        StopCoroutine(nameof(HitFlashRoutine));    // nameof()就是字符串化，好处很多，例如crtl r r
        // 其他人走完了，你可以开始了
        StartCoroutine(nameof(HitFlashRoutine));
    }

    /// <summary>
    /// 协程：受击时闪烁
    /// </summary>
    /// <returns></returns>
    private IEnumerator HitFlashRoutine()
    {
        sr.material = flashMaterial;
        yield return new WaitForSeconds(flashDuration);
        sr.material = defaultMaterial;
    }
}
