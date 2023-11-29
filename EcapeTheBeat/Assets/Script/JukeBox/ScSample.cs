using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "sample", menuName ="sample")]
public class ScSample : ScriptableObject
{
    public List<sampleLine> note;
}


[System.Serializable]
public struct sampleLine
{
    public List<instrument> line;
}