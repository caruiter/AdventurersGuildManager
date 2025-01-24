using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ConvoController : MonoBehaviour
{

    public TextMeshProUGUI lineText;
    public TextMeshProUGUI nameText;
    public GameObject dialogueWindow;

    public GameObject buttonHolder;
    public Button button1;
    public Button button2;

    private int index;

    public SimpleConvo currentConvo;

    // Start is called before the first frame update
    void Start()
    {
        index = 0;
        lineText.text = currentConvo.theLines[0];
        nameText.text = currentConvo.theNames[0];

        buttonHolder.SetActive(false);
    }

    //player clicks to call next line of dialogue
    public void CallNextLine()
    {
        Debug.Log("click: " + index + "; " + currentConvo.theLines.Count);
        if (index < currentConvo.theLines.Count-1)
        {
            index++;


            //if on the last line of dialogue
            if (index == currentConvo.theLines.Count && currentConvo.isBranching != true)
            {
                FinishDialogue();
            }
            //if the dialogue is branching
            else if (index == currentConvo.theLines.Count-1 && currentConvo.isBranching == true)
            {
                CallFinalLine();
            }


            lineText.text = currentConvo.theLines[index];
            nameText.text = currentConvo.theNames[index];

        }
    }

    //if branching, pulls up option buttons
    public void CallFinalLine()
    {
        if (currentConvo.isBranching)
        {
            Debug.Log("okay!");

            buttonHolder.SetActive(true);

            button1.GetComponentInChildren<TextMeshProUGUI>().text = currentConvo.branchOpt1;
            button2.GetComponentInChildren<TextMeshProUGUI>().text = currentConvo.branchOpt2;

            button1.onClick.AddListener(BeginBranch1);
            button2.onClick.AddListener(BeginBranch2);

        }
    }

    //closes dialogue window
    public void FinishDialogue()
    {
        dialogueWindow.SetActive(false);
    }

    //begins the first branch
    public void BeginBranch1()
    {
        currentConvo = currentConvo.branch1.GetComponent<SimpleConvo>();
        index = 0;
        lineText.text = currentConvo.theLines[0];
        nameText.text = currentConvo.theNames[0];
        Debug.Log(currentConvo.name);

        buttonHolder.SetActive(false);
    }

    //begins the second branch
    public void BeginBranch2()
    {

        currentConvo = currentConvo.branch2.GetComponent<SimpleConvo>();
        index = 0;
        lineText.text = currentConvo.theLines[0];
        nameText.text = currentConvo.theNames[0];
        Debug.Log(currentConvo.name);

        buttonHolder.SetActive(false);
    }
}
