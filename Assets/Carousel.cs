using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Carousel : MonoBehaviour
{
    private int carouselSize;
    private int separation;
    private GameObject[] imageObjs;
    private Vector3 endPosition;
    private Vector3 startPosition;

    void Start()
    {
        carouselSize = 5;
        separation = 50;
        imageObjs = new GameObject[carouselSize];
        for (int i = 0; i < carouselSize; i++)
        {
            GameObject child = gameObject.transform.GetChild(i).gameObject;
            imageObjs[i] = child;
        }
        float width = imageObjs[0].GetComponent<RectTransform>().rect.width;
        Debug.Log(width);
        endPosition = new Vector3(-width / 2 - separation, Screen.height/2, 0);
        Debug.Log(endPosition);
        startPosition = new Vector3(width * (carouselSize - 1) + separation * (carouselSize - 1) + (width / 2), Screen.height / 2, 0);
        Debug.Log(startPosition);
    }

    void Update()
    {
        float step = 100 * Time.deltaTime;
        foreach (GameObject image in imageObjs)
        {
            if(Mathf.Round(image.transform.position.x) == endPosition.x)
            {
                image.transform.position = startPosition;
            }
            else
            {
                image.transform.position = Vector3.MoveTowards(image.transform.position, endPosition, step);
            }
            
        }
    }
}
