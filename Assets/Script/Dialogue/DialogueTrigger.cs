using UnityEngine;

public enum EventType
{
    Start,
    OnClickObject
}
public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private EventType EventType;
    [SerializeField] SDialogue Dialogue;
    [SerializeField] private bool destory;
    [SerializeField] private bool isWitness;

    private void Start()
    {
        if (EventType == EventType.Start && this.enabled)
        {
            Debug.Log("HereIaM");
            if (isWitness)
            {

                EventManager.isThisWitness?.Invoke();
                EventManager.StartDialogue?.Invoke(Dialogue);
                SpawnManager.Instance.canActivateDominace = true;
                SpawnManager.Instance.StopRoomChage = true;

            }
            else
                EventManager.StartDialogue?.Invoke(Dialogue);

            if (destory)
            {
                GameManager.Instance.bodyChecks++;
            }
        }
    }

    private void OnMouseDown()
    {
        Debug.Log("HereIaMFuori");
        if (EventType == EventType.OnClickObject && this.enabled)
        {
            Debug.Log("HereIaM");
            if (isWitness)
            {

                EventManager.isThisWitness?.Invoke();
                EventManager.StartDialogue?.Invoke(Dialogue);
                SpawnManager.Instance.canActivateDominace = true;
                SpawnManager.Instance.StopRoomChage = true;

            }
            else
                EventManager.StartDialogue?.Invoke(Dialogue);

            if (destory)
            {
                GameManager.Instance.bodyChecks++;
            }
        }
    }

    public void SetDialogueOff(SDialogue dialogue)
    {
        if (dialogue == this.Dialogue)
        {
            this.enabled = false;
        }
    }

    private void OnEnable()
    {
        EventManager.StartDialogue += SetDialogueOff;
    }

    private void OnDisable()
    {
        EventManager.StartDialogue -= SetDialogueOff;
    }


}
