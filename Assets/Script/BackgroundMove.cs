using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMove : MonoBehaviour
{
    [SerializeField]
    private float speed = 0.5f;

    private MeshRenderer meshRenderer = null;
    private Vector2 offset = Vector2.zero;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        if (meshRenderer == null) return;
        offset.x -= speed * Time.deltaTime;
        meshRenderer.material
            .SetTextureOffset("_MainTex", offset);
    }
}