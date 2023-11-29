using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScMobMan : MonoBehaviour 
{
    public static ScMobMan Instance;
    [SerializeField] List<ScSnare> snareList = new List<ScSnare>();
    [SerializeField] List<Scspray> sprayList = new List<Scspray>();
    [SerializeField] ScScreenShake camShake;
    [SerializeField] GameObject beamRef;
    [SerializeField] ScCircle circleRef;

    private List<ScBeam> beamList = new List<ScBeam>() ;

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

    public void Play(ScPart part)
    {
       switch (part.instrumentToPlay) 
       {
            case note.kick:
                if (part.instrumentIndex < 0)
                {
                    foreach (ScSnare snare in snareList)
                        snare.Shoot();
                }
                else
                {
                    snareList[part.instrumentIndex].Shoot();
                }
                break;

            case note.beam:
                FindBeam();
                break;

            case note.circle:
                circleRef.Shoot();
                break;

            case note.spray:
                if (part.instrumentIndex < 0)
                {
                    foreach (Scspray spray in sprayList)
                        spray.Shoot();
                }
                else
                {
                    sprayList[part.instrumentIndex].Shoot();
                }
                break;
       }
        if (part.screenShake)
            camShake.Shake(0.35f,0.15f);
    }

    private void FindBeam() //find inactiv Beam ennemies, create one if needed 
    {
        if (beamList.Count != 0)
        {
            beamList[0].gameObject.SetActive(true);
            beamList[0].Shoot();
            beamList.RemoveAt(0);
            
        }
        else
        {
            var tempo = Instantiate(beamRef);
            tempo.gameObject.SetActive(true);
            tempo.GetComponent<ScBeam>().Shoot();
        }
    }

    public void GetBeam(ScBeam retiredBeam)
    {
        beamList.Add(retiredBeam);
    }
}

public enum note
{
    kick, //zero=TopRight  One=BottomRight Two=BottoLeft Three=TopLeft
    dash,
    beam,
    circle,
    spray
}
