using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject postionIstance;
    [SerializeField] private Camera cameras;
    private Quaternion cameraPos;
    [SerializeField] private Quaternion startCameraPos;
    private bool canRotate;
    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;

    private void Start()
    {
        startCameraPos = cameraPos;
    }


    private void OnEnable()
    {
        EventManager.ActivateObj += ActivateThings;
        EventManager.ObjAssignation += OnIstantiateObj;
    }

    private void OnDisable()
    {
        EventManager.ActivateObj -= ActivateThings;
        EventManager.ObjAssignation -= OnIstantiateObj;
    }
    private void Update()
    {

    }
    private void ActivateThings()
    {
        cameras.enabled = true;
        canRotate = true;
    }

    private void OnIstantiateObj(GameObject obj)
    {

        Instantiate(obj, postionIstance.transform.position, Quaternion.identity);
        //obj.GetComponent<BoxCollider>().enabled = true;
        //obj.GetComponent<ItemMove>().enabled = true;

    }


}
