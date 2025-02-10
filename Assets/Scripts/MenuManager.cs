using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;

public class MenuManager : MonoBehaviour
{

    public GameObject questLog;
    public GameObject adventurerLog;
    public GameObject mainScreen;
    public GameObject newQuestScreen;
    public GameObject resultScreen;
    public GameObject Baclony;
    public GameObject InnArea;
    public GameObject StoreUI;
    public GameObject QuestInfoNull;

    public GameObject advLevelUpScreen;
    public TextMeshProUGUI advLevelInfo;
    public GameObject guildTaxScreen;
    public TextMeshProUGUI guildTaxInfo;
    public GameObject guildRankUpScreen;
    public TextMeshProUGUI guildRankInfo;
    public GameObject injuryScreen;
    public TextMeshProUGUI injuryInfo;

    public Animator weekEndAnim;

    public QuestScript nullQ;
    public EndingCheck End;
    public infoHolder info;
    public LetterScript letter;
    public TextMeshProUGUI weekDisplay;
    public TextMeshProUGUI goldDisplay;

    public GameObject negativeMoney;

    //public GameObject resultsScreen;
    public GameObject successText;
    public GameObject failureText;
    public TextMeshProUGUI resultsSpecifics;
    public TextMeshProUGUI completionText;

    public List<AdventurerScript> injuryNotification;

    // Start is called before the first frame update
    void Start()
    {
        questLog.SetActive(false);
        adventurerLog.SetActive(false);
        mainScreen.SetActive(true);
        newQuestScreen.SetActive(false);
        resultScreen.SetActive(false);
        Baclony.SetActive(false);
        InnArea.SetActive(false);
        StoreUI.SetActive(false);
        QuestInfoNull.SetActive(false);
        negativeMoney.SetActive(false);
        guildRankUpScreen.SetActive(false);
        guildTaxScreen.SetActive(false);
        advLevelUpScreen.SetActive(false);
        injuryScreen.SetActive(false);

        UpdateGold();
        weekDisplay.text = "Week 0";
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //opens the quest log
    public void OpenQuestLog()
    {
        //open regular if active quest that isnt NULL
        if (info.activeQuests.Count > 0 && !info.activeQuests.Contains(nullQ))
        {
        questLog.GetComponent<QuestAssigningTesting>().currentQuest = info.activeQuests[0];
        questLog.SetActive(true);
        Debug.Log("past assigning quest");
        questLog.GetComponent<QuestAssigningTesting>().UpdateDisplay();

        adventurerLog.SetActive(false);
        mainScreen.SetActive(false);
        newQuestScreen.SetActive(false);
        Baclony.SetActive(false);
        InnArea.SetActive(false);
        StoreUI.SetActive(false);
        QuestInfoNull.SetActive(false);
        }

        else 
        {
            if (!info.activeQuests.Contains(nullQ)) {
                info.activeQuests.Add(nullQ);
            }
            questLog.SetActive(true);
            adventurerLog.SetActive(false);
            mainScreen.SetActive(false);
            newQuestScreen.SetActive(false);
            Baclony.SetActive(false);
            InnArea.SetActive(false);
            StoreUI.SetActive(false);
            QuestInfoNull.SetActive(true);
        }

    }

    //opens the adventurer log
    public void OpenAdventurerLog()
    {

            questLog.SetActive(false);
            adventurerLog.SetActive(true);
            mainScreen.SetActive(false);
            newQuestScreen.SetActive(false);
            Baclony.SetActive(false);
            InnArea.SetActive(false);
            StoreUI.SetActive(false);
            QuestInfoNull.SetActive(false);
            adventurerLog.GetComponent<InventoryManager>().UpdateDisplay();

    }

    //opens the main screen
    public void OpenMainScreen()
    {
        questLog.SetActive(false);
        adventurerLog.SetActive(false);
        mainScreen.SetActive(true);
        newQuestScreen.SetActive(false);
        resultScreen.SetActive(false);
        Baclony.SetActive(false);
        InnArea.SetActive(false);
        StoreUI.SetActive(false);
        QuestInfoNull.SetActive(false);
        UpdateGold();
    }

    //opens new quest window
    public void OpenNewQuestScreen()
    {
        questLog.SetActive(false);
        adventurerLog.SetActive(false);
        mainScreen.SetActive(false);
        newQuestScreen.SetActive(true);
        resultScreen.SetActive(false);
        Baclony.SetActive(false);
        InnArea.SetActive(false);
        StoreUI.SetActive(false);
        QuestInfoNull.SetActive(false);

        newQuestScreen.GetComponent<ModifiedQuestScreen>().BeginQuestDraw();
    }


    //opens the InnArea Screen
    public void OpenInnAreaScreen()
    {
        questLog.SetActive(false);
        adventurerLog.SetActive(false);
        mainScreen.SetActive(false);
        newQuestScreen.SetActive(false);
        resultScreen.SetActive(false);
        Baclony.SetActive(true);
        InnArea.SetActive(true);
        StoreUI.SetActive(false);
        QuestInfoNull.SetActive(false);
    }

    //opens the Store UI Screen
    public void OpenStoreUIScreen()
    {
        //**check that at least one adventurer has been hired first
        //if(info.adventurersHired > 0) (commented out for easter egg dialogue)
        
            questLog.SetActive(false);
            adventurerLog.SetActive(false);
            mainScreen.SetActive(false);
            newQuestScreen.SetActive(false);
            resultScreen.SetActive(false);
            Baclony.SetActive(false);
            InnArea.SetActive(false);
            StoreUI.SetActive(true);
            QuestInfoNull.SetActive(false);
        
       
    }



    //calls next week
    public void CallNextWeek()
    {

        weekEndAnim.Play("NextWeekAnim");

        info.weeks++;
        UpdateWeek();

        letter.CheckMessages();

        //restore adventurer endurance
        if (info.adventurersOnStandby.Count > 0)
        {
            foreach(AdventurerScript re in info.adventurersOnStandby)
            {
                if (re.currentEndurance < re.maxEndurance)
                {
                    re.currentEndurance++;
                }
            }
        }


        //CheckTax()
        
        //CheckNextQuest();
        //OpenNewQuestScreen();
    }

    public void CheckTax(){
        //every month
        if(info.weeks%4 == 0)
        {
            int tax = 5;
            if(info.unlockedAdventurers.Count > 0)//if there are unlocked adventurers
            {
                foreach(AdventurerScript a in info.unlockedAdventurers)
                {
                    tax += 10; // tax 10 gold for each adventurer
                }
            }
            ShowTax(tax);
            info.gold -= tax;
            Debug.Log("tax - " + info.weeks);
        } else{
            CheckNextQuest();
        }
    }

    //updates gold shown
    public void UpdateGold()
    {
        goldDisplay.text = "Gold: " + info.gold;
        if(info.gold < 0)
        {
            info.negativeMoney = true;
            negativeMoney.SetActive(true);
        }
        else
        {
            info.negativeMoney = false;
            negativeMoney.SetActive(false);
        }
    }
    public void GoldGift()
    {
        if(info.negativeMoney == true)
        {
            info.gold = 25 + (5 * info.adventurersHired);
            info.negativeMoney = false;
            UpdateGold();
        }
    }

    public void UpdateWeek()
    {
        weekDisplay.text = "Week " + info.weeks;

        List<AdventurerScript> hold = new List<AdventurerScript>();
        if (info.injuredAdventurers.Count > 0)
        {
            //for each adventurer
            foreach (AdventurerScript a in info.injuredAdventurers)
            {
                a.injuryCountdown--;
                //if they've recovered
                if (a.injuryCountdown < 1)
                {
                    a.injured = false;

                }
                else
                {
                    hold.Add(a);
                }
            }

        
    }
        info.injuredAdventurers = hold;
    }


    //checks whether quest is successful
    public void EvaluateQuest(QuestScript testing)
    {
        GetComponent<AudioManager>().playPageTurn();
        Debug.Log("evaluate: " + testing.questName);
        bool passed = false;

        /**
       if (testing.adventurersAssigned.Count != 0)
       {
           float a = Random.Range(testing.atkRangeMin, testing.atkRangeMax);
           float d = Random.Range(testing.defRangeMin, testing.defRangeMax);
           float h = Random.Range(testing.healthRangeMin, testing.healthRangeMax);

           bool passed = true;

           foreach (AdventurerScript adv in testing.adventurersAssigned)
           {
               if (adv.atk < a)
               {
                   passed = false;
               }
               else if (adv.def < d)
               {
                   passed = false;
               }
               else if (adv.health < h)
               {
                   passed = false;
               }
       }**/

                    //initialize rewards/costs
        int extraGold = 0;
        int extraProb = 0;
        int extraExp = 0;
        int extraEn = 0;

        if (testing.adventurersAssigned.Count >0)
        {


            //calculate stats from adventurers
            int poolLuck = 0;
            int poolDex = 0;
            int poolArc = 0;
            int poolStr = 0;

            foreach(AdventurerScript stat in testing.adventurersAssigned)
            {
                poolLuck += stat.luck;
                poolDex += stat.dex;
                poolArc += stat.arcane;
                poolStr += stat.str;
            }

            int statCast = testing.maxAdventurers * 5;

            if (statCast <= poolLuck) {
                extraGold = testing.gold/4;
                Debug.Log("extra gold: " + extraGold);
            }
            if (statCast <= poolDex)
            {
                extraEn = 1;
                Debug.Log("extra En");
            }
            if(statCast <= poolArc)
            {
                extraExp = testing.adventurerExp/10;
                Debug.Log("extra exp: " + extraExp);
            }
            if (statCast <= poolStr)
            {
                extraProb = 5;
                Debug.Log("extra prob: " + extraProb);
            }



            int cast = Random.Range(0, 101);

                if (cast <= (testing.currentProbability + extraProb))
                {
                    passed = true;
                }
                else
                {
                    passed = false;
                }
            
            resultScreen.SetActive(true);

            testing.sent = false;



            //int statCast = Random.Range(0, 21);

            /**if (statCast >= poolLuck) {
                gol += gol / 2;
            }
            if (statCast >= poolDex)
            {
                en = en / 2;
            }
            if(statCast >= poolArc)
            {
                ex += ex / 2;
            }
            if (statCast >= poolStr)
            {
                rep += rep / 2;
            }*/


            
            

            
                //foreach (AdventurerScript adv in testing.adventurersAssigned)
                while(testing.adventurersAssigned.Count>0)
                {
                    AdventurerScript adv = testing.adventurersAssigned[0];
                    if(passed){
                    //give adventurers exp
                    adv.exp += testing.adventurerExp + extraExp;
                    adv.CheckLevelUp();
                    }

                    //subtract endurance cost
                    adv.currentEndurance -= testing.enduranceCost - extraEn;

                    //return adventurers to standby
                    info.adventurersOnStandby.Add(adv);
                    testing.adventurersAssigned.Remove(adv);
                }  
        }

            


            if (passed || info.questsCompleted == 0)
            {
                int gol = testing.gold + extraGold;
                if(testing.FinalQuest == true)
                {
                    End.ended = true;
                }
                successText.SetActive(true);
                failureText.SetActive(false);
                completionText.text = testing.onCompletion;
                resultsSpecifics.text = "Obtained: "+ gol +" gold and "+ testing.rep + " reputation points";
                //ReturnQuestToPool();

                info.gold += gol;
                info.reputation += testing.rep;
                UpdateGold();

                info.activeQuests.Remove(testing);
                info.sentQuests.Remove(testing);
                info.completedQuests.Add(testing);
                info.questsCompleted++;
                info.ChainQuestCheck();
        }
            else
            {
                successText.SetActive(false);
                failureText.SetActive(true);
                resultsSpecifics.text = "Lost: "+testing.rep/2+" reputation points";

                info.reputation -= testing.rep / 2;

                if(!info.questPool.Contains(testing)){
                    info.questPool.Add(testing);
                }
                info.sentQuests.Remove(testing);


            //see if adventurer is injured
            if (testing.adventurersAssigned.Count > 0)
            {
                foreach(AdventurerScript adv in testing.adventurersAssigned)
                {
                    int coin = Random.Range(0, 100);
                    int line = 80 - (adv.luck*3);
                    if (coin < 80)
                    {
                        adv.injured = true;
                        adv.injuryCountdown = 3;
                        info.injuredAdventurers.Add(adv);
                        injuryNotification.Add(adv);
                    }
                }
            }
            if (injuryNotification.Count > 0)
            {
                ShowInjury();
            }

            }


         //remove adventurers
        while (testing.adventurersAssigned.Count > 0)
        {
            testing.adventurersAssigned.Remove(testing.adventurersAssigned[0]);
        }
    }


    

    //iterates sent quest being checked
    public void CheckNextQuest()
    {
        
        if (info.sentQuests.Count > 0) //if there are quests sent out
        {
            Debug.Log("CHECK NEXT QUEST:" + info.sentQuests.Count);
            EvaluateQuest(info.sentQuests[0]);
        }
        else
        {
            UpdateGold();
            //CheckRankUp();
            OpenMainScreen();
        }
        /**
        else //otherwise
        {
            OpenNewQuestScreen();
            newQuestScreen.GetComponent<ModifiedQuestScreen>().BeginQuestDraw();
        }**/
    }

    //shows that an adventurer has levelled up
    public void ShowLevelUp(GameObject adventurer)
    {
        GetComponent<AudioManager>().playPageTurn();
        GetComponent<AudioManager>().playCymbals();
        AdventurerScript adv = adventurer.GetComponent<AdventurerScript>();
        advLevelUpScreen.SetActive(true);
        advLevelInfo.text = adv.name + " has levelled up!\nLevel: " + adv.level;
    }

    //shows that the guild has ranked up
    public void ShowRankUp()
    {
        GetComponent<AudioManager>().playPageTurn();
        GetComponent<AudioManager>().playFanfare();
        guildRankUpScreen.SetActive(true);
        guildRankInfo.text = info.guildName + " has proven their mettle!\nGuild Rank: "+ info.guildRank;
    }

    //shows that the guild has been taxed
    public void ShowTax(int theTax)
    {
        GetComponent<AudioManager>().playPageTurn();
        guildTaxScreen.SetActive(true);
        guildTaxInfo.text = info.guildName + " has been taxed for each adventurer hired.\nYou have paid " +theTax +" gold in total.";
    }

    //move to injured adventurer if any, else close window
    public void NextInjured()
    {
        injuryNotification.Remove(injuryNotification[0]);
        if (injuryNotification.Count > 0)
        {
            ShowInjury();
        }
        else
        {
            injuryScreen.SetActive(false);
        }
    }

    //show injured adventurer
    public void ShowInjury()
    {
        GetComponent<AudioManager>().playPageTurn();
        injuryScreen.SetActive(true);
        injuryInfo.text = injuryNotification[0].aName + " has been injured!";
        GetComponent<AudioManager>().playGuitar();
    }

    public void CloseTaxPopup(){
        GetComponent<AudioManager>().playClick();
        guildTaxScreen.SetActive(false);
        CheckNextQuest();
    }

    public void CloseResultsPopup(){
        GetComponent<AudioManager>().playClick();
        if(!info.CheckGuildLevelUp()){
            CheckNextQuest();
        }
    }

    public void CloseRankUpPopup(){
        GetComponent<AudioManager>().playClick();
        guildRankUpScreen.SetActive(false);
        CheckNextQuest();
    }

    public void CloseAdvLevelPopUp(){
        GetComponent<AudioManager>().playClick();
        advLevelUpScreen.SetActive(false);
    }

    public void CloseInjuryPopUp(){
        GetComponent<AudioManager>().playClick();
        NextInjured();
    }
}
