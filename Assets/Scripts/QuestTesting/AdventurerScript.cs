using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdventurerScript : MonoBehaviour
{
    public infoHolder info;
    public MenuManager menu;

    public string aName;
    public string aClass;
    public string desc;

    public int buttonNumber;

    public int maxEndurance;
    public int currentEndurance;
    public int baseEndurance;

    public int dex;
    public int str;
    public int luck;
    public int arcane;

    public int atk;
    public int def;
    public int health;


    public int level;
    public int exp;

    public bool injured;
    public int injuryCountdown;

    public List<string> equipment;
    public QuestScript personalQuest;
    public HiringManager hire;
    public GameObject adventurerButton;
    public HireCostText hct;

    //optional, start with adventurer above level 1
    public int startingLevel;
    public int hireCost;

    // Start is called before the first frame update
    void Start()
    {
        info = FindObjectOfType<infoHolder>();
        injured = false;
        injuryCountdown = 0;

        //check what the starting level should be
        if(startingLevel != 0)
        {
            level = startingLevel;
            exp = 0; //idk how we're doing starting levels yet
        }
        else
        {
            level = 1;
            exp = 0;
        }

        currentEndurance = maxEndurance;
        
    }

    // Update is called once per frame
    void Update()
    {
        baseEndurance = maxEndurance;
    }

    //check if it's time to level up adventurer, probably after quest
    public void CheckLevelUp()
    {
        switch (level)
        {
            case 1:
                if(exp > 5)
                {
                    LevelUp();
                }
                break;
            case 2:
                if(exp > 10)
                {
                    LevelUp();
                }
                break;
            case 3:
                if (exp > 20)
                {
                    LevelUp();
                }
                break;
            default:
                break;
        }
    }

    //adventurer levels up
    public void LevelUp() {
        level++;
        exp = 0;

        maxEndurance++;
        currentEndurance = maxEndurance;
        baseEndurance++;

        menu.ShowLevelUp(gameObject);
        //okay temp stuff now
        /**
        atk++;
        def++;
        health++;**/
        
    }

    public void Hire()
    {
        hireCost = hire.hireCost;
        if ((info.gold - hireCost) >= 0)
        {
            adventurerButton.SetActive(true);
            Debug.Log("HIRED" + aName);
            info.adventurersOnStandby.Add(this);
            info.unlockedAdventurers.Add(this);
            info.questPool.Add(personalQuest);
            info.gold -= hireCost;
            hire.hired = true;
            info.adventurersHired++;
            hire.hireCost += 25;
            hct.UpdateGoldText();
        }
        else
        {
            hire.hired = false;
        }
    }

    //adjust stats based on equipment
    public void ReviseStats()
    {
        if(equipment.Count > 0) //if there is equiment
        {
            int adjust = 0;
            foreach (string eq in equipment)
            {
   
                switch (eq) //switch based on what item
                {
                    case "chainmail":
                        adjust += 3;
                        break;
                    case "iron helmet":
                        adjust += 2;
                        break;
                    case "boots":
                        adjust += 1;
                        break;
                }
            }
            maxEndurance = baseEndurance + adjust;
        }
        else //if no equipment
        {
            maxEndurance = baseEndurance;
        }
       
    }
}
