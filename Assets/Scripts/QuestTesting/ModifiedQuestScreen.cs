using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ModifiedQuestScreen : MonoBehaviour
{
    public infoHolder info;

    public MenuManager menu;

    public QuestScript nullQ;

    //public List<QuestScript> genericQuests;

    /**
    public TextMeshProUGUI activeList;
    public TextMeshProUGUI poolList;**/

    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI name3;

    public TextMeshProUGUI blurb1;
    public TextMeshProUGUI blurb2;
    public TextMeshProUGUI blurb3;

    public TextMeshProUGUI specs1;
    public TextMeshProUGUI specs2;
    public TextMeshProUGUI specs3;

    public TextMeshProUGUI rank1;
    public TextMeshProUGUI rank2;
    public TextMeshProUGUI rank3;

    public TextMeshProUGUI reward1;
    public TextMeshProUGUI reward2;
    public TextMeshProUGUI reward3;

    public TextMeshProUGUI questCounter;
    public List<QuestScript> fromPool;

    public Button pick1;
    public Button pick2;
    public Button pick3;

    private int newQuestsAdded;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //selects and displays new quests
    public void DrawFromPool()
    {
        Debug.Log("pooldraw");

        //are there enough quests in the pool?
        if (info.questPool.Count >= 3)
        {
            for (int a = fromPool.Count; a < 3; a++)
            {
                //pick 3 random quests from pool
                int c = Random.Range(0, info.questPool.Count);
                Debug.Log(c);
                if (a == 0)
                {
                    fromPool.Add(info.questPool[c]);
                }
                else if (fromPool.Contains(info.questPool[c]) == false || a == 0)
                {
                    fromPool.Add(info.questPool[c]);
                }
                else
                {
                    a--;
                }
            }
        }
        else
        {//add remaining quests
            foreach (QuestScript b in info.questPool)
            {
                fromPool.Add(b);
                //info.questPool.Remove(b);
            }
            //ran out of quests, add a set generic one
            while (fromPool.Count < 3)
            {
                int c = Random.Range(0, info.genericQuestPool.Count);
                fromPool.Add(info.genericQuestPool[c]);
                //adding from generic pool
                if (info.genericQuestPool[c].adventurersAssigned.Count > 0)
                {
                    foreach(AdventurerScript a in info.genericQuestPool[c].adventurersAssigned)
                    {
                        info.genericQuestPool[c].adventurersAssigned.Remove(a);
                    }
                }
                
            }
        }

        //set display info

        name1.text = fromPool[0].questName;
        name2.text = fromPool[1].questName;
        name3.text = fromPool[2].questName;

        blurb1.text = fromPool[0].blurb;
        blurb2.text = fromPool[1].blurb;
        blurb3.text = fromPool[2].blurb;

        specs1.text = fromPool[0].maxAdventurers + " Adventurers";
        specs2.text = fromPool[1].maxAdventurers + " Adventurers";
        specs3.text = fromPool[2].maxAdventurers + " Adventurers";

        rank1.text = "Rank: " + fromPool[0].rank;
        rank2.text = "Rank: " + fromPool[1].rank;
        rank3.text = "Rank: " + fromPool[2].rank;

        reward1.text = fromPool[0].gold + " GOLD/" + fromPool[0].rep + " REP/" + fromPool[0].adventurerExp + " EXP";
        reward2.text = fromPool[1].gold + " GOLD/" + fromPool[1].rep + " REP/" + fromPool[1].adventurerExp + " EXP";
        reward3.text = fromPool[2].gold + " GOLD/" + fromPool[2].rep + " REP/" + fromPool[2].adventurerExp + " EXP";

        //display number of quests chosen so far accounting for the null quest
        if (info.activeQuests.Contains(nullQ))
        {
            questCounter.text = "Quests Taken: " + "0/3";
        }
        else {
            questCounter.text = "Quests Taken: " + info.activeQuests.Count + "/3";
        }
    }

    //activates quest, linked to button
    public void ActivateQuest(int which)
    {
        Debug.Log("first");
        if (info.activeQuests.Contains(nullQ))
        {
            info.activeQuests.Remove(nullQ);
        }
        info.activeQuests.Add(fromPool[which]);
        info.questPool.Remove(fromPool[which]);

        Debug.Log("second");
        //scrub current pool
        for (int f = 0; f <= fromPool.Count-1; f++)
        {
            fromPool.Remove(fromPool[f]);
            Debug.Log("third");
        }
        if (fromPool.Count != 0)
        {
            fromPool.Remove(fromPool[0]);
            Debug.Log("fourth");
        }

        newQuestsAdded++;
        CheckQuestDraw();
        //menu.OpenMainScreen();
    }

    //begins drawing process
    public void BeginQuestDraw()
    {
        Debug.Log("BEGIN QUEST DRAW");
        if (info.activeQuests.Contains(nullQ))
        {
            newQuestsAdded = 0;
        }
        else
        {
            newQuestsAdded = info.activeQuests.Count;
        }
        pick1.interactable = true;
        pick2.interactable = true;
        pick3.interactable = true;
        CheckQuestDraw();
    }

    //check if 3 quests have been selected
    public void CheckQuestDraw()
    {
        Debug.Log("CHECK DRAW: " + newQuestsAdded);
        if (newQuestsAdded >= 3)
        {
            pick1.interactable = false;
            pick2.interactable = false;
            pick3.interactable = false;
            DrawFromPool();
        }
        else
        {
            DrawFromPool();
        }
    }



}


