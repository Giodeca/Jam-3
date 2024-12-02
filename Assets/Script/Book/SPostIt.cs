using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PostIt", menuName = "Puzzle/PostIt")]
public class SPostIt : ScriptableObject
{
    [TextArea]
    public string text;
}
