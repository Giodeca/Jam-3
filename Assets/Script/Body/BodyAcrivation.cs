using UnityEngine;
using UnityEngine.UI;

public class BodyAcrivation : MonoBehaviour
{
    public GameObject[] infos;
    public BoxCollider coll;
    public Button bloodActivation;
    public void InfosSpawn()
    {
        coll.enabled = false;
        foreach (GameObject obj in infos)
        {
            Debug.Log("Here");
            obj.GetComponent<ZoomingObject>().enabled = true;
        }
        bloodActivation.enabled = true;
        bloodActivation.GetComponent<ButtonColorChanger>().enabled = true;
    }


}
