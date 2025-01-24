using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinNoise : MonoBehaviour
{
    public infoHolder info;
    public int CurrentCoin;
    public AudioSource CoinAudio;
    public AudioSource CoinAudio2;


    private void Start()
    {
        CurrentCoin = info.gold;
    }
    // Update is called once per frame
    void Update()
    {
        if(CurrentCoin > info.gold)
        {
            CoinAudio.Play();
            CurrentCoin = info.gold;
        }
        if (CurrentCoin < info.gold)
        {
            CoinAudio2.Play();
            CurrentCoin = info.gold;
        }
    }
}
