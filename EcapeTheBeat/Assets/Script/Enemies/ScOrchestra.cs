using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScOrchestra : MonoBehaviour
{
    /*orchestra is used to play all the sounds according to the ScSoundTrack
    it also triggers the enemies attack waves */
    public static ScOrchestra Instance;

    [SerializeField] AudioSource drumKick;
    [SerializeField] AudioSource snare;
    [SerializeField] AudioSource highHat;

    public UnityEvent drumKickEvent;
    public UnityEvent snareEvent;

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
        ScSoundTrack.Instance.playNote.AddListener(PlaySound);
    }

    private void PlaySound(instrument instruToPlay)
    {
        switch (instruToPlay) 
        {
            case instrument.drumKick:
                drumKick.Play();
                drumKickEvent.Invoke();
                break;
            case instrument.highHat:
                highHat.Play();
                
                break;
            case instrument.snare:
                snare.Play();
                snareEvent.Invoke();
                break;
        }
    }

}
