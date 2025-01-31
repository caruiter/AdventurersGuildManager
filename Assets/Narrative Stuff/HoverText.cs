using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class HoverText : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject InfoText;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        InfoText.SetActive(true);
        //Debug.Log("Cursor Entering " + name + " GameObject");
    }

    //Detect when Cursor leaves the GameObject
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        InfoText.SetActive(false);
        //Debug.Log("Cursor Exiting " + name + " GameObject");
    }

    public void OnDisable()
    {
        InfoText.SetActive(false);
    }

}
