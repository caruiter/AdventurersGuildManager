using FMOD.Studio;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public GameObject menuScreen;
    public GameObject creditsScreen;
    public GameObject guideScreen;

    // List of Banks to load
    [FMODUnity.BankRef]
    public List<string> banks;

    public string Scene = "CompilationScene";

    public List<GameObject> panels;

    public void Start()
    {
        OpenMenu();
    }


    public void PlayGame()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("CompilationScene");
        StartCoroutine(LoadGameAsync());
    }


    public void OpenCredits()
    {
        menuScreen.SetActive(false);
        creditsScreen.SetActive(true);
    }

    public void OpenMenu()
    {
        menuScreen.SetActive(true);
        creditsScreen.SetActive(false);
        guideScreen?.SetActive(false);
    }

    public void OpenGuide()
    {
        guideScreen?.SetActive(true);
        menuScreen.SetActive(false);
    }


    public void Main_Menu()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 0);
    }

    public void OptionGame()
    {

    }

    public void DisplayPanel(int panel)
    {
        foreach (GameObject p in panels)
        {
            p.SetActive(false);
        }
        panels[panel].SetActive(true);
    }

    public void QuitGame()
    {
        Application.Quit();
    }


    IEnumerator LoadGameAsync()
    {
        // Start an asynchronous operation to load the scene
        AsyncOperation async = SceneManager.LoadSceneAsync(Scene);

        // Don't let the scene start until all Studio Banks have finished loading
        async.allowSceneActivation = false;

        // Iterate all the Studio Banks and start them loading in the background
        // including the audio sample data
        foreach (string b in banks)
        {
            FMODUnity.RuntimeManager.LoadBank(b, true);
            Debug.Log("Loaded bank " + b);
        }
        /*
            For Chrome / Safari browsers / WebGL.  Reset audio on response to user interaction (LoadBanks is called from a button press), to allow audio to be heard.
        */
        FMODUnity.RuntimeManager.CoreSystem.mixerSuspend();
        FMODUnity.RuntimeManager.CoreSystem.mixerResume();

        // Keep yielding the co-routine until all the bank loading is done
        // (for platforms with asynchronous bank loading)
        while (!FMODUnity.RuntimeManager.HaveAllBanksLoaded)
        {
            yield return null;
        }

        // Keep yielding the co-routine until all the sample data loading is done
        while (FMODUnity.RuntimeManager.AnySampleDataLoading())
        {
            yield return null;
        }

        // Allow the scene to be activated. This means that any OnActivated() or Start()
        // methods will be guaranteed that all FMOD Studio loading will be completed and
        // there will be no delay in starting events
        async.allowSceneActivation = true;

        // Keep yielding the co-routine until scene loading and activation is done.
        while (!async.isDone)
        {
            yield return null;
        }

    }
}
