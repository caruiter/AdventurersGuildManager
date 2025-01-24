using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Reflection;
using PixelCrushers.DialogueSystem;
using PixelCrushers.DialogueSystem.UnityGUI.Wrappers;
using PixelCrushers.DialogueSystem.UnityGUI;
using PixelCrushers.DialogueSystem.Wrappers;

// To add a button to toggle continue button mode, add this script to the 
// Dialogue Manager. Then connect the button's OnClick() event to 
// ToggleContinue.ToggleContinueMode.
public class ToggleContinue : MonoBehaviour
{
    public PixelCrushers.DialogueSystem.TextMeshProTypewriterEffect type;
    public void ToggleContinueButtonMode()
    {
        // Toggle modes:
        var oldMode = DialogueManager.DisplaySettings.subtitleSettings.continueButton;
        var newMode = (oldMode == DisplaySettings.SubtitleSettings.ContinueButtonMode.Always) ? DisplaySettings.SubtitleSettings.ContinueButtonMode.Never
            : DisplaySettings.SubtitleSettings.ContinueButtonMode.Always;
        DialogueManager.DisplaySettings.subtitleSettings.continueButton = newMode;
        if (DialogueManager.ConversationView != null) DialogueManager.ConversationView.SetupContinueButton();

        // If turning continue mode off and the sequence is already done, auto-continue.
        if (DialogueManager.IsConversationActive && newMode == DisplaySettings.SubtitleSettings.ContinueButtonMode.Never)
        {
            if (!type.IsPlaying) DialogueManager.Instance.BroadcastMessage("OnContinue", SendMessageOptions.DontRequireReceiver);
        }
    }
}