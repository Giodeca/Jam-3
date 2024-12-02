using UnityEngine;

public class CircleManager : MonoBehaviour
{
    [SerializeField] public float[] CircleSolutionRot;
    private int solvedCircle;
    public DialogueTrigger triggerDialogue;
    [SerializeField] private int IndexRoom;
    [SerializeField] Animator animator;
    [SerializeField] GameObject finalBook;

    private void Awake()
    {
        //triggerDialogue = GetComponent<DialogueTrigger>();
    }




    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            triggerDialogue.enabled = true;
            EventManager.CirclePuzzleFinish?.Invoke();
            EventManager.MoveToNextRoom?.Invoke(IndexRoom);
            gameObject.SetActive(false);
            EventManager.ChangeLayer?.Invoke();
        }
    }
    public void CheckVictory()
    {
        if (SpawnManager.Instance.isLv2 == false)
        {
            if (solvedCircle >= 3 /*CircleSolutionRot.Length*/)
            {

                EventManager.ChangeLayer?.Invoke();
                EventManager.CirclePuzzleFinish?.Invoke();
                EventManager.MoveToNextRoom?.Invoke(7);
                triggerDialogue.enabled = true;
                //Destroy(gameObject);
                gameObject.layer = LayerMask.NameToLayer("Default");
            }
            else
                Debug.Log("DioCane");
        }
        else
        {
            if (solvedCircle >= 3 /*CircleSolutionRot.Length*/)
            {
                animator.enabled = true;
                gameObject.layer = LayerMask.NameToLayer("Default");
                finalBook.SetActive(true);
            }
        }

    }
    public void SolveCircle()
    {
        solvedCircle++;
        CheckVictory();

    }
}
