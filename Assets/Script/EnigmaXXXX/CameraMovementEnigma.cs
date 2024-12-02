using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraMovementEnigma : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 originObj;
    private Vector3 difference;
    private Vector3 differenceObj;
    public bool isDragging;
    public bool isDraggingObj;
    public GameObject coseRotanti;
    public GameObject coseRotantiY;
    [SerializeField] private LayerMask gameFinish;
    [SerializeField] private LayerMask thingsRotable;
    [SerializeField] private LayerMask thingsRotableY;
    [SerializeField] private float ZoomMultyplier;
    [SerializeField] private float ZoomFovMin;
    [SerializeField] private float ZoomFowMax;
    public GameObject CameraPivot;
    [SerializeField] private float dragMulty;

    private bool blockMovement;
    public Camera _mainCamera;



    private void Start()
    {

    }

    private void Update()
    {


        //Zoom();D
        //HeldDrag();
        if (GameManager.Instance.canClick == true)
        {
            GetTargetToRotate();

            if (coseRotanti != null)
                HoldDragObj();

            if (coseRotantiY != null)
                HoldDragObjY();
        }

    }


    private void GetTargetToRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, gameFinish))
            {
                SceneManager.LoadScene("SoluzioneCheap2");
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, thingsRotable))
            {
                coseRotanti = hit.collider.gameObject;
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, thingsRotableY))
            {
                coseRotantiY = hit.collider.gameObject;

            }
        }
    }
    //private void Zoom()
    //{
    //    float zoomValue = Input.GetAxisRaw("Mouse ScrollWheel");
    //    float zooming = zoomValue * ZoomMultyplier;
    //    _mainCamera.fieldOfView = Mathf.Clamp(_mainCamera.fieldOfView + zooming, ZoomFovMin, ZoomFowMax);
    //}

    private void HoldDragObj()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            originObj = GetMousePositionOverObjectY();
            isDraggingObj = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDraggingObj = false;
            float absoluteRotationZ = Mathf.Abs(coseRotanti.transform.rotation.eulerAngles.z);
            coseRotanti.GetComponent<CircleRotate>().OnCheckRotation(coseRotanti, absoluteRotationZ);
            coseRotanti = null;
        }
        if (isDraggingObj)
        {
            differenceObj = (GetMousePositionOverObjectY() - originObj) * dragMulty;
            Quaternion rotation = Quaternion.Euler(0, 0, -1 * differenceObj.y);
            coseRotanti.transform.rotation *= rotation;
        }
    }
    private void HoldDragObjY()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            originObj = GetMousePositionOverObjectY();
            isDraggingObj = true;
        }
        else if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDraggingObj = false;
            float absoluteRotationX = Mathf.Abs(coseRotantiY.transform.rotation.eulerAngles.x);
            coseRotantiY.GetComponent<CircleRotate>().OnCheckRotation(coseRotantiY, absoluteRotationX);
            //EventManager.CheckRotationEnigma?.Invoke(coseRotantiY, absoluteRotationX);
            coseRotantiY = null;
        }
        if (isDraggingObj)
        {
            differenceObj = (GetMousePositionOverObjectY() - originObj) * dragMulty;
            Quaternion rotation = Quaternion.Euler(1 * differenceObj.y, 0, 0);
            coseRotantiY.transform.rotation *= rotation;
        }
    }
    //private void HeldDrag()
    //{
    //    if (Mouse.current.rightButton.wasPressedThisFrame)
    //    {
    //        origin = GetMousePosition();
    //        isDragging = true;
    //    }
    //    else if (Mouse.current.rightButton.wasReleasedThisFrame)
    //    {
    //        isDragging = false;
    //    }


    //    if (isDragging)
    //    {
    //        difference = (GetMousePosition() - origin) * dragMulty;
    //        CameraPivot.transform.Rotate(new Vector3(0, 1 * difference.x, 0), Space.World);
    //    }
    //}
    private Vector3 GetMousePosition()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 screenPosition = new Vector3(mousePos.x, 0, 0);
        return screenPosition;
    }

    private Vector3 GetMousePositionOverObject()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 screenPosition = new Vector3(mousePos.x, 0, 0);
        return screenPosition;
    }

    private Vector3 GetMousePositionOverObjectY()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 screenPosition = new Vector3(0, mousePos.y, 0);
        return screenPosition;
    }

}
