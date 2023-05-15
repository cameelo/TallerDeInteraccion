using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GaussianBlur : MonoBehaviour
{
    private float minSigma = 0.1f;
    private float maxSigma = 100.0f;
    private float sigma;
    private bool isBlurring = true;
    private bool isDeblurring = false;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        Image img = gameObject.GetComponent<Image>();
        mat = img.material;
        sigma = minSigma;
        mat.SetFloat("_Sigma", sigma);
        mat.SetFloat("_Size", 30.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (isBlurring)
        {
            //Increase blur with deltatime
            //When it reaches max blur set isBlurring to false even if prompt was not sent
            sigma += Time.deltaTime * 2;
            mat.SetFloat("_Sigma", sigma);
            if (sigma >= maxSigma)
            {
                isBlurring = false;
            }
        }
        else if (isDeblurring)
        {
            //Decrease blur with deltatime
            //If it reaches 0 set isDeblurring to false
            sigma -= Time.deltaTime * 2;
            mat.SetFloat("_Sigma", sigma);
            if (sigma <= minSigma)
            {
                isDeblurring = false;
            }
        }
    }

    public void StopBlurring()
    {
        isBlurring = false;
    }

    public void StartDeblurr()
    {
        isDeblurring = true;
    }
}
