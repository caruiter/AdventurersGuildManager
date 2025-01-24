using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TaxSpend : MonoBehaviour
{
    public infoHolder info;
    public GameObject observers;
    public DialogueSystemTrigger trigger;
    public DialogueSystemTrigger triggerShop;
    public MenuManager menu;
    public GameObject shopUI;
    public int compareGold = 25;
    public bool shopEgg;
    public bool taxEgg;

    // Update is called once per frame
    void Update()
    {
        if (info.adventurersHired > 0)
        {
            observers.SetActive(false);
        }
        else if (info.gold < compareGold && info.adventurersHired == 0 && shopUI.activeInHierarchy == false && shopEgg == false)
        {
            trigger.OnUse();
            if(DialogueLua.GetVariable("Taxed").AsInt == 0)
            {
                info.gold = 25;
                menu.UpdateGold();
                taxEgg = true;
            }
            else
            {
                compareGold = info.gold;
            }//triggers alt dialogue if it isnt the first time seeing dialogue.
        }
        else if(info.gold < compareGold && info.adventurersHired == 0 && shopUI.activeInHierarchy == true && taxEgg == false)
        {
            triggerShop.OnUse();
            if(DialogueLua.GetVariable("Spent").AsInt == 0)
            {
                info.gold = 25;
                menu.UpdateGold();
                shopEgg = true; //you can only get bonus gold from the shop OR tax easter egg, not both
            }
            else
            {
                compareGold = info.gold;
            }//triggers alt dialogue if it isnt the first time seeing dialogue.
        }
    }


}
