using UnityEngine;
using UnityEngine.InputSystem;

public class CircleGameRotate : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 originObj;
    private Vector3 difference;
    private Vector3 differenceObj;
    private bool isDragging;
    private bool isDraggingObj;
    public GameObject coseRotanti;
    private GameObject coseRotantiY;
    [SerializeField] private LayerMask thingsRotable;
    [SerializeField] private LayerMask thingsRotableY;
    [SerializeField] private float ZoomMultyplier;
    [SerializeField] private float ZoomFovMin;
    [SerializeField] private float ZoomFowMax;
    public GameObject CameraPivot;
    [SerializeField] private float dragMulty;

    private bool blockMovement;
    public Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
        {
            Debug.LogError("Main Camera not found. Please ensure there is a Camera tagged as MainCamera in the scene.");
        }
    }

    private void Update()
    {
        //Zoom();
        HeldDrag();
        GetTargetToRotate();

        if (coseRotanti != null)
        {
            HoldDragObj();
            HandleKeyRotation(); // Aggiungi questo metodo per gestire le rotazioni tramite tasti
        }

        if (coseRotantiY != null)
        {
            HoldDragObjY();
        }
    }

    private void GetTargetToRotate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, thingsRotable))
            {
                coseRotanti = hit.collider.gameObject;
                Debug.Log(coseRotanti);
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, thingsRotableY))
            {
                coseRotantiY = hit.collider.gameObject;
                Debug.Log(coseRotantiY.name);
            }
        }
    }

    private void HoldDragObj()
    {
        //if (Mouse.current.leftButton.wasPressedThisFrame)
        //{
        //    originObj = GetMousePositionOverObject();
        //    isDraggingObj = true;
        //}
        //else if (Mouse.current.leftButton.wasReleasedThisFrame)
        //{
        //    isDraggingObj = false;
        //    coseRotanti = null;
        //}
        //if (isDraggingObj)
        //{
        //    differenceObj = (GetMousePositionOverObject() - originObj) * dragMulty;
        //    Quaternion rotation = Quaternion.Euler(0, 1 * differenceObj.x, 0);
        //    coseRotanti.transform.rotation *= rotation;
        //}
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
            coseRotantiY = null;
        }
        if (isDraggingObj)
        {
            differenceObj = (GetMousePositionOverObjectY() - originObj) * dragMulty;
            Quaternion rotation = Quaternion.Euler(1 * differenceObj.y, 0, 0);
            coseRotantiY.transform.rotation *= rotation;
        }
    }

    private void HeldDrag()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            origin = GetMousePosition();
            isDragging = true;
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            difference = (GetMousePosition() - origin) * dragMulty;
            CameraPivot.transform.Rotate(new Vector3(0, 1 * difference.x, 0), Space.World);
        }
    }

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

    private void HandleKeyRotation()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            coseRotanti.transform.Rotate(Vector3.right, 10 * Time.deltaTime);
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            coseRotanti.transform.Rotate(Vector3.left, 10 * Time.deltaTime);
        }
    }

}
