using UnityEngine;

public class ConnectionManager : MonoBehaviour
{
    [SerializeField] private GameObject[] connection;

    private void OnEnable()
    {
        EventManager.ActivateObj += ActivationConnection;
        EventManager.DeactivateObj += DeactivationConnection;
    }

    private void OnDisable()
    {
        EventManager.ActivateObj -= ActivationConnection;
        EventManager.DeactivateObj -= DeactivationConnection;
    }


    private void ActivationConnection()
    {
        foreach (var connect in connection)
        {
            connect.SetActive(true);
        }
    }

    private void DeactivationConnection()
    {
        foreach (var connect in connection)
        {
            connect.SetActive(false);
        }
    }
}
