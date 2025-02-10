using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestAssigningTesting : MonoBehaviour
{
    public QuestScript currentQuest;
    public infoHolder info;
    public int index;
    public MenuManager menu;

    public TextMeshProUGUI questNameDisplay;
    public TextMeshProUGUI questDescDisplay;
    public TextMeshProUGUI questObjDisplay;
    public TextMeshProUGUI questReqsDisplay;
    public TextMeshProUGUI questRewardsDisplay;
    public TextMeshProUGUI bonusesDisplay;

    public List<GameObject> AdventurerRosterButtons;
    public List<GameObject> AssignedAdvButtons;
    public List<TextMeshProUGUI> assignedAdventurerNames;
    public List<TextMeshProUGUI> assignedAdventurerStats;

    public GameObject inProg;

    // Start is called before the first frame update
    void Start()
    {
        currentQuest = info.activeQuests?[0];
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenMenu()
    {
        currentQuest = info.activeQuests?[0];
        UpdateDisplay();
    }

    //updates information shown 
    public void UpdateDisplay()
    {
        Debug.Log("UPDATE DISPLAY");


        //update quest info shown
        questNameDisplay.text = currentQuest.questName;
        questDescDisplay.text = currentQuest.blurb;   
        questObjDisplay.text = currentQuest.objective;

        bool endok = true;
        foreach(AdventurerScript adv in currentQuest.adventurersAssigned){
            if(adv.currentEndurance < currentQuest.enduranceCost){
                endok = false;
            }
        }
        if(endok){
            questReqsDisplay.text = "Endurance Cost: " + currentQuest.enduranceCost;
        } else{
            questReqsDisplay.text = "Endurance Cost: <color=red>" + currentQuest.enduranceCost + "</color>";
        }

        questReqsDisplay.text += "\nBase Probability: " + currentQuest.baseProbability;
        if(currentQuest.currentProbability>=80){
            questReqsDisplay.text += "\nCalculated Probability: " + "<color=#005500>"+currentQuest.currentProbability + "</color>";
        } else{
            questReqsDisplay.text += "\nCalculated Probability: " + "<color=red>"+currentQuest.currentProbability+ "</color>";
        }
        
        //questReqsDisplay.text += "\nBonus Stat: " + currentQuest.bonusStat;

        questRewardsDisplay.text = "Expected Rewards: " + currentQuest.gold + " gold, ";
        questRewardsDisplay.text += "\n"+ currentQuest.adventurerExp + " exp, " + currentQuest.rep + " reputation";

        /**
        questReqsDisplay.text = "Atk range: " + currentQuest.atkRangeMin + "-" + currentQuest.atkRangeMax;
        questReqsDisplay.text += "\nDef range: " + currentQuest.defRangeMin + "-" + currentQuest.defRangeMax;
        questReqsDisplay.text += "\nHealth range: " + currentQuest.healthRangeMin + "-" + currentQuest.healthRangeMax;
      **/
        //update adventurer roster
        for (int a = 0; a < AdventurerRosterButtons.Count; a++)
        {
            //assume adventurer locked
            AdventurerRosterButtons[a].GetComponentInChildren<TextMeshProUGUI>().text = "Locked";
            AdventurerRosterButtons[a].GetComponent<Button>().interactable = false;
            AdventurerRosterButtons[a].GetComponent<Image>().color = Color.grey;

        }

        //unlock available buttons
        foreach(AdventurerScript s in info.unlockedAdventurers)
        {
            int toUnlock = s.buttonNumber;

            AdventurerRosterButtons[toUnlock].GetComponentInChildren<TextMeshProUGUI>().text = s.aName;
            //check if already assigned

            if (info.adventurersOnStandby.Contains(s) == false)
            {
                AdventurerRosterButtons[toUnlock].GetComponent<Button>().interactable = false;
                AdventurerRosterButtons[toUnlock].GetComponent<Image>().color = Color.grey;

            }
            else
            {
                AdventurerRosterButtons[toUnlock].GetComponent<Button>().interactable = true;
                AdventurerRosterButtons[toUnlock].GetComponent<Image>().color = Color.white;

                //indicate if adventurer's endurance is negative
                if (s.currentEndurance <= 0)
                {
                    AdventurerRosterButtons[toUnlock].GetComponent<Image>().color = Color.red;
                } else if (s.injured)
                {
                    AdventurerRosterButtons[toUnlock].GetComponent<Image>().color = Color.blue;
                    AdventurerRosterButtons[toUnlock].GetComponent<Button>().interactable = false;
                }
            }
        }
        

        //update bonuses applied
        string bonusString = "";
        int str = 0;
        int dex = 0;
        int luck = 0;
        int arc = 0;

        foreach(AdventurerScript AvSct in currentQuest.adventurersAssigned){
            str += AvSct.str;
            dex += AvSct.dex;
            luck += AvSct.luck;
            arc += AvSct.arcane;
        }
        
        int gateNum = currentQuest.maxAdventurers * 5;
        if(str >= gateNum){
            if(bonusString == ""){
                bonusString = "Bonuses: +5% probability (>"+gateNum+" str)";
            } else{
                bonusString += ", +5% probability (>"+gateNum+" str)";
            }
        }
        if(dex >= gateNum){
            if(bonusString == ""){
                bonusString = "Bonuses: -1 endurance cost (>"+gateNum+" dex)";
            } else{
                bonusString += ", -1 endurance cost (>"+gateNum+" dex)";
            }
        }
        
        if(luck >= gateNum){
            if(bonusString == ""){
                bonusString = "Bonuses: +25% gold (>"+gateNum+" luck)";
            } else{
                bonusString += ", +25% gold (>"+gateNum+" luck)";
            }
        }

        if(arc >= gateNum){
            if(bonusString == ""){
                bonusString = "Bonuses: +10% exp (>"+gateNum+" arcane)";
            } else{
                bonusString += ", +10% exp (>"+gateNum+" arcane)";
            }
        }

        if(bonusString == ""){
            bonusesDisplay.text = "No bonuses active";
        } else{
            bonusesDisplay.text = bonusString;
        }
       
        //update adventurers assigned
        for (int g = 0; g < AssignedAdvButtons.Count; g++)
        {
            //if within max number of adventurers
            if (g < currentQuest.maxAdventurers)
            {
                AssignedAdvButtons[g].GetComponent<Button>().interactable = true;
                AssignedAdvButtons[g].GetComponent<Image>().color = Color.white;
            }
            else
            {
                AssignedAdvButtons[g].GetComponent<Button>().interactable = false;
                AssignedAdvButtons[g].GetComponent<Image>().color = Color.grey;
            }
            if (g < currentQuest.adventurersAssigned.Count) //if adventurer is assigned for slot
            {
                //show details
                assignedAdventurerNames[g].text = currentQuest.adventurersAssigned[g].aName;

                if(currentQuest.adventurersAssigned[g].currentEndurance<currentQuest.enduranceCost){
                    assignedAdventurerStats[g].text = "<color=#ff8088>Endurance: " + currentQuest.adventurersAssigned[g].currentEndurance + "</color>";
                } else{
                    assignedAdventurerStats[g].text = "Endurance: " + currentQuest.adventurersAssigned[g].currentEndurance;
                }

                /**
                assignedAdventurerStats[g].text = "atk: " + currentQuest.adventurersAssigned[g].atk;
                assignedAdventurerStats[g].text += "\ndef: " + currentQuest.adventurersAssigned[g].def;
                assignedAdventurerStats[g].text += "\nhealth: " + currentQuest.adventurersAssigned[g].health;
                **/
            }
            else
            {
                //show as empty
                assignedAdventurerNames[g].text = "Empty Slot";

                assignedAdventurerStats[g].text = "Endurance: - ";
                //assignedAdventurerStats[g].text = "atk: -\ndef: -\nhealth: -";
            }
        }

        //if quest sent out, show it 
        if (currentQuest.sent)
        {
            inProg.SetActive(true);
            foreach(GameObject c in AdventurerRosterButtons)
            {
                c.GetComponent<Button>().interactable = false;
                c.GetComponent<Image>().color = Color.cyan;
            }
            foreach(GameObject a in AssignedAdvButtons)
            {
                a.GetComponent<Button>().interactable = false;
                a.GetComponent<Image>().color = Color.cyan;
            }

        }
        else
        {
            inProg.SetActive(false);
        }
    }

    //scroll through which quest is being shown
    public void ScrollButton(int direction)
    {
        //scroll 'back'
        if(direction == 0)
        {
            Debug.Log("BACK");
           if(index == 0)
            {
                index = info.activeQuests.Count - 1;
            }
            else
            {
                index--;
            }

        }
        else //scroll 'forward'
        {
            Debug.Log("FORWARD");
            if(index == info.activeQuests.Count - 1)
            {
                index = 0;
            }
            else
            {
                index++;
            }
        }
        currentQuest = info.activeQuests[index];
        menu.gameObject.GetComponent<AudioManager>().playPageTurn();
        UpdateDisplay();
    }

    //button to add adventurer
    public void AddAdventurer(AdventurerScript self)
    {
        Debug.Log(self.aName);
        //AdventurerScript self = gSelf.GetComponent<AdventurerScript>();
        //check for maximum assigned adventurers
        if (currentQuest.adventurersAssigned.Count < currentQuest.maxAdventurers)
        {
            currentQuest.adventurersAssigned.Add(self);
            Debug.Log("removal?" +  info.adventurersOnStandby.Remove(self));
            currentQuest.CalculateProbability();
            menu.gameObject.GetComponent<AudioManager>().playScribble();
            UpdateDisplay();
        }

        
    }

    //puts adventurer back on roster
    public void RemoveAdventurer(int which)
    {
        //check for null
        if(currentQuest.adventurersAssigned.Count!=0){
            if (currentQuest.adventurersAssigned[which] != null)
            {
                info.adventurersOnStandby.Add(currentQuest.adventurersAssigned[which]);
                currentQuest.adventurersAssigned.Remove(currentQuest.adventurersAssigned[which]);
                currentQuest.CalculateProbability();
                menu.gameObject.GetComponent<AudioManager>().playScribble2();
                UpdateDisplay();
            }
        }


    }

    //sends adventurers off on quest
    public void SendQuest()
    {
        if(currentQuest.sent == false && currentQuest.adventurersAssigned.Count>0)
        {
            currentQuest.sent = true;
            info.sentQuests.Add(currentQuest);
            UpdateDisplay();
        }
    }


    //returns quest to pool
    public void RejectQuest()
    {
        if(currentQuest.sent == false)
        {
            //remove any adventurers already assigned to this quest
            if (currentQuest.adventurersAssigned.Count > 0)
            {
                while (currentQuest.adventurersAssigned.Count > 0)
                {
                    info.adventurersOnStandby.Add(currentQuest.adventurersAssigned[currentQuest.adventurersAssigned.Count - 1]);
                    currentQuest.adventurersAssigned.Remove(currentQuest.adventurersAssigned[currentQuest.adventurersAssigned.Count - 1]);
                }
            }
            //return it to pool, change display
            ReturnQuestToPool();
            if (info.activeQuests.Count > 1)
            {
                ScrollButton(0);
            }
            else if (info.activeQuests.Count == 1)
            {
                currentQuest = info.activeQuests[0];
                index = 0;
                UpdateDisplay();
            }
            else
            {
                menu.OpenMainScreen();
            }
        }
        
        
    }

    //returns quest to pool
    public void ReturnQuestToPool()
    {
        info.questPool.Add(currentQuest);
        info.activeQuests.Remove(currentQuest);
    }
}
