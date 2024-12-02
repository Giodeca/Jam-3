using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CameraMovement : MonoBehaviour
{
    private Vector3 origin;
    private Vector3 difference;
    private bool isDragging;
    private string nameScene;
    [SerializeField] private float ZoomMultyplier;
    [SerializeField] private Vector2 minBoundary;
    [SerializeField] private Vector2 maxBoundary;
    [SerializeField] private float dragSpeedMultiplier;
    [SerializeField] private LayerMask loaderScene;

    private bool blockMovement;
    private Camera _mainCamera;


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
        ClickOnTarget();
        if (!blockMovement)
        {
            Zoom();
            HandleDrag();

        }

    }
    private void Zoom()
    {
        float zoomValue = Input.GetAxisRaw("Mouse ScrollWheel");
        float zooming = zoomValue * ZoomMultyplier;
        _mainCamera.fieldOfView = Mathf.Clamp(_mainCamera.fieldOfView + zooming, 20f, 60f);
    }

    private void ClickOnTarget()
    {

        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, loaderScene))
            {
                nameScene = hit.collider.gameObject.GetComponent<MapSceneLoader>().sceneName;
                SceneManager.LoadScene(nameScene);
            }

        }
    }
    private void HandleDrag()
    {

        if (Mouse.current.rightButton.wasPressedThisFrame)
        {

            origin = GetMouseWorldPosition();
            isDragging = true;
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }

        if (isDragging)
        {
            difference = (GetMouseWorldPosition() - origin) * dragSpeedMultiplier;
            Vector3 newPosition = _mainCamera.transform.position - difference;
            newPosition = ClampPosition(newPosition);
            _mainCamera.transform.position = newPosition;
        }
    }

    private Vector3 ClampPosition(Vector3 position)
    {
        position.x = Mathf.Clamp(position.x, minBoundary.x, maxBoundary.x);
        position.y = Mathf.Clamp(position.y, minBoundary.y, maxBoundary.y);
        return position;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();
        Vector3 screenPosition = new Vector3(mousePosition.x, mousePosition.y, _mainCamera.nearClipPlane);
        return _mainCamera.ScreenToWorldPoint(screenPosition);
    }

    private void LateUpdate()
    {
        if (!blockMovement)
        {
            if (isDragging)
                origin = GetMouseWorldPosition();
        }
    }
    private void OnDrawGizmos()
    {
        if (_mainCamera == null)
        {
            _mainCamera = Camera.main;
            if (_mainCamera == null) return;
        }

        Gizmos.color = Color.red;
        Vector3 bottomLeft = new Vector3(minBoundary.x, minBoundary.y, _mainCamera.transform.position.z);
        Vector3 topRight = new Vector3(maxBoundary.x, maxBoundary.y, _mainCamera.transform.position.z);
        Vector3 bottomRight = new Vector3(maxBoundary.x, minBoundary.y, _mainCamera.transform.position.z);
        Vector3 topLeft = new Vector3(minBoundary.x, maxBoundary.y, _mainCamera.transform.position.z);

        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}

