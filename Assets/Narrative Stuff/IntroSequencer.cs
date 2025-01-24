using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEngine.UI;
using UnityEngine.Playables;

public class IntroSequencer : MonoBehaviour
{
    public int IntroState = 0;
    public int ReadState = 0;

    public TimelineTrigger trigger1;
    public TimelineTrigger trigger2;
    public TimelineTrigger trigger3;
    public PlayableDirector trigger4;

    public infoHolder info;

    public Button tavernButton;
    public Button shopButton;
    public Button questButton;
    public Button nextWeekButton;

    private void Start()
    {
        tavernButton.interactable = false;
        questButton.interactable = false;
        nextWeekButton.interactable = false;
        shopButton.interactable = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (IntroState != ReadState && DialogueManager.isConversationActive == false)
        {
            if (IntroState == 1)
            {
                trigger1.OnUse();
            }
            if (IntroState == 2)
            {
                trigger2.OnUse();
            }
            if (IntroState == 3 || IntroState == 4)
            {
                tavernButton.interactable = true;
            }
            if (IntroState == 5)
            {
                tavernButton.interactable = false;
                questButton.interactable = true;
                if (info.sentQuests.Count > 0)
                {
                    nextWeekButton.interactable = true;
                }
            }
            if (IntroState == 6)
            {
                tavernButton.interactable = false;
                questButton.interactable = false;
                nextWeekButton.interactable = false;
                shopButton.interactable = true;
            }
            if(IntroState == 7)
            {
                trigger3.OnUse();

            }
            if(IntroState == 8)
            {
                tavernButton.interactable = true;
                questButton.interactable = true;
                nextWeekButton.interactable = true;
                shopButton.interactable = true;
                this.enabled = false;
            }
            if (IntroState == 9)
            {
                tavernButton.interactable = true;
                questButton.interactable = true;
                nextWeekButton.interactable = true;
                shopButton.interactable = true;
                trigger4.Play();
                this.enabled = false;
            }
        }
    }
    public void ReadPlus()
    {
        ReadState++;
    }

    public void DebugGoldSpent()
    {
        info.goldSpent = 1;
    }
}
