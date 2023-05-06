using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateImage : MonoBehaviour
{
    private bool isAnimating = false;
    [SerializeField] public float animationSpeed;
    private Vector3 targetPosition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isAnimating)
        {
            float step = animationSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, step);
            if(transform.position == targetPosition)
            {
                isAnimating = false;
            }
        }
    }

    public void StartAnimation()
    {
        isAnimating = true;
        targetPosition = transform.position + new Vector3(225, 0, 0);
    }
}
