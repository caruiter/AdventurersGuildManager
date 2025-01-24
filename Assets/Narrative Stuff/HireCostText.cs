using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class HireCostText : MonoBehaviour
{
    public HiringManager hire;
    public TMP_Text text;
    public TextMeshProUGUI goldDisplay;
    public infoHolder info;

    // Update is called once per frame

    private void OnEnable()
    {
        UpdateGoldText();
    }
    public void UpdateGoldText()
    {
        DialogueLua.SetVariable("HireCost", hire.hireCost);
        text.text = "Cost to Hire a New Adventurer: " + hire.hireCost + "G";
        goldDisplay.text = "Gold: " + info.gold;
    }
}
