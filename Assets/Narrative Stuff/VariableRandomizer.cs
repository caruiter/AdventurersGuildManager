using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PixelCrushers.DialogueSystem;
public class VariableRandomizer : MonoBehaviour
{
    
    private string randomEnemyD;
    [SerializeField] private string[] enemyListD;
    private int randomNumber;
    public void RandomizeD()
    {
        randomNumber = Random.Range(0, enemyListD.Length);
        randomEnemyD = enemyListD[randomNumber];
        DialogueLua.SetVariable("DQuestTarget", randomEnemyD);
        print(randomEnemyD);
    }
}
