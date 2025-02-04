using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    public infoHolder info;
    public MenuManager menu;

    public string currentItemDisplayed;
    private int itemCost;
    private bool consumable;

    public TextMeshProUGUI ItemNameDisplay;
    public TextMeshProUGUI ItemDescription;
    public TextMeshProUGUI ItemCostDisplay;
    public TextMeshProUGUI ItemNumberDisplay;

    public Button buyButton;

    public TextMeshProUGUI goldText;


    // Start is called before the first frame update
    void Start()
    {
        goldText.text = "Gold: " + info.gold;
        SelectItem(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SelectItem(int which)
    {
        //display relevant information
        switch (which)
        {
            case 0:
                ItemNameDisplay.text = "Potion 1";
                ItemDescription.text = "A small potion that can immedietly provide relief for worn out adventurers. Consumable, restores 2 Endurance.";
                ItemCostDisplay.text = "Costs 5 gold";
                itemCost = 5;
                currentItemDisplayed = "sEndPotion";
                consumable = true;
                break;
            case 1:
                ItemNameDisplay.text = "Potion 2";
                ItemDescription.text = "A medium potion that can immedietly provide relief for worn out adventurers. Consumable, restores 5 Endurance.";
                ItemCostDisplay.text = "Costs 15 gold";
                itemCost = 15;
                currentItemDisplayed = "mEndPotion";
                consumable = true;
                break;
            case 2:
                ItemNameDisplay.text = "Potion 3";
                ItemDescription.text = "A large potion that can immedietly provide relief for worn out adventurers. Consumable, restores 10 Endurance.";
                ItemCostDisplay.text = "Costs 30 gold";
                itemCost = 30;
                currentItemDisplayed = "lEndPotion";
                consumable = true;
                break;
            case 3:
                ItemNameDisplay.text = "Boots";
                ItemDescription.text = "A nice pair of boots that makes long trips a bit easier. Equippable, increases max Endurance by 1.";
                ItemCostDisplay.text = "Costs 10 gold";
                itemCost = 10;
                currentItemDisplayed = "boots";
                consumable = false;
                break;
            case 4:
                ItemNameDisplay.text = "Iron Helmet";
                ItemDescription.text = "A decent iron helmet that gives a sense of security. Equippable, increases max Endurance by 2.";
                ItemCostDisplay.text = "Costs 15 gold";
                itemCost = 15;
                currentItemDisplayed = "helmet";
                consumable = false;
                break;
            case 5:
                ItemNameDisplay.text = "Chainmail";
                ItemDescription.text = "A reliable chainmail tunic. Equippable, increases max Endurance by 3.";
                ItemCostDisplay.text = "Costs 25 gold";
                itemCost = 25;
                currentItemDisplayed = "chainmail";
                consumable = false;
                break;

        }

        NumberInInventory();


    }

    public void BuyItem()
    {
        //check that player has enough gold
        if(info.gold >= itemCost)
        {
            if (consumable)
            {
                info.consumableItems.Add(currentItemDisplayed);
            }
            else
            {
                info.equipableItems.Add(currentItemDisplayed);
            }
            info.gold -= itemCost;
            info.goldSpent += itemCost;
        }
        NumberInInventory();

        //update gold shown
        goldText.text = "Gold: " + info.gold;
    }


    //update display of number of selected item in inventory AND updates buybutton
    public void NumberInInventory()
    {
        //check how many player has in inventory
        int guildHas = 0;
        if (info.consumableItems.Contains(currentItemDisplayed))
        {
            foreach (string c in info.consumableItems)
            {
                if (c.Equals(currentItemDisplayed))
                {
                    guildHas++;
                }
            }

        }
        else if (info.equipableItems.Contains(currentItemDisplayed))
        {
            foreach (string e in info.equipableItems)
            {
                if (e.Equals(currentItemDisplayed))
                {
                    guildHas++;
                }
            }
        }
        ItemNumberDisplay.text = "Items of this kind currently in inventory: " + guildHas;


        //ALSO shows if player has enough gold to buy item
        if (info.gold > itemCost)
        {
            buyButton.interactable = true;
            buyButton.GetComponent<Image>().color = Color.white;
        }
        else
        {
            buyButton.interactable = false;
            buyButton.GetComponent<Image>().color = Color.grey;
        }
    }
}
