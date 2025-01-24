using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class LetterScript : MonoBehaviour
{
    public infoHolder info;
    public bool unreadMessage = false;
    public int readMessages;
    public int messagesToRead;
    public GameObject unreadMessageIndicator;
    public IntroSequencer intro;

    public int rankToInt;
    [SerializeField] private List<Button> button;
    [SerializeField] private List<TMP_Text> text;
    [SerializeField] private TMP_Text nullText;

    private void Update()
    {
        if(messagesToRead != readMessages)
        {
            unreadMessageIndicator.SetActive(true);
        }
        else
        {
            unreadMessageIndicator.SetActive(false);
        }
    }
    public void CheckMessages()
    {
        //set rank to int for functions later
        if (info.guildRank == "D")
        {
            rankToInt = 0;
        }
        if (info.guildRank == "C")
        {
            rankToInt = 1;
        }
        if (info.guildRank == "B")
        {
            rankToInt = 2;
        }
        if (info.guildRank == "A")
        {
            rankToInt = 3;
        }
        if (info.guildRank == "S")
        {
            rankToInt = 4;
        }
        if (intro.IntroState == 8 || intro.IntroState == 9)
        {
            //checks rank, week, and if button is already active. sets button active and adds 1 to "messages to read", for unread messages check.
            if (rankToInt >= 0 && info.weeks > 3 && button[0].interactable == false)
            {
                button[0].gameObject.SetActive(true);
                button[0].interactable = true;
                text[0].text = "Letter From Erio: Belonging";
                messagesToRead++;
            }
            else if (rankToInt >= 1 && info.weeks > 5 && button[1].interactable == false)
            {
                button[1].gameObject.SetActive(true);
                button[1].interactable = true;
                text[1].text = "Letter From Erio: Council of Mages";
                messagesToRead++;
            }
            else if (rankToInt >= 2 && info.weeks > 9 && button[2].interactable == false)
            {
                button[2].gameObject.SetActive(true);
                button[2].interactable = true;
                text[2].text = "Letter From Erio: Crime in Wilsor";
                messagesToRead++;
            }
            else if (rankToInt >= 3 && button[3].interactable == false)
            {
                button[3].gameObject.SetActive(true);
                button[3].interactable = true;
                text[3].text = "Letter From Erio: Overseas Expedition";
                messagesToRead++;
            }
            else if (rankToInt >= 4 && button[4].interactable == false)
            {
                button[4].gameObject.SetActive(true);
                button[4].interactable = true;
                text[4].text = "Letter From Erio: Ranking Ceremony";
                messagesToRead++;
            }
            else if (rankToInt >= 1 && button[5].interactable == false)
            {
                button[5].gameObject.SetActive(true);
                button[5].interactable = true;
                text[5].text = "Letter From Gramps: Growth";
                messagesToRead++;
            }
            else if (rankToInt >= 4 && button[6].interactable == false)
            {
                button[6].gameObject.SetActive(true);
                button[6].interactable = true;
                text[6].text = "Letter From Gramps: Farewell";
                messagesToRead++;
            }
            else if (rankToInt >= 0 && info.reputation >= 15 && button[7].interactable == false)
            {
                button[7].gameObject.SetActive(true);
                button[7].interactable = true;
                text[7].text = "Letter From Erio: Ranking Up";
                messagesToRead++;
            }
        }
        if(messagesToRead > 0)
        {
            nullText.enabled = false;
        }
    }

   public void ReadMessage()
    {
        if(messagesToRead != readMessages)
        {
            readMessages++;
        }
    }
}
