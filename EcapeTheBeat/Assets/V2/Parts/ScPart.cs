using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "part", menuName = "part")]
public class ScPart : ScriptableObject
{
    public bool screenShake;
    public note instrumentToPlay;
    public int instrumentIndex; // set negativ to play all instrument of "instrumentToPlay" kind

}
