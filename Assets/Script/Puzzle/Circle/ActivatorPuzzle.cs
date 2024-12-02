using UnityEngine;

public class ActivatorPuzzle : MonoBehaviour
{
    private void OnEnable()
    {
        EventManager.CircleActivator += OnActivation;
    }
    private void OnDisable()
    {
        EventManager.CircleActivator -= OnActivation;
    }

    private void OnActivation()
    {

        EventManager.CirclePuzzleStart?.Invoke();
        //gameObject.SetActive(false);
    }
}
