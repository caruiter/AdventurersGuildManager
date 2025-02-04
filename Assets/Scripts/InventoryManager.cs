using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    public infoHolder info;
    public MenuManager menu;

    public AdventurerScript currentAdventurer;

    public List<AdventurerScript> allAdventurers; //MATCH TO RELEVANT BUTTON ORDER

    public TextMeshProUGUI nameDisplay;
    public TextMeshProUGUI stat1Display;
    public TextMeshProUGUI maxEndDisplay;
    public TextMeshProUGUI stat2Display;
    public TextMeshProUGUI stat3Display;
    public TextMeshProUGUI stat4Display;
    public TextMeshProUGUI stat5Display;
    public TextMeshProUGUI charDescDisplay;

    public TextMeshProUGUI[] itemCounts;
    public TextMeshProUGUI adventurerEquipment;
    public TextMeshProUGUI status;
    public GameObject healButton;

    public GameObject infoLockedScreen;

    // Start is called before the first frame update
    void Start()
    {
        SwitchAdventurer(0);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //show buttons based on what player has in inventory and which adventurer is displayed
    public void UpdateDisplay()
    {
        nameDisplay.text = currentAdventurer.aName;
        stat1Display.text = currentAdventurer.currentEndurance.ToString();
        maxEndDisplay.text = currentAdventurer.maxEndurance.ToString();
        stat2Display.text = currentAdventurer.luck.ToString();
        stat3Display.text = currentAdventurer.dex.ToString();
        stat4Display.text = currentAdventurer.arcane.ToString();
        stat5Display.text = currentAdventurer.str.ToString();
        charDescDisplay.text = currentAdventurer.desc;

        //hides information if adventurer isn't hired
        if (info.unlockedAdventurers.Contains(currentAdventurer))
        {
            infoLockedScreen.SetActive(false);
            Debug.Log("adventurer: " + currentAdventurer.aName);
        }
        else
        {
            Debug.Log("no adventurer: " + currentAdventurer.aName);
            infoLockedScreen.SetActive(true);
        }

        //count consumable items
        int sPotCount = 0;
        int mPotCount= 0;
        int lPotCount = 0;
        foreach(string cons in info.consumableItems)
        {
            switch (cons)
            {
                case "sEndPotion":
                    sPotCount++;
                    break;
                case "mEndPotion":
                    mPotCount++;
                    break;
                case "lEndPotion":
                    lPotCount++;
                    break;
            }
        }
        itemCounts[0].text = "Count: " + sPotCount;
        itemCounts[1].text = "Count: " + mPotCount;
        itemCounts[2].text = "Count: " + lPotCount;

        //count equipable items
        int cCount =0;
        int hCount = 0;
        int bCount = 0;
        foreach (string eq in info.equipableItems)
        {
            switch (eq)
            {
                case "helmet":
                    hCount++;
                    break;
                case "chainmail":
                    cCount++;
                    break;
                case "boots":
                    bCount++;
                    break;
            }
        }
        itemCounts[3].text = "Count: " + bCount;
        itemCounts[4].text = "Count: " + hCount;
        itemCounts[5].text = "Count: " + cCount;

        if (currentAdventurer.equipment.Count > 0)
        {
            adventurerEquipment.text = currentAdventurer.equipment[0];
            if (currentAdventurer.equipment.Count > 1)
            {
                for(int c = 1; c < currentAdventurer.equipment.Count; c++)
                {
                    adventurerEquipment.text += "\n" + currentAdventurer.equipment[c];
                }
            }
        }
        else
        {
            adventurerEquipment.text = "This adventurer has nothing equipped!";
        }
        if (currentAdventurer.injured == false)
        {
            status.text = "This adventurer is healthy!";
            healButton.GetComponent<Button>().interactable = false;
            healButton.GetComponent<Image>().color = Color.grey;
            healButton.GetComponentInChildren<TextMeshProUGUI>().text = "heal for 0 gold";
        }
        else
        {
            status.text = "This adventurer is injured!";
            healButton.GetComponent<Button>().interactable = true;
            healButton.GetComponent<Image>().color = Color.red;
            int cost = 40;
            cost += (currentAdventurer.level * 5);
            healButton.GetComponentInChildren<TextMeshProUGUI>().text = "heal for "+cost+ " gold";
        }
    }

    //switch what adventurer is being displayed
    public void SwitchAdventurer(int which)
    {
        //check that player has the adventurer
        /**
        if (info.unlockedAdventurers.Contains(allAdventurers[which]))
        {**/
            //switch to that adventurer
            currentAdventurer = allAdventurers[which];
            UpdateDisplay();
        //}

    }

    //use a consumable item
    public void UseConsumable(string what)
    {
        if (info.consumableItems.Contains(what)) //check that player has item
        {
            switch (what)
            {
                case "sEndPotion":
                    currentAdventurer.currentEndurance += 2;
                    break;
                case "mEndPotion":
                    currentAdventurer.currentEndurance += 5;
                    break;
                case "lEndPotion":
                    currentAdventurer.currentEndurance += 10;
                    break;
            }
            //check that endurance caps out
            if (currentAdventurer.currentEndurance > currentAdventurer.maxEndurance)
            {
                currentAdventurer.currentEndurance = currentAdventurer.maxEndurance;
            }
            //get rid of item
            info.consumableItems.Remove(what);
            UpdateDisplay();
        }

    }

    //equip a non-consumable item
    public void EquipItem(string what)
    {
        if (info.equipableItems.Contains(what)) //check that player has item
        {
            //check that adventurer has room
            if (currentAdventurer.equipment.Count < 3 && !currentAdventurer.equipment.Contains(what))
            {
                //move to adventurer's script and revise stats
                currentAdventurer.equipment.Add(what);
                currentAdventurer.ReviseStats();

                info.equipableItems.Remove(what);
            }
            
        }
        UpdateDisplay();

    }

    //remove a non-consumable item and return adventurer's stats to normal
    public void ReturnItem(string what)
    {
        if (currentAdventurer.equipment.Contains(what)) //check that player has item
        {
            //move to adventurer's script and revise stats
            info.equipableItems.Add(what);

            currentAdventurer.equipment.Remove(what);
            currentAdventurer.ReviseStats();
        }
        UpdateDisplay();
    }

    //remove all non-consumable items from an Adventurer
    public void ReturnAllItems()
    {
        if (currentAdventurer.equipment.Count > 0)
        {
            while (currentAdventurer.equipment.Count > 0)
            {
                string e = currentAdventurer.equipment[currentAdventurer.equipment.Count - 1];
                info.equipableItems.Add(e);
                currentAdventurer.equipment.Remove(e);
            }
        }
        UpdateDisplay();
        }


    public void HealAdventurer()
    {
        int cost = 40;
        cost += (currentAdventurer.level * 5);
        Debug.Log("cost " + cost + " gold: "+info.gold);
        if (info.gold >= cost)
        {
            Debug.Log("pay");
            info.gold -= cost;
            currentAdventurer.injured = false;
            menu.UpdateGold();
            UpdateDisplay();
        }
    }
    }
