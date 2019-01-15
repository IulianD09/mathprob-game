using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBGClouds : MonoBehaviour
{
    public float loopTime;

    void Update()
    {
        MeshRenderer mr = GetComponent<MeshRenderer>();

        Material mat = mr.material;

        Vector2 offset = mat.mainTextureOffset;

        offset.x += Time.deltaTime / loopTime;

        mat.mainTextureOffset = offset;
    }
}
