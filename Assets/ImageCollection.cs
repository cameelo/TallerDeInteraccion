using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ImageCollection : Singleton
{
    private string[] images;
    private string[] masks;
    private bool isInitialized = false;

    // Start is called before the first frame update
    void Start()
    {
        images = System.IO.Directory.GetFiles("./Patrimonio/Imagenes");
        masks = System.IO.Directory.GetFiles("./Patrimonio/Mascaras");


        for (int i=0;i<images.Length;i++)
        {
            images[i] = images[i].Replace("\\", "/");
            Debug.Log(images[i]);

            masks[i] = masks[i].Replace("\\", "/");
            Debug.Log(masks[i]);
        }

        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public string[] getImages()
    {
        return images;
    }

    public string getImage(int pos)
    {
        return (string)images.GetValue(pos);
    }

    public string[] getMasks()
    {
        return masks;
    }

    public string getMask(int pos)
    {
        return (string)masks.GetValue(pos);
    }

    public bool getIsInitialized()
    {
        return isInitialized;
    }
}
