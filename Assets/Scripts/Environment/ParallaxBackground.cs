using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    [SerializeField] private ParallaxLayer[] layers;
    private Transform mainCamera;
    private float cameraStartPosX;

    private void Awake()
    {
        mainCamera = Camera.main.transform;
        cameraStartPosX = mainCamera.position.x;

        foreach(var layer in layers)
        {
            layer.GetStartPosX();
            layer.GetImageWidth();
        }
    }

    private void Update()
    {
        foreach(var layer in layers)
        {
            LayerMove(layer);
            EndlessBackGround(layer);
        }
    }

    private void LayerMove(ParallaxLayer layer)
    {
        float cameraMove = mainCamera.position.x - cameraStartPosX;

        float layerMove = cameraMove * layer.parallaxMul;

        layer.transform.position =
            new Vector3(layer.startPosX + layerMove, layer.transform.position.y, layer.transform.position.z);
    }

    private void EndlessBackGround(ParallaxLayer layer)
    {
        float cameraMove = mainCamera.position.x - cameraStartPosX;

        if (layer.isLoop)
        {
            float offset = cameraMove * (1 - layer.parallaxMul);

            if (offset > layer.startPosX + layer.imageWidth)
            {
                layer.startPosX += layer.imageWidth;
            }
            else if (offset < layer.startPosX - layer.imageWidth)
            {
                layer.startPosX -= layer.imageWidth;
            }
        }
    }
}
