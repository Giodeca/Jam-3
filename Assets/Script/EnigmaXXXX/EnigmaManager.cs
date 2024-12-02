using UnityEngine;

public class EnigmaManager : MonoBehaviour
{
    [SerializeField] private GameObject[] activator;
    [SerializeField] private GameObject[] deactivate;


    public void Activation()
    {
        foreach (GameObject obj in deactivate)
        {
            obj.SetActive(false);
        }
        foreach (GameObject obj in activator)
        {
            obj.SetActive(true);
        }
    }
    public void DeaActivation()
    {
        foreach (GameObject obj in deactivate)
        {
            obj.SetActive(true);
        }
        foreach (GameObject obj in activator)
        {
            obj.SetActive(false);
        }
    }
}
