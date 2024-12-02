using UnityEngine;

public class ItemMove : MonoBehaviour
{

    Vector3 mPrevPos = Vector3.zero;
    Vector3 mPosDelta = Vector3.zero;
    private bool isDragging = false;
    //[SerializeField] private Camera mCamera;

    public float rotationSpeed = 0.1f;
    private void OnEnable()
    {
        EventManager.ReactivateThings += OnReactivate;
    }

    private void OnDisable()
    {
        EventManager.ReactivateThings -= OnReactivate;
    }

    void Update()
    {
        MoveAround();
    }

    private void MoveAround()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Check if the mouse is over the object
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                    mPrevPos = Input.mousePosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            mPosDelta = Input.mousePosition - mPrevPos;

            mPosDelta *= rotationSpeed;

            if (Vector3.Dot(transform.up, Vector3.up) >= 0)
            {
                transform.Rotate(transform.up, Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }
            else
            {
                transform.Rotate(transform.up, -Vector3.Dot(mPosDelta, Camera.main.transform.right), Space.World);
            }

            transform.Rotate(Camera.main.transform.right, Vector3.Dot(mPosDelta, Camera.main.transform.up), Space.World);

            mPrevPos = Input.mousePosition;
        }
    }

    private void OnReactivate()
    {
        Destroy(this.gameObject);
    }

}
