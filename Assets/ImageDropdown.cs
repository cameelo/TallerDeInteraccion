using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ImageDropdown : MonoBehaviour
{
    public ImageCollection ImageCollection;
    TMPro.TMP_Dropdown m_Dropdown;
    [SerializeField] private Image image;
    List<string> m_DropOptions;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        ImageCollection = GameObject.FindObjectOfType<ImageCollection>();
        yield return new WaitUntil(() => ImageCollection.getIsInitialized());
        m_DropOptions = new List<string>();
        string[] images = ImageCollection.getImages();
        m_Dropdown = GetComponent<TMPro.TMP_Dropdown>();

        foreach (var i in images)
        {
            m_DropOptions.Add(i);
        }

        m_Dropdown.AddOptions(m_DropOptions);
    }

    // Update is called once per frame
    public void updateImage()
    {
        if(m_Dropdown.value == 0)
        {
            image.sprite = null;
        }
        else
        {
            Texture2D selectedImage = new Texture2D(2, 2);
            byte[] imageBytes = File.ReadAllBytes(ImageCollection.getImage(m_Dropdown.value - 1));
            selectedImage.LoadImage(imageBytes);
            var sprite = Sprite.Create(selectedImage, new Rect(0, 0, selectedImage.width, selectedImage.height), Vector2.zero, 1f);
            image.sprite = sprite;
        }
    }

    public Texture2D getSelectedImage()
    {
        return (Texture2D)ImageCollection.getImages().GetValue(m_Dropdown.value);
    }

    public Texture2D getSelectedMask()
    {
        return (Texture2D)ImageCollection.getMasks().GetValue(m_Dropdown.value);
    }
}
