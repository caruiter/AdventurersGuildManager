using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestScript : MonoBehaviour
{

    public string questName;
    public string blurb;
    public string objective;
    public string rank;
    public int maxAdventurers;
    public string onCompletion;
    public bool sent;
    public bool time;
    //public bool rankUpQuest;
    public bool FinalQuest;

    public List<AdventurerScript> adventurersAssigned;

    public string rewardDisplay;
    //public List<string> rewardList;
    public int gold;
    public int rep;
    public int adventurerExp;

    public int baseProbability;
    public int currentProbability;

    public int enduranceCost;
    public string bonusStat;
    public int bonusStatInt;

    public int atkRangeMin;
    public int atkRangeMax;
    public int defRangeMin;
    public int defRangeMax;
    public int healthRangeMin;
    public int healthRangeMax;


    // Start is called before the first frame update
    void Start()
    { }

    // Update is called once per frame
    void Update()
    {
        
    }

    //calculate probability based on adventurer's endurance
    public void CalculateProbability()
    {
        if(adventurersAssigned.Count != 0){
            int probAdjust = 0;

            foreach(AdventurerScript adv in adventurersAssigned)
            {
                int end = adv.currentEndurance;
                probAdjust += end;
            }
            int check = baseProbability;
            currentProbability = check + (probAdjust * 5);
        }else{
        currentProbability = 0;
        }
        
    } 
}
