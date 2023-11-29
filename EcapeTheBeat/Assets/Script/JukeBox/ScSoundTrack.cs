using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScSoundTrack : MonoBehaviour
{
    public static ScSoundTrack Instance;
    private AudioSource audioSource;
    [SerializeField] int bpm;
    private float gap;
    private int sampleCount;
    private int previousSample = -1;
    private int timeSignature = 4;
    private float custonTime;


    [SerializeField] private List<ScSample> parts = new List<ScSample>();



    public UnityEvent<instrument> playNote;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Destroy(this);
            return;
        }
    }
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gap = GetTwodigitFloat(60f / bpm);
    }

    private void Update()
    {
        custonTime += Time.deltaTime;
        if (custonTime > gap)
        {
            custonTime = 0;
            sampleCount += 1;
        }
        
        if (sampleCount != previousSample)
        {
            previousSample = sampleCount;
            foreach(ScSample part in parts)
            {
                foreach(sampleLine line in part.note)
                {
                    if (line.line[sampleCount % line.line.Count] != instrument.mute)
                        playNote.Invoke(line.line[sampleCount % 8]);
                }
            }
        }
    }

    private float GetTwodigitFloat(float valueToRound)
    {
        return (Mathf.RoundToInt(valueToRound * 100f)) / 100f;
    }
}


public enum instrument
{
    mute,
    drumKick,
    snare,
    highHat
}
