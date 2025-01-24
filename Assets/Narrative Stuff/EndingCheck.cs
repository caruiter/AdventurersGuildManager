using PixelCrushers.DialogueSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EndingCheck : MonoBehaviour
{
    public ConditionObserver con;

    public GameObject endingstuff;
    public bool ended = false;
    public GameObject results;
    public PlayableDirector END;
    // Update is called once per frame
    void Update()
    {
            con.enabled = true;

        if (ended == true && results.activeInHierarchy == false)
        {
            endingstuff.SetActive(true);
            END.Play();
            this.enabled = false;
        }

    }
}
