using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIOperations : MonoBehaviour
{
    float offsetX;
    float offsetY;
    //Move
    public void BeginMoveDrag()
    {
        offsetX = transform.position.x - Input.mousePosition.x;
        offsetY = transform.position.y - Input.mousePosition.y;
    }

    public void OnMoveDrag()
    {
        transform.position = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }

    //Scale
    public void BeginScaleDrag()
    {
        offsetX = GetComponent<RectTransform>().sizeDelta.x - Input.mousePosition.x;
        offsetY = GetComponent<RectTransform>().sizeDelta.y - Input.mousePosition.y;
    }

    public void OnScaleDrag()
    {
        GetComponent<RectTransform>().sizeDelta = new Vector3(offsetX + Input.mousePosition.x, offsetY + Input.mousePosition.y);
    }
}
