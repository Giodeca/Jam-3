using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[Serializable]
public struct UICharacter
{
    public GameObject CharacterPanel;
    public Image CharacterImage;
    public TextMeshProUGUI CharacterName;
    public TextMeshProUGUI DialogueText;
    [NonSerialized] public List<DialoguePart> DialoguePart;
}
[Serializable]
public struct DialoguePart
{
    [TextArea]
    public string[] text;
}
public enum PersonDialogue
{
    None,
    FirstPerson,
    SecondPerson
}
public class DialogueLogic : MonoBehaviour
{
    private TextMeshProUGUI textToUse;
    [SerializeField] private Image Background;
    [SerializeField] PersonDialogue Current_Character;
    [SerializeField] private UICharacter F_Character, S_Character;
    private bool activateWithness;

    int F_internalIndex;
    int F_Index;
    int S_internalIndex;
    int S_Index;

    bool startCoroutine;

    bool endScene;



    private void OnIsThisWitness()
    {
        activateWithness = true;
    }

    private void OpenDialogue(SDialogue current)
    {
        if(GameManager.Instance != null)
            GameManager.Instance.canClick = false;
        
        Background.gameObject.SetActive(true);
        Background.sprite = current.Background;

        F_Character.DialoguePart = new List<DialoguePart>();
        S_Character.DialoguePart = new List<DialoguePart>();

        F_Character.CharacterName.text = current.F_Name;
        S_Character.CharacterName.text = current.S_Name;

        F_Character.CharacterImage.sprite = current.F_Image;
        S_Character.CharacterImage.sprite = current.S_Image;

        for (int i = 0; i < current.F_CharacterDialogues.Length; i++)
        {
            F_Character.DialoguePart.Add(current.F_CharacterDialogues[i]);
        }
        for (int i = 0; i < current.S_CharacterDialogues.Length; i++)
        {
            S_Character.DialoguePart.Add(current.S_CharacterDialogues[i]);
        }
        if (current.FirstOnRight)
        {
            Current_Character = PersonDialogue.FirstPerson;
        }
        else
        {
            Current_Character = PersonDialogue.SecondPerson;
        }
        endScene = current.endScene;
        CharacterTurn(this.Current_Character);
        AllDialogueLogic();
    }

    void ResetIndex()
    {
        F_internalIndex = 0;
        F_Index = 0;
        S_internalIndex = 0;
        S_Index = 0;
    }

    public void CharacterTurn(PersonDialogue current)
    {
        this.Current_Character = current;
        switch (this.Current_Character)
        {
            case PersonDialogue.FirstPerson:
                F_Character.CharacterPanel.SetActive(true);
                S_Character.CharacterPanel.SetActive(false);
                textToUse = F_Character.DialogueText;
                break;
            case PersonDialogue.SecondPerson:
                S_Character.CharacterPanel.SetActive(true);
                F_Character.CharacterPanel.SetActive(false);
                textToUse = S_Character.DialogueText;
                break;
            default:
                Background.gameObject.SetActive(false);
                F_Character.CharacterPanel.SetActive(false);
                S_Character.CharacterPanel.SetActive(false);
                ResetIndex();

                break;
        }
    }
    IEnumerator WriteInTimeline(string dialogue)
    {
        startCoroutine = true;
        for (int i = 0; i < dialogue.Length; i++)
        {
            char c = (char)dialogue[i];
            textToUse.text += c;
            yield return null;
        }
        startCoroutine = false;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (Current_Character != PersonDialogue.None)
            {
                AllDialogueLogic();
            }
        }
    }

    public void EndDialogue()
    {

        if (endScene)
        {
            //Fade Out
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            CharacterTurn(PersonDialogue.None);
            if (activateWithness)
            {
                EventManager.EndDialogueWitness?.Invoke();
                activateWithness = false;
            }
            if(GameManager.Instance != null)
                GameManager.Instance.canClick = true;
        }
    }

    void AllDialogueLogic()
    {
        if (!startCoroutine)
        {

            if (F_Character.DialoguePart.Count > 0 && S_Character.DialoguePart.Count > 0)
            {
                if (F_Index > F_Character.DialoguePart.Count - 1 || S_Index > S_Character.DialoguePart.Count - 1)
                {
                    EndDialogue();
                    return;
                }

                if (F_internalIndex > F_Character.DialoguePart[F_Index].text.Length - 1)
                {
                    F_internalIndex = 0;
                    F_Index++;
                    CharacterTurn(PersonDialogue.SecondPerson);
                }
                if (S_internalIndex > S_Character.DialoguePart[S_Index].text.Length - 1)
                {
                    S_internalIndex = 0;
                    S_Index++;
                    CharacterTurn(PersonDialogue.FirstPerson);
                }
            }

            if(Current_Character == PersonDialogue.FirstPerson)
            {
                    Debug.Log("F_Chaar" + F_Character.DialoguePart.Count);
                if (F_Character.DialoguePart.Count > 0)
                {
                    F_Character.DialogueText.text = "";
                    StartCoroutine(WriteInTimeline(F_Character.DialoguePart[F_Index].text[F_internalIndex]));
                    F_internalIndex++;
                }
                else
                {
                    EndDialogue();
                    return;
                }
            }
            else if(Current_Character == PersonDialogue.SecondPerson)
            {
                    Debug.Log("F_Chaar" + S_Character.DialoguePart.Count);
                if (S_Character.DialoguePart.Count > 0)
                {
                    S_Character.DialogueText.text = "";
                    StartCoroutine(WriteInTimeline(S_Character.DialoguePart[S_Index].text[S_internalIndex]));
                    S_internalIndex++;
                }
                else
                {
                    EndDialogue();
                    return;
                }
            }
            
        }
    }

    private void OnEnable()
    {
        EventManager.StartDialogue += OpenDialogue;
        EventManager.isThisWitness += OnIsThisWitness;
    }
    private void OnDisable()
    {
        EventManager.StartDialogue -= OpenDialogue;
        EventManager.isThisWitness -= OnIsThisWitness;
    }
}
