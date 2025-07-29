using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ScrollBackground : MonoBehaviour
{
    public float speed;
    public Renderer background;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        background.material.mainTextureOffset += new Vector2(speed * Time.deltaTime, 0f);
    }
}
