using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip pageTurn;
    public AudioClip fanfare;
    public AudioClip cymbals;
    public AudioClip guitar;
    public AudioClip scribble;
    public AudioClip scribble2;
    public AudioClip clickClip;


    private AudioSource au;

    // Start is called before the first frame update
    void Start()
    {
        au = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void playPageTurn(){
        au.volume = 1f;
        au.PlayOneShot(pageTurn);
        //Debug.Log("PLAYYY");
    }
    public void playFanfare(){
        au.volume = 0.5f;
        au.PlayOneShot(fanfare);
    }

    public void playScribble(){
        au.volume = 1f;
        au.PlayOneShot(scribble);
    }

    public void playCymbals(){
        au.volume = 0.2f;
        au.PlayOneShot(cymbals);
    }
    public void playGuitar(){
        au.volume = 0.5f;
        au.PlayOneShot(guitar);
    }

    public void playClick(){
        au.volume = 1f;
        au.PlayOneShot(clickClip);
    }

    public void playScribble2(){
        au.volume = 1f;
        au.PlayOneShot(scribble2);
    }
    
}
