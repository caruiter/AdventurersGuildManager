using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class infoHolder : MonoBehaviour
{

    public string guildName;
    public string guildRank = "D";

    public int questsCompleted;
    public ModifiedQuestScreen questPoolController;

    public MenuManager menu;

    public List<AdventurerScript> unlockedAdventurers;
    public List<AdventurerScript> adventurersOnStandby;
    public List<AdventurerScript> injuredAdventurers;

    public QuestScript[] part1Quests; // DON'T TAG THESE AS QUESTS TOO 
    public QuestScript[] part2Quests; // OR THESE

    public List<QuestScript> questPool;
    public List<QuestScript> genericQuestPool;

    public List<QuestScript> activeQuests;
    public List<QuestScript> sentQuests;
    public List<QuestScript> completedQuests;

    public List<QuestScript> rankUpQuests; //DONT TAG THESE AS QUESTS - MANUALLY INPUT

    public List<string> consumableItems;
    public List<string> equipableItems;

    public EndingCheck end;

    public int weeks;
    public int gold;
    public int goldSpent;
    public int reputation;
    public int adventurersHired;

    public Image shelf;
    public Sprite Cshelf;
    public Sprite Bshelf;
    public Sprite Ashelf;
    public Sprite Sshelf;

    public bool negativeMoney = false;


    // Start is called before the first frame update
    void Start()
    {
        weeks = 1;
        SearchForQuests();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //searches for added quests to add to pool
    public void SearchForQuests()
    {
        //search for all gameobjects tagged quest
        GameObject[] searching = GameObject.FindGameObjectsWithTag("Quest");
        foreach(GameObject z in searching)
        {
            //if not already in pool + RANK = GUILDRANK, add
            if(questPool.Contains(z.GetComponent<QuestScript>()) == false)
            {
                if (z.GetComponent<QuestScript>().rank == guildRank)
                {
                    questPool.Add(z.GetComponent<QuestScript>());
                }
            }
        }
        //do the same but for generic quests
        GameObject[] gSearching = GameObject.FindGameObjectsWithTag("GenericQuest");
        foreach (GameObject z in gSearching)
        {
            //if not already in pool + RANK = GUILDRANK, add
            if (genericQuestPool.Contains(z.GetComponent<QuestScript>()) == false)
            {
                if (z.GetComponent<QuestScript>().rank == guildRank)
                {
                    genericQuestPool.Add(z.GetComponent<QuestScript>());
                }
            }
        }
    }

    //check if a quest chain needs to be updated
    public void ChainQuestCheck()
    {
        for(int c = 0; c < part1Quests.Length; c++)
        {
            if (completedQuests.Contains(part1Quests[c]) && completedQuests.Contains(part2Quests[c])==false)
            {
                if(questPool.Contains(part2Quests[c]) == false && activeQuests.Contains(part2Quests[c])==false)
                {
                    questPool.Add(part2Quests[c]);
                }
            }
        }
    }

    //check if guild rank can level up
    public bool CheckGuildLevelUp()
    {
        switch (guildRank)
        {
            case "D":
                if(reputation >= 15)
                {
                    //if necessary quest is completed
                    if (completedQuests.Contains(rankUpQuests[0]))
                    {
                        guildRank = "C";
                        shelf.sprite = Cshelf;
                        SearchForQuests();
                        menu.ShowRankUp();
                        return true;
                        //else if necessary quest hasn't been unlocked
                    } else if (questPool.Contains(rankUpQuests[0]) == false && activeQuests.Contains(rankUpQuests[0])==false)
                    {
                        questPoolController.fromPool.Add(rankUpQuests[0]);
                        questPool.Add(rankUpQuests[0]);
                        return false;
                    }
                } 
                break;
            case "C":
                if (reputation >= 35)
                {
                    //if necessary quest is completed
                    if (completedQuests.Contains(rankUpQuests[1]))
                    {
                        guildRank = "B";
                        shelf.sprite = Bshelf;
                        SearchForQuests();
                        menu.ShowRankUp();
                        return true;
                        //else if necessary quest hasn't been unlocked
                    }
                    else if (questPool.Contains(rankUpQuests[1]) == false && activeQuests.Contains(rankUpQuests[1]) == false)
                    {
                        questPoolController.fromPool.Add(rankUpQuests[1]);
                        questPool.Add(rankUpQuests[1]);
                        return false;
                    }
                }
                break;
            case "B":
                if (reputation >= 60)
                {
                    //if necessary quest is completed
                    if (completedQuests.Contains(rankUpQuests[2]))
                    {
                        guildRank = "A";
                        shelf.sprite = Ashelf;
                        SearchForQuests();
                        menu.ShowRankUp();
                        return true;
                        //else if necessary quest hasn't been unlocked
                    }
                    else if (questPool.Contains(rankUpQuests[2]) == false && activeQuests.Contains(rankUpQuests[2]) == false)
                    {
                        questPoolController.fromPool.Add(rankUpQuests[2]);
                        questPool.Add(rankUpQuests[2]);
                        return false;
                    }
                }
                break;
            case "A":
                if (reputation >= 100)
                {
                    //if necessary quest is completed
                    if (completedQuests.Contains(rankUpQuests[3]))
                    {
                        guildRank = "S";
                        shelf.sprite = Sshelf;
                        SearchForQuests();
                        menu.ShowRankUp();
                        return true;
                        //else if necessary quest hasn't been unlocked
                    }
                    else if (questPool.Contains(rankUpQuests[3]) == false && activeQuests.Contains(rankUpQuests[3]) == false)
                    {
                        questPoolController.fromPool.Add(rankUpQuests[3]);
                        questPool.Add(rankUpQuests[3]);
                        return false;
                    }
                }
                break;
            case "S": //for final quest addition upon reaching S rank
                    //if necessary quest is completed
                    if (completedQuests.Contains(rankUpQuests[4]))
                    {
                        SearchForQuests();
                        //else if necessary quest hasn't been unlocked
                    }
                    else if (questPool.Contains(rankUpQuests[4]) == false && activeQuests.Contains(rankUpQuests[4]) == false)
                    {
                        questPoolController.fromPool.Add(rankUpQuests[4]);
                        questPool.Add(rankUpQuests[4]);
                        end.enabled = true;
                        return false;
                    }
                break;
        }
        return false;
    }

}
