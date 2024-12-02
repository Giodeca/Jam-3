
using UnityEngine;

public class EndDomination : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.SolveDomination += EnableTrigger;
    }
    private void OnDisable()
    {
        EventManager.SolveDomination -= EnableTrigger;
    }

    void EnableTrigger()
    {
        GetComponent<DialogueTrigger>().enabled = true;
    }
}
