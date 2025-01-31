using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuppWeekEndFade : MonoBehaviour
{
    [SerializeField] MenuManager menem;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void WeekFadeDone(){
        menem.CheckNextQuest();
        menem.CheckTax();
    }
}
