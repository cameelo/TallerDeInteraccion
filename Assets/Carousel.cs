using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{
    private int carouselSize;
    private GameObject[] imageObjs;
    private Vector3 endPosition;
    private Vector3 startPosition;
    private ImageCollection imageCollection;
    private int recontructedIndex;

    IEnumerator Start()
    {
        imageCollection = GameObject.FindObjectOfType<ImageCollection>();
        yield return new WaitUntil(() => imageCollection.getIsInitialized());

        carouselSize = 5;
        imageObjs = new GameObject[carouselSize];
        for (int i = 0; i < carouselSize; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            imageObjs[i] = child;
        }
        float width = imageObjs[0].GetComponent<RectTransform>().rect.width;
        
        //TODO: startPostion and endPosition are hardcoded for a resolution of 1920x1080. To get this to work on different resolutions we need to figure out the transform between the editor and world space.
        endPosition = new Vector3(-250, Screen.height/2, 0);
        startPosition = new Vector3(Screen.width + 580, Screen.height / 2, 0);

        //Set carousel initial images
        recontructedIndex = 0;
        foreach (GameObject imageObj in imageObjs)
        {
            Texture2D texture = new Texture2D(2, 2);
            texture.LoadImage(imageCollection.getFileBytes(ref recontructedIndex));
            var sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero, 1f);
            Image image = imageObj.GetComponent<Image>();
            image.sprite = sprite;
            //No sería necesiario borrar la textura allocada con new?
        }
    }

    void Update()
    {
        float step = 100 * Time.deltaTime;
        foreach (GameObject imageObj in imageObjs)
        {
            if(Mathf.Round(imageObj.transform.position.x) == endPosition.x)
            {
                imageObj.transform.position = startPosition;

                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(imageCollection.getFileBytes(ref recontructedIndex));
                var sprite = Sprite.Create(texture, new Rect(0, 0, 256, 256), Vector2.zero, 1f);
                Image image = imageObj.GetComponent<Image>();
                image.sprite = sprite;
            }
            else
            {
                imageObj.transform.position = Vector3.MoveTowards(imageObj.transform.position, endPosition, step);
            }
            
        }
    }
}
