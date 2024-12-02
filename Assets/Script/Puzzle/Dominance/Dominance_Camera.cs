
using UnityEngine;

public class Dominance_Camera : MonoBehaviour
{
    [SerializeField] float rotationSpeed = 5f;
    [SerializeField] float scrollSpeed = 10f;
    Camera zoomCamera;

    private void Awake()
    {
        zoomCamera = GetComponentInChildren<Camera>();
    }
    private void Update()
    {

        if (Input.GetKey(KeyCode.Mouse1))
        {
            CameraOrbit();
        }
        CameraZoom();
    }
    void CameraOrbit()
    {
        if (Input.GetAxis("Mouse Y") != 0 || Input.GetAxis("Mouse X") != 0)
        {
            float verticalInput = Input.GetAxis("Mouse Y") + rotationSpeed * Time.deltaTime;
            float horizontalInput = Input.GetAxis("Mouse X") + rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.right, verticalInput);
            transform.Rotate(Vector3.up, horizontalInput, Space.World);
        }
    }

    void CameraZoom()
    {
        if (zoomCamera.fieldOfView > 1 && zoomCamera.fieldOfView < 100)
        {
            zoomCamera.fieldOfView -= Input.GetAxis("Mouse ScrollWheel") * scrollSpeed;
        }

    }
}
