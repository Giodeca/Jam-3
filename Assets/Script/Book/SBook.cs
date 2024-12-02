using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PostIt", menuName = "Puzzle/Book")]
public class SBook : ScriptableObject
{
    [TextArea]
    public string FirstPage, SecondPage;
}
