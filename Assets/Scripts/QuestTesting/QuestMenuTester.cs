using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestMenuTester : MonoBehaviour
{
    public infoHolder info;

    public GameObject detailScreen;
    public GameObject buttonScreen;
    public GameObject questScreen;

    public QuestScript genericQuest;

    /**
    public Button option1;
    public Button option2;
    public Button option3;
    **/

    public TextMeshProUGUI activeList;
    public TextMeshProUGUI poolList;

    public TextMeshProUGUI name1;
    public TextMeshProUGUI name2;
    public TextMeshProUGUI name3;

    public TextMeshProUGUI blurb1;
    public TextMeshProUGUI blurb2;
    public TextMeshProUGUI blurb3;

    public TextMeshProUGUI specs1;
    public TextMeshProUGUI specs2;
    public TextMeshProUGUI specs3;

    public List<QuestScript> fromPool;

    // Start is called before the first frame update
    void Start()
    {
        detailScreen.SetActive(false);
        questScreen.SetActive(false);
        //fromPool.Add();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //returns to button screen
    public void OpenButtonScreen()
    {
        buttonScreen.SetActive(true);
        questScreen.SetActive(false);
        detailScreen.SetActive(false);
    }

    //shows detail screen
    public void OpenDetailScreen()
    {
        buttonScreen.SetActive(false);
        questScreen.SetActive(false);
        detailScreen.SetActive(true);

        //get info from the infoholder
        string pool = "";
        foreach(QuestScript g in info.questPool)
        {
            pool += "\n" + g.questName;
        }
        string active = "";
        foreach (QuestScript c in info.activeQuests)
        {
            active += "\n" + c.questName;
        }
        activeList.text = active;
        poolList.text = pool;
    }


    //opens quest screen, selects new quests
    public void OpenQuestScreen()
    {
        buttonScreen.SetActive(false);
        detailScreen.SetActive(false);
        questScreen.SetActive(true);
        DrawFromPool();
    }

    //selects and displays new quests
    public void DrawFromPool()
    {

        //are there enough quests in the pool?
        if (info.questPool.Count > 3)
        {
            for (int a = 0; a < 3; a++)
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
            foreach(QuestScript b in info.questPool)
            {
                fromPool.Add(b);
                //info.questPool.Remove(b);
            }
            //ran out of quests, add a set generic one
            while (fromPool.Count < 3)
            {
                fromPool.Add(genericQuest);
            }
        }

        //set display info

        name1.text = fromPool[0].questName;
        name2.text = fromPool[1].questName;
        name3.text = fromPool[2].questName;

        blurb1.text = fromPool[0].blurb;
        blurb2.text = fromPool[1].blurb;
        blurb3.text = fromPool[2].blurb;

        specs1.text = "Max Adventurers: " + fromPool[0].maxAdventurers;
        specs2.text = "Max Adventurers: " + fromPool[1].maxAdventurers;
        specs3.text = "Max Adventurers: " + fromPool[2].maxAdventurers;

    }

    //activates quest, linked to button
    public void ActivateQuest(int which)
    {
        info.activeQuests.Add(fromPool[which]);
        info.questPool.Remove(fromPool[which]);

        //scrub current pool
        for (int f = 0; f <= fromPool.Count; f++)
        {
            fromPool.Remove(fromPool[f]);
        }
        if (fromPool.Count != 0)
        {
            fromPool.Remove(fromPool[0]);
        }


        OpenButtonScreen();
    }


}
