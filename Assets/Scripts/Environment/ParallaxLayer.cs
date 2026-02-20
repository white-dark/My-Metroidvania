using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[System.Serializable]
public class ParallaxLayer
{
    public Transform transform;
    public float parallaxMul;
    public bool isLoop;

    [HideInInspector] public float startPosX;
    [HideInInspector] public float imageWidth;

    public void GetStartPosX()
    {
        startPosX = transform.position.x;
    }

    public void GetImageWidth()
    {
        var sprite = transform.GetComponent<SpriteRenderer>();
        imageWidth = sprite.bounds.size.x;
    }
}
