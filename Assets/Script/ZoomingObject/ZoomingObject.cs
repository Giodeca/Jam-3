using System;
using UnityEngine;

public class ZoomingObject : MonoBehaviour, IInteractable
{
    [SerializeField] private Transform CameraTransform;
    [SerializeField] private MaskCamera Mask;
    [SerializeField] public int uniqueID;
    private Animator animator;
    [NonSerialized] public ItemCollectable item;

    [SerializeField] private PopUp TextPopUp;
    [SerializeField][TextArea] private string[] text;
    public bool IsLastPuzzle;


    private void Awake()
    {
        TryGetComponent(out item);
        TryGetComponent(out animator);
    }
    private void OnEnable()
    {
        EventManager.Interactable += OnInteract;
    }

    private void OnDisable()
    {
        EventManager.Interactable -= OnInteract;
    }

    public void OnInteract(int ID)
    {
        if (ID == this.uniqueID)
        {
            if (IsLastPuzzle)
            {
                GameManager.Instance.TasteForBloodCount++;
                Mask.gameObject.SetActive(true);
                Mask.ChangeZoomCamera(CameraTransform);
                TextPopUp.gameObject.SetActive(true);
                TextPopUp.AssignText(text);
                if (item != null)
                {
                    TextPopUp.AssignItem(item);
                }
                if (animator != null)
                {
                    animator.SetTrigger("Interctable");
                }
            }
            else
            {
                Mask.gameObject.SetActive(true);
                Mask.ChangeZoomCamera(CameraTransform);
                TextPopUp.gameObject.SetActive(true);
                TextPopUp.AssignText(text);
                if (item != null)
                {
                    TextPopUp.AssignItem(item);
                }
                if (animator != null)
                {
                    animator.SetTrigger("Interctable");
                }
            }



        }
    }
}
