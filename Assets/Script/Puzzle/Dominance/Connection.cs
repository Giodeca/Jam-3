
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Connection : MonoBehaviour
{
    bool isSelected;

    protected Vector4 VFXColor;

    [SerializeField] private GameObject ConnectionPrefab;
    private List<ConnectionVFX> VFXConnections = new List<ConnectionVFX>();

    [SerializeField] public List<Connection> nextConnection;

    [SerializeField] protected VisualEffect VFX;
    

    [SerializeField] bool isLastConnection;

    private void Awake()
    {
        VFX = GetComponentInChildren<VisualEffect>();
        InstanceConnections();
        
        
        VFXColor = VFX.GetVector4("Color");
        GetPreviousConnection();

    }
    private void OnEnable()
    {
        EventManager.RestartDominance += ResetDominance;
    }
    private void OnDisable()
    {
        EventManager.RestartDominance -= ResetDominance;
    }
    private void OnMouseDown()
    {
        ChangeColor();
    }

    private void Update()
    {
        DrawLine();

    }

    protected void InstanceConnections()
    {
        for (int i = 0; i < nextConnection.Count; i++)
        {
            GameObject VFXConnection = Instantiate(ConnectionPrefab, Vector3.zero, Quaternion.identity);
            VFXConnections.Add(VFXConnection.GetComponent<ConnectionVFX>());
            VFXConnections[VFXConnections.Count - 1].SetConnection(transform.position, nextConnection[i].transform.position, Color.white);
        }
    }



    //private IEnumerator LiquidGoingUp(float startValue, float endValue, float duration, Material color)
    //{
    //    float elapseTime = 0;
    //    while (elapseTime < duration)
    //    {
    //        elapseTime += Time.deltaTime;
    //        liquidAmount = Mathf.Lerp(startValue, endValue, elapseTime / duration);
    //        SetLiquidAmount(liquidAmount, color);
    //        yield return null;
    //    }

    //    SetLiquidAmount(endValue, color);

    //}

    //public void SetLiquidAmount(float amount, Material color)
    //{
    //    color.SetFloat("_LiquidAmount", amount);
    //}

    protected void GetPreviousConnection()
    {
        for (int i = 0; i < nextConnection.Count; i++)
        {
            if (!nextConnection[i].nextConnection.Contains(this))
            {
                nextConnection[i].nextConnection.Add(this);
            }
        }
    }
    bool CheckIfTheresConnection()
    {
        for (int i = 0; i < nextConnection.Count; i++)
        {
            if (nextConnection[i] != null)
            {
                if (DominanceManager.instance.listOfConnection.Count > 0)
                {
                    if (DominanceManager.instance.listOfConnection[DominanceManager.instance.listOfConnection.Count - 1] == nextConnection[i])
                    {
                        return true;
                    }
                }
            }            
        }
        return false;
    }

    bool CheckRightLastConnection()
    {
        if (this == DominanceManager.instance.Connection().firstConnection.LastConnection)
        {
            Debug.Log("Here");
            return true; 
        }
        return false;
    }

    ConnectionVFX RightVFXConnection(Vector3 start, Vector3 end)
    {
        Connection previous = DominanceManager.instance.listOfConnection[DominanceManager.instance.listOfConnection.Count - 1];
        for (int i = 0; i < previous.VFXConnections.Count; i++)
        {
            if (previous.VFXConnections[i].CheckRightConnection(start, end))
            {
                return previous.VFXConnections[i];
            }
        }
        return null;
    }

    void ChangeColor()
    {
        if (!isSelected && CheckIfTheresConnection())
        {
            Debug.Log("Change color");

            VFX.SetVector4("Color", DominanceManager.instance.Connection().color);
            VFX.SetVector4("ColorInside", DominanceManager.instance.Connection().color);

            RightVFXConnection(DominanceManager.instance.listOfConnection[DominanceManager.instance.listOfConnection.Count - 1].transform.position, transform.position).SetConnection(DominanceManager.instance.Connection().color);

            DominanceManager.instance.AddConnection(this);
            isSelected = true;

            if (isLastConnection)
            {
                if (CheckRightLastConnection())
                {
                    Debug.Log("Connection Right");
                    DominanceManager.instance.SolvingPuzzle();
                }
                else
                {
                    EventManager.RestartDominance?.Invoke();
                }

            }
        }
        else
            EventManager.RestartDominance?.Invoke();

    }
    private void ResetDominance()
    {
        VFX.SetVector4("Color", VFXColor);
        VFX.SetVector4("ColorInside", VFXColor);

        for(int i = 0; i < VFXConnections.Count; i++)
        {
            VFXConnections[i].SetConnection(Color.white);
        }
        isSelected = false;
    }
    void DrawLine()
    {
        for (int i = 0; i < nextConnection.Count; i++)
        {
            Debug.DrawLine(this.transform.position, nextConnection[i].transform.position);
        }
    }
}
