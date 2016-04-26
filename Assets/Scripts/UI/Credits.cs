using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

// Script for the UI used in credits scene

public class Credits : MonoBehaviour
{
    [Header("ATR")]
    public Text atr;
    
    [Header("Ben")]
    public Text benHeading;
    public Text benRole0;
    public Text benRole1;
    public Text benWeb;
    
    [Header("Hux")]
    public Text huxHeading;
    public Text huxRole0;
    public Text huxRole1;
    public Text huxWeb;
    
    [Header("Jack")]
    public Text jackHeading;
    public Text jackRole0;
    public Text jackRole1;
    public Text jackRole2;
   
    [Header("Jess")]
    public Text jessHeading;
    public Text jessRole0;
    public Text jessRole1;
    public Text jessRole2;
   
    [Header("Voice Actors")]
    public Text voiceHeading;
    public Text adrian;
    public Text christy;
    public Text tony;
   
    [Header("Team")]
    public Text teamHeading;
    
    [Header("Containers")]
    public Text[] textObjs;
    public Image[] imageObjs;
    public int[] order;                 // Set elements to 4 (Inspector)
    public bool[] sequenceTriggered;    // Set elements to 6 (Inspector)

    private float autoExit;
    private float timer;
    private List<int> creditsOrder = new List<int>();
  

    // ATR 4 - 4secs
    public IEnumerator ATR()
    {
        atr.CrossFadeAlpha(1, 0.25f, false);
        yield return new WaitForSeconds(3f);
        atr.CrossFadeAlpha(0, 1f, false);
        yield return new WaitForSeconds(1f);
    }

    // BENNY - 10secs
    public IEnumerator Sequence1()
    {
        benHeading.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1f);
        benRole0.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        benRole1.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        benWeb.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(7f);

        //Fade out
        benHeading.CrossFadeAlpha(0, 1, false);
        benRole0.CrossFadeAlpha(0, 1, false);
        benRole1.CrossFadeAlpha(0, 1, false);
        benWeb.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1f);
    }
    
    // HUX - 10secs
    public IEnumerator Sequence2()
    {
        huxHeading.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1f);
        huxRole0.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        huxRole1.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        huxWeb.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(7f);

        //Fade out
        huxHeading.CrossFadeAlpha(0, 1, false);
        huxRole0.CrossFadeAlpha(0, 1, false);
        huxRole1.CrossFadeAlpha(0, 1, false);
        huxWeb.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1f);
    }
    
    // JACKO - 10secs
    public IEnumerator Sequence3()
    {
        jackHeading.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1f);
        jackRole0.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        jackRole1.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        jackRole2.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(7f);

        //Fade out
        jackHeading.CrossFadeAlpha(0, 1, false);
        jackRole0.CrossFadeAlpha(0, 1, false);
        jackRole1.CrossFadeAlpha(0, 1, false);
        jackRole2.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1f);
    }
    
    // JESS - 10secs
    public IEnumerator Sequence4()
    {
        jessHeading.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1f);
        jessRole0.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        jessRole1.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        jessRole2.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(7f);

        //Fade out
        jessHeading.CrossFadeAlpha(0, 1, false);
        jessRole0.CrossFadeAlpha(0, 1, false);
        jessRole1.CrossFadeAlpha(0, 1, false);
        jessRole2.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1f);
    }

    // VOICE ACTORS - 10secs
    public IEnumerator Sequence5()
    {
        voiceHeading.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(1f);
        christy.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        tony.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(0.5f);
        adrian.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(7f);

        //Fade out
        voiceHeading.CrossFadeAlpha(0, 1, false);
        adrian.CrossFadeAlpha(0, 1, false);
        christy.CrossFadeAlpha(0, 1, false);
        tony.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1f);
    }

    // TEAM - 8secs
    public IEnumerator Sequence6()
    {
        teamHeading.CrossFadeAlpha(1, 1, false);
        yield return new WaitForSeconds(7f);
        teamHeading.CrossFadeAlpha(0, 1, false);
        yield return new WaitForSeconds(1f);
    }

    void Awake()
    {
        InitialiseValues();
    }

    void Start()
    {
        autoExit = 64;
        StartCoroutine("ATR");

        // Reset vals to normal
        EventManager.inst.credits = false;
        EventManager.inst.atEndTerrain = false;
        EventManager.inst.memoryPlaying = false;
        EventManager.inst.currentCheckPoint = 0;
        EventManager.inst.memoryLookScalar = 1;
        EventManager.inst.memoryMoveScalar = 1;
        Cursor.visible = false;
    }

    void Update()
    {
        timer += Time.deltaTime;

        SequenceCalls();

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.Space))
        {
            Application.LoadLevel("MainMenu");
        }
    }
    
    void InitialiseValues()
    {
        for (int i = 0; i < 4; i++)
        {
            creditsOrder.Add(i + 1);
        }

        for (int j = 0; j < 4; j++)
        {
            order[j] = ReturnRandomInt();
        }

        for (int k = 0; k < 6; k++)
        {
            sequenceTriggered[k] = false;
        }

        for (int l = 0; l < textObjs.Length; l++)
        {
            textObjs[l].CrossFadeAlpha(0, 0, false);
        }

        for (int m = 0; m < imageObjs.Length; m++)
        {
            imageObjs[m].CrossFadeAlpha(0, 0, false);
        }
    }

    void SequenceCalls()
    {
        // First team member
        if (timer > 5 && !sequenceTriggered[0])
        {
            sequenceTriggered[0] = true;
            StartCoroutine("Sequence" + order[0]);
        }

        // Second team member
        if (timer > 15 && !sequenceTriggered[1])
        {
            sequenceTriggered[1] = true;
            StartCoroutine("Sequence" + order[1]);
        }

        // Third team member
        if (timer > 25 && !sequenceTriggered[2])
        {
            sequenceTriggered[2] = true;
            StartCoroutine("Sequence" + order[2]);
        }

        // Fourth team member
        if (timer > 35 && !sequenceTriggered[3])
        {
            sequenceTriggered[3] = true;
            StartCoroutine("Sequence" + order[3]);
        }

        // Voice actors
        if (timer > 45 && !sequenceTriggered[4])
        {
            sequenceTriggered[4] = true;
            StartCoroutine("Sequence5");
        }

        // Team
        if (timer > 55 && !sequenceTriggered[5])
        {
            sequenceTriggered[5] = true;
            StartCoroutine("Sequence6");
        }

        if (timer > autoExit)
        {
            Application.LoadLevel("MainMenu");
        }
    }

    int ReturnRandomInt()
    {
        int rand = Random.Range(0, creditsOrder.Count);
        int temp = creditsOrder[rand];
        creditsOrder.RemoveAt(rand);
        return temp;
    }
}
