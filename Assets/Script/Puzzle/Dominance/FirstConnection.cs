using UnityEngine;
using UnityEngine.VFX;

public class FirstConnection : Connection
{
    [SerializeField] public Connection LastConnection;
    

    private void Awake()
    {
        VFX = GetComponentInChildren<VisualEffect>();
        VFXColor = VFX.GetVector4("Color");
        InstanceConnections();
        GetPreviousConnection();
    }
    private void OnMouseDown()
    {
        if (DominanceManager.instance.Connection() == null)
        {
            DominanceManager.instance.NewConnection(VFXColor, this);
            DominanceManager.instance.AddConnection(this);
        }
        else
        {
            Debug.Log("Reset");
            EventManager.RestartDominance?.Invoke();
        }


    }
}
