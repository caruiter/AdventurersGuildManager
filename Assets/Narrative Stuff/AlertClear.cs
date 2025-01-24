using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;

public class AlertClear : MonoBehaviour
{
    public void AlertClearing()
    {
        DialogueLua.SetVariable("Alert", "");
    }
}
