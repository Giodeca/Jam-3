
using UnityEngine;

using UnityEngine.VFX;

public class ConnectionVFX : MonoBehaviour
{
    private VisualEffect VFX;

    [SerializeField] private GameObject[] pos;


    private void Awake()
    {
        VFX = GetComponentInChildren<VisualEffect>();
    }

    public bool CheckRightConnection(Vector3 startPos, Vector3 endPos)
    {
        if((VFX.GetVector3("Pos1") == startPos && VFX.GetVector3("Pos4") == endPos) || (VFX.GetVector3("Pos4") == startPos && VFX.GetVector3("Pos1") == endPos))
        {
            return true;
        }
        return false;
    }

    public void SetConnection(Vector4 color)
    {
        VFX.SetVector4("Color", color);
    }

    public void SetConnection(Vector3 start, Vector3 end, Vector4 color)
    {
        pos[0].transform.position = start;
        pos[1].transform.position = start + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        pos[2].transform.position = start + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), Random.Range(-1f, 1f));
        pos[3].transform.position = end;
        //VFX.SetVector3("Pos1", start);
        //VFX.SetVector3("Pos2", end);
        //VFX.SetVector3("Pos3", start + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)));
        //VFX.SetVector3("Pos4", start + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)));
        VFX.SetVector4("Color", color);
    }

}
