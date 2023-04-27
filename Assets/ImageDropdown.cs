using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageDropdown : MonoBehaviour
{
    public ImageCollection ImageCollection;
    TMPro.TMP_Dropdown m_Dropdown;
    List<string> m_DropOptions;

    // Start is called before the first frame update
    IEnumerator Start()
    {
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
    void Update()
    {

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
