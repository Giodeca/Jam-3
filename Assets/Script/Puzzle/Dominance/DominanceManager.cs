using System.Collections.Generic;

using UnityEngine;


public class DominanceManager : MonoBehaviour
{
    public static DominanceManager instance;
    SelectConnection select;

    public List<FirstConnection> connectionType = new List<FirstConnection>();
    public List<Connection> listOfConnection = new List<Connection>();

    private int puzzleSolved;
    private void Awake()
    {
        instance = this;
    }
    private void OnEnable()
    {
        EventManager.RestartDominance += ResetDominance;
    }
    private void OnDisable()
    {
        EventManager.RestartDominance -= ResetDominance;
    }
    public void NewConnection(Vector4 color, FirstConnection firstConnection)
    {
        select = new SelectConnection(color, firstConnection);
        Debug.Log("new connection");
    }
    public SelectConnection Connection()
    {
        if (select != null)
        {
            return select;
        }
        else
        {
            Debug.Log("Connection not found");
            return null;
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            EventManager.DominanceEnd?.Invoke();
            EventManager.SolveDomination?.Invoke();
            SpawnManager.Instance.StopRoomChage = false;
            Debug.Log("Victory");
            SpawnManager.Instance.abilityRunning = false;
        }
    }
    public void AddConnection(Connection connection)
    {
        listOfConnection.Add(connection);
    }
    public void ClearList()
    {
        listOfConnection.Clear();
    }
    public void ResetDominance()
    {
        ClearList();
        select = null;
    }
    public void SolvingPuzzle()
    {
        puzzleSolved++;
        for (int i = 0; i < listOfConnection.Count; i++)
        {
            listOfConnection[i].enabled = false;
        }
        ClearList();
        select = null;
        if (DominanceSolved())
        {
            SpawnManager.Instance.StopRoomChage = false;
            EventManager.DominanceEnd?.Invoke();
            EventManager.SolveDomination?.Invoke();
            SpawnManager.Instance.abilityRunning = false;
            Debug.Log("Victory");
        }
    }

    public bool DominanceSolved()
    {
        if (puzzleSolved == connectionType.Capacity)
        {
            return true;
        }
        return false;
    }
}
