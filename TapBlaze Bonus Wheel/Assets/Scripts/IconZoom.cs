using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconZoom : MonoBehaviour
{
    Vector2 targetSize;

    void OnEnable()
    {
        targetSize = transform.GetComponent<RectTransform>().sizeDelta;
        transform.GetComponent<RectTransform>().sizeDelta = new Vector2(0, 0);
    }

    void Update()
    {
        if(transform.GetComponent<RectTransform>().sizeDelta.x < targetSize.x)
        {
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x + 5f, transform.GetComponent<RectTransform>().sizeDelta.y);
        }
        if (transform.GetComponent<RectTransform>().sizeDelta.y < targetSize.y)
        {
            transform.GetComponent<RectTransform>().sizeDelta = new Vector2(transform.GetComponent<RectTransform>().sizeDelta.x, transform.GetComponent<RectTransform>().sizeDelta.y + 5f);
        }
    }
}
