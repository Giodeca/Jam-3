using UnityEngine;

[CreateAssetMenu(fileName = "new dialogue", menuName = "Dialogue")]
public class SDialogue : ScriptableObject
{

    [Header("Dialogue's Background")]
    public Sprite Background;

    [Header("Character's Name")]
    public string F_Name;
    public string S_Name;

    [Header("Character's Image")]
    public Sprite F_Image;
    public Sprite S_Image;

    [Header("Dialogue")]
    public bool FirstOnRight;
    public DialoguePart[] F_CharacterDialogues;
    public DialoguePart[] S_CharacterDialogues;
    public bool endScene;
}
