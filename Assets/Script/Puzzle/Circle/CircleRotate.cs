using UnityEngine;

public class CircleRotate : MonoBehaviour
{

    public CircleManager circleManager;
    [SerializeField] private Camera refCamera;
    [SerializeField] private int circleIndex;
    private int round = 3;
    public bool isSolved;

    //Vector3 previousPosition;
    //Vector3 cameraDistance;
    private void OnEnable()
    {
        //EventManager.CheckRotationEnigma += OnCheckRotation;
    }
    private void OnDisable()
    {
        //EventManager.CheckRotationEnigma -= OnCheckRotation;
    }

    private void Awake()
    {
        //circleManager = transform.parent.GetComponent<CircleManager>();
    }
    public void OnCheckRotation(GameObject obj, float rotation)
    {
        if (!isSolved && obj == gameObject)
        {
            float targetRotation = circleManager.CircleSolutionRot[circleIndex];


            if (Mathf.Abs(rotation - targetRotation) <= 8)
            {
                isSolved = true;
                Debug.Log(circleManager);
                circleManager.SolveCircle();
            }
        }
    }
    //private void OnMouseDown()
    //{
    //    previousPosition = refCamera.ScreenToViewPostPoint(Input.mousePosition)
    //    camDistance = transform.position - refCamera.transform.position;
    //}

    //private void OnMouseDrag()
    //{

    //    Vector2 mousePosition = refCamera.WorldToScreenPoint(Input.mousePosition);

    //    Vector2 direction = (mousePosition - (Vector2)transform.position).normalized;

    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(new Vector3(angle, 0, 0));


    //Vector3 direction = previousPosition - refCamera.ScreenToViewportPoint(Input.mousePosition);
    //refCamera.transform.position = Vector3.zero;
    //refCamera.transform.Rotate(Vector3.right, direction.x * 180f);


    //}

    //private void OnMouseUp()
    //{
    //    transform.rotation = Quaternion.Euler(Mathf.RoundToInt(transform.rotation.eulerAngles.x), transform.rotation.eulerAngles.y, 0);

    //    if ((transform.rotation.eulerAngles.x < circleManager.CircleSolutionRot[circleIndex] + round && transform.rotation.eulerAngles.x > circleManager.CircleSolutionRot[circleIndex] - round) || (transform.rotation.eulerAngles.x < circleManager.CircleSolutionRot[circleIndex] - 360 + round && transform.rotation.eulerAngles.x > circleManager.CircleSolutionRot[circleIndex] + 360 - round))
    //    {
    //        circleManager.SolveCircle();
    //        transform.rotation = Quaternion.Euler(circleManager.CircleSolutionRot[circleIndex], transform.rotation.eulerAngles.y, 0);
    //        GetComponent<Collider>().enabled = false;
    //        GetComponent<MeshRenderer>().material.color = Color.grey;
    //    }

    //}


}
