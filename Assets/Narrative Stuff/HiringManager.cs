using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.Wrappers;
using Unity.VisualScripting;

public class HiringManager : MonoBehaviour
{
    public int hiringNum = 0;
    public int currentlyHiring = 0;
    public int hireCost = 25;
    public bool hired = false;
    public List<AdventurerScript> adventurers;
    public List<GameObject> advButtons;

    // Update is called once per frame

    private void OnEnable()
    {
        hiringNum = DialogueLua.GetVariable("AdventurerHired").AsInt;
        HireAdv(hiringNum);
        gameObject.SetActive(false);
    }
    public void HireAdv(int hiringNum)
    {
        if (currentlyHiring != hiringNum)
        {
            adventurers[hiringNum - 1].Hire();
            if (hired == true)
            {
                advButtons[hiringNum - 1].SetActive(false);
                currentlyHiring = 0;
                hiringNum = 0;
                hired = false;
            }
            else
            {
                currentlyHiring = 0;
                hiringNum = 0;
                DialogueLua.SetVariable("AdventurerHired", 0); 
                DialogueLua.SetVariable("Alert", "Not Enough Gold!");
}
        }
    }
}
