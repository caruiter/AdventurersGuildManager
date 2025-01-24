using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
using UnityEditor;

public class VariableUpdater : MonoBehaviour
{
    public infoHolder info;
    public HiringManager hire;
    public IntroSequencer intro;
    public MenuManager menu;
    void FixedUpdate()
    {
        if (intro.IntroState != 8 && intro.IntroState != 9)
        {
            DialogueLua.SetVariable("QuestsCompleted", info.questsCompleted);
            intro.IntroState = DialogueLua.GetVariable("IntroState").asInt;
        }

        DialogueLua.SetVariable("GoldSpent", info.goldSpent);
        DialogueLua.SetVariable("negativeMoney", info.negativeMoney);

        info.guildName = DialogueLua.GetVariable("GuildName").asString;
    }

    public void moneyBoon()
    {
        info.gold = 25;
        info.negativeMoney = false;
        menu.UpdateGold();
    }
}

