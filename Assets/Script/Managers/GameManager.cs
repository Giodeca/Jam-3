using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
    [Header("RoomRotation")]
    public GameObject[] roomRotable;
    public GameObject CameraPivot;
    public Camera mainCamera;
    private Vector3 origin;
    private Vector3 difference;
    private bool isDragging;
    [SerializeField] private float dragMulty;
    public int roomIndex;
    [SerializeField] private LayerMask roomChanger;
    [SerializeField] private LayerMask interactable;
    [SerializeField] private LayerMask dialogueTrigger;
    [SerializeField] private LayerMask collectable;
    [SerializeField] private LayerMask puzzleStart;
    [SerializeField] private LayerMask bodyTarget;
    [SerializeField] private LayerMask animTrigger;
    bool isMoving;
    bool isRotating;
    [SerializeField] public Inventory inventory;
    [SerializeField] private GameObject closeUp;
    [SerializeField] private GameObject closeMenu;
    [SerializeField] private GameObject[] VisionActivationObj;
    public bool isVisionActive;
    public int bodyChecks;
    public bool bodyChecked;
    public Camera one;
    public Camera second;
    public Button abiility;
    public bool canClick;
    public int ClickDominanceCount;
    public int TasteForBloodCount;
    public bool canClickFinalMirror;
    public bool BloodSorceryActive;
    public GameObject book;
    public bool addBooK;
    public bool blockInventory;
    public bool HasTheKey;




    protected override void Awake()
    {
        base.Awake();
        mainCamera = Camera.main;

        //if (SpawnManager.Instance.LastKnowPosition != null)
        //    CameraPivot.transform.position = SpawnManager.Instance.LastKnowPosition;

    }

    protected override void OnEnable()
    {
        base.Awake();
        EventManager.CirclePuzzleStart += OnCirlePuzzleStart;
        EventManager.CirclePuzzleFinish += OnCirlePuzzleFinish;
        EventManager.MoveToNextRoom += OnCompletedGame;
        EventManager.ActivateVision += OnActivationVision;
        EventManager.DeactivationVision += OnDeactivateVision;
        EventManager.EndDialogueWitness += OnEndDialogueEvent;
    }
    private void OnDisable()
    {
        EventManager.CirclePuzzleStart -= OnCirlePuzzleStart;
        EventManager.CirclePuzzleFinish -= OnCirlePuzzleFinish;
        EventManager.MoveToNextRoom -= OnCompletedGame;
        EventManager.ActivateVision -= OnActivationVision;
        EventManager.DeactivationVision -= OnDeactivateVision;
        EventManager.EndDialogueWitness -= OnEndDialogueEvent;
    }

    private void OnEndDialogueEvent()
    {
        abiility.enabled = true;
        if (ClickDominanceCount == 0)
        {
            abiility.GetComponent<ButtonColorChanger>().enabled = true;
            ClickDominanceCount++;
        }

    }
    private void Start()
    {
        inventory.ItemUsed += Inventory_ItemUsed;
        if (SpawnManager.Instance.isLv2 == true)
            roomIndex = 1;

    }

    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        Debug.Log("Dd");
    }


    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.M))
        {
            SpawnManager.Instance.StopRoomChage = false;
            Debug.Log("Victory");
        }
        if (!addBooK)
        {
            if (book != null)
            {
                IInventoryItem bookItem = book.GetComponent<IInventoryItem>();
                inventory.AddItem(bookItem);
                addBooK = true;
            }

        }
        if (canClick)
        {
            if (!canClickFinalMirror)
            {
                if (TasteForBloodCount == 3)
                {
                    canClickFinalMirror = true;
                    TasteForBloodCount = 0;
                }
            }
            HeldDrag();
            OnBodyCheckCompleted();
            if (Input.GetKeyDown(KeyCode.K))
            {
                roomIndex++;
                CameraPivot.transform.position = roomRotable[roomIndex].transform.position;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (!isRotating)
                {
                    StartCoroutine(RotatePivot(CameraPivot.transform.rotation, 90));
                }

            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!isRotating)
                {
                    StartCoroutine(RotatePivot(CameraPivot.transform.rotation, -90));
                }
            }
            GetNewPosition();
        }

    }

    private void OnBodyCheckCompleted()
    {
        if (bodyChecks == 3 && !bodyChecked)
        {
            bodyChecked = true;
        }
    }
    private void OnDeactivation()
    {
        this.gameObject.SetActive(false);
    }
    private IEnumerator RotatePivot(Quaternion startRot, float jump)
    {
        isRotating = true;
        Quaternion endRot = Quaternion.Euler(startRot.eulerAngles.x, CalculateYPivot() + jump, 0);

        float elapseTime = 0;
        float maxTimer = 2;
        while (elapseTime < maxTimer)
        {
            CameraPivot.transform.rotation = Quaternion.Slerp(startRot, endRot, elapseTime / maxTimer);

            elapseTime += Time.deltaTime;
            yield return null;
        }
        CameraPivot.transform.rotation = endRot;
        isRotating = false;
    }




    private void GetNewPosition()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);


            if (Physics.Raycast(ray, out hit, Mathf.Infinity, roomChanger))
            {
                if (SpawnManager.Instance.StopRoomChage == false)
                {
                    if (hit.collider != null && !isMoving)
                    {
                        roomIndex = hit.collider.gameObject.GetComponent<RoomConnection>().roomIndexConnection;
                        EventManager.changingRoom?.Invoke();
                        StartCoroutine(MoveCamera(CameraPivot.transform, roomRotable[roomIndex].transform));
                    }
                }
                else
                {
                    if (SpawnManager.Instance.isFeedBackTextRunning == false)
                    {
                        SpawnManager.Instance.isFeedBackTextRunning = true;
                        StartCoroutine(SpawnManager.Instance.FeedBackTextCoroutine());
                    }

                }
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, animTrigger))
            {
                if (hit.collider != null)
                {
                    hit.collider.gameObject.GetComponent<Animator>().enabled = true;
                    EventManager.Interactable?.Invoke(hit.collider.gameObject.GetComponent<ZoomingObject>().uniqueID);
                    hit.collider.GetComponent<HasKeyReturn>().CollectItem();
                }
            }

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, interactable))
            {
                if (hit.collider != null)
                {
                    EventManager.Interactable?.Invoke(hit.collider.gameObject.GetComponent<ZoomingObject>().uniqueID);
                }
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, dialogueTrigger))
            {
                if (hit.collider != null)
                {
                    hit.collider.GetComponent<DialogueTrigger>().enabled = true;
                }
            }
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity, collectable))
            //{
            //    IInventoryItem item = hit.collider.GetComponent<IInventoryItem>();
            //    if (item != null)
            //    {
            //        inventory.AddItem(item);
            //    }
            //}

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, puzzleStart))
            {
                if (hit.collider != null)
                {
                    Debug.Log(hit.collider.gameObject.name);
                    EventManager.CircleActivator?.Invoke();
                }
            }
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, bodyTarget))
            {
                if (hit.collider != null)
                {
                    BodyAcrivation newBody = hit.collider.gameObject.GetComponent<BodyAcrivation>();
                    newBody.InfosSpawn();
                    EventManager.Interactable?.Invoke(hit.collider.gameObject.GetComponent<ZoomingObject>().uniqueID);
                }
            }

        }
    }

    public IEnumerator MoveCamera(Transform startPos, Transform endPos)
    {
        isMoving = true;
        float duration = .75f;
        float elapseTime = 0;

        while (elapseTime < duration)
        {

            CameraPivot.transform.position = Vector3.Lerp(startPos.position, endPos.position, elapseTime / duration);
            elapseTime += Time.deltaTime;
            yield return null;
        }
        CameraPivot.transform.position = endPos.position;

        SpawnManager.Instance.LastKnowPosition = endPos.position;
        isMoving = false;
    }

    private void OnCompletedGame(int index)
    {
        StartCoroutine(MoveCamera(CameraPivot.transform, roomRotable[index].transform));
    }

    private void OnActivationVision()
    {
        foreach (GameObject obj in VisionActivationObj)
        {
            if (obj != null)
                obj.SetActive(true);
        }

    }
    private void OnDeactivateVision()
    {
        foreach (GameObject obj in VisionActivationObj)
        {
            if (obj != null)
                obj.SetActive(false);
        }

    }
    private void OnCirlePuzzleStart()
    {
        mainCamera = second;
        blockInventory = true;
        closeUp.SetActive(true);
        CameraPivot.SetActive(false);

        closeMenu.SetActive(true);

    }
    private void OnCirlePuzzleFinish()
    {
        mainCamera = one;
        blockInventory = false;
        CameraPivot.SetActive(true);
        closeUp.SetActive(false);
        closeMenu.SetActive(false);

    }
    private void HeldDrag()
    {
        if (Mouse.current.rightButton.wasPressedThisFrame)
        {
            origin = GetMousePosition();
            isDragging = true;
            EventManager.DeactivateObj?.Invoke();
        }
        else if (Mouse.current.rightButton.wasReleasedThisFrame)
        {
            isDragging = false;
            EventManager.ActivateObj?.Invoke();
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

    float CalculateYPivot()
    {
        float currentY = CameraPivot.transform.rotation.eulerAngles.y;

        if (currentY < 90 && currentY > 0)
        {
            return 45;
        }
        if (currentY > 90 && currentY < 180)
        {
            return 135;
        }
        if (currentY > 180 && currentY < 270)
        {
            return 225;
        }
        if (currentY > 270 && currentY < 360)
        {
            return 315;
        }
        return 0;
    }

}
