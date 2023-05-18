using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class Constants
{
    public const string imageDir = "./Patrimonio/Imagenes";
    public const string maskDir = "./Patrimonio/Mascaras";
    public const string reconstructedDir = "./Patrimonio/Reconstruido";
}

public class ImageCollection : Singleton
{
    private string[] images;
    private string[] masks;
    private List<string> reconstructed;
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
        

        string[] reconstructedArray = System.IO.Directory.GetFiles(Constants.reconstructedDir);
        for (int i = 0; i < reconstructedArray.Length; i++)
        {
            reconstructedArray[i] = reconstructedArray[i].Replace("\\", "/");
            Debug.Log(reconstructedArray[i]);
        }
        reconstructed = new List<string>(reconstructedArray);

        isInitialized = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int getSize()
    {
        return images.Length;
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

    //Returns the bytes from the recontruted image at index.
    //It also increses the index by one for the carousel
    public byte[] getFileBytes(ref int index)
    {
        string path = reconstructed[index];
        index = (index + 1)%reconstructed.Count;
        return File.ReadAllBytes(path);
    }

    public void addAndSaveImage(byte[] data)
    {
        string addImageName = Constants.reconstructedDir + "/" + (reconstructed.Count+1).ToString() + ".png";
        reconstructed.Add(addImageName);
        File.WriteAllBytes(addImageName, data);
    }
}
