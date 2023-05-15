using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Pixelation : MonoBehaviour
{
    private float pixelSize = 1.0f;
    private float maxPixelSize = 32;
    private bool isPixelating = true;
    private bool isDepixelating = false;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        Image img = gameObject.GetComponent<Image>();
        mat = img.material;
    }

    // Update is called once per frame
    void Update()
    {
        if (isPixelating)
        {
            pixelSize += Time.deltaTime * 0.5f;
            mat.SetFloat("_PixelWidth", pixelSize);
            mat.SetFloat("_PixelHeight", pixelSize);
            if (pixelSize >= maxPixelSize)
            {
                isPixelating = false;
            }
        }

        if (isDepixelating)
        {
            pixelSize -= Time.deltaTime * 1.5f;
            mat.SetFloat("_PixelWidth", pixelSize);
            mat.SetFloat("_PixelHeight", pixelSize);
            if (pixelSize <= 1.0f)
            {
                isDepixelating = false;
            }
        }
    }

    public void StopPixelating()
    {
        isPixelating = false;
    }

    public void StartDepixelating()
    {
        isDepixelating = true;
    }
}
