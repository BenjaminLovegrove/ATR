using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Script for the UI used in credits scene

public class Credits : MonoBehaviour
{
    public GameObject atr;
    public GameObject ben;
    public GameObject hux;
    public GameObject jacko;
    public GameObject jess;
    public GameObject voiceActors;
    public GameObject team;
    public AudioClip musicClip;
    
    private float autoExit;
    private float timer;
    private List<int> creditsOrder = new List<int>();
    public int[] order;
    public bool[] sequenceTriggered;    

    // ATR
    public IEnumerator ATR()
    {
        yield return new WaitForSeconds(1f);
        atr.SetActive(true);
        yield return new WaitForSeconds(3f);
        atr.SetActive(false);
    }

    // BENNY
    public IEnumerator Sequence1()
    {
        ben.SetActive(true);
        yield return new WaitForSeconds(14f);
        ben.SetActive(false);
    }
    
    // HUX
    public IEnumerator Sequence2()
    {
        hux.SetActive(true);
        yield return new WaitForSeconds(14f);
        hux.SetActive(false);
    }
    
    // JACKO
    public IEnumerator Sequence3()
    {
        jacko.SetActive(true);
        yield return new WaitForSeconds(14f);
        jacko.SetActive(false);
    }
    
    // JESS
    public IEnumerator Sequence4()
    {
        jess.SetActive(true);
        yield return new WaitForSeconds(14f);
        jess.SetActive(false);
    }

    // VOICE ACTORS
    public IEnumerator Sequence5()
    {
        voiceActors.SetActive(true);
        yield return new WaitForSeconds(9f);
        voiceActors.SetActive(false);
    }

    // TEAM
    public IEnumerator Sequence6()
    {
        team.SetActive(true);
        yield return new WaitForSeconds(7.5f);
        team.SetActive(false);
    }

    void Awake()
    {
        InitialiseValues();
    }

    void Start()
    {
        autoExit = musicClip.length;
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
        if (timer > 20 && !sequenceTriggered[1])
        {
            sequenceTriggered[1] = true;
            StartCoroutine("Sequence" + order[1]);
        }

        // Third team member
        if (timer > 35 && !sequenceTriggered[2])
        {
            sequenceTriggered[2] = true;
            StartCoroutine("Sequence" + order[2]);
        }

        // Fourth team member
        if (timer > 50 && !sequenceTriggered[3])
        {
            sequenceTriggered[3] = true;
            StartCoroutine("Sequence" + order[3]);
        }

        // Voice actors
        if (timer > 65 && !sequenceTriggered[4])
        {
            sequenceTriggered[4] = true;
            StartCoroutine("Sequence5");
        }

        // Voice actors
        if (timer > 75 && !sequenceTriggered[5])
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
