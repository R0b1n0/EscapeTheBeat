using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScjukeBox : MonoBehaviour
{
    public static ScjukeBox Instance;
    [SerializeField] List<Song> playlist = new List<Song>();
    [SerializeField] Intervales[] triggerBeat;
    private AudioSource musicPlayer;
    private Song nowlaying;


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
        nowlaying = playlist[Random.Range(0, playlist.Count)];
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.clip = nowlaying.track;
        musicPlayer.Play();

    }

    private void Update()
    {
        foreach (Intervales interval in triggerBeat)
        {
            Debug.Log(interval.IntervalLengh((nowlaying.bpm)));
            float sampleTime = musicPlayer.timeSamples / (nowlaying.track.frequency * interval.IntervalLengh((nowlaying.bpm)));
            OnBeatPortionEvent(interval.CheckForNewInterval(sampleTime));
        }
        
        
    }

    public void OnBeatPortionEvent(beatPortion portion)
    {
        switch (portion)
        {
            case beatPortion.full:
                
                break;

            case beatPortion.half:
                
                break;
            case beatPortion.empty:
                
                break;
        }
    }
}

[System.Serializable]
public struct Song
{
    public AudioClip track;
    public int bpm;
}

[System.Serializable]
public class Intervales
{
    [SerializeField] public float step; //1 = once beat, 0.5f = twice a beat
    [SerializeField] private beatPortion portion;

    private int lastInterval;

    public float  IntervalLengh(int bpm)
    {
        return 60f / (bpm - step);
    }

    public beatPortion CheckForNewInterval(float interval)
    {
        if (Mathf.FloorToInt(interval) != lastInterval)
        {
            lastInterval = Mathf.FloorToInt(interval);
            return portion;
        }
        else
            return beatPortion.empty;
    }
}
public enum beatPortion
{
    full,
    quarter,
    half,
    third,
    empty
}