using UnityEngine;

public class MaskCamera : MonoBehaviour
{
    [SerializeField] private Camera ZoomCamera;
    //private RenderTexture renderTexture;


    private void OnEnable()
    {
        EventManager.changingRoom += OnChanginRoom;
        EventManager.DeactivateMask += OnDeactivateMask;
    }
    private void OnDisable()
    {
        EventManager.changingRoom -= OnChanginRoom;
        EventManager.DeactivateMask -= OnDeactivateMask;
    }
    void Start()
    {
        // Crea un RenderTexture con le dimensioni dello schermo
        //renderTexture = new RenderTexture(Screen.width, Screen.height, 16);
        //ZoomCamera.targetTexture = renderTexture;

        // Assegna il RenderTexture al materiale della maschera
        //GetComponent<Renderer>().material.mainTexture = renderTexture;
    }
    public void ChangeZoomCamera(Transform transform)
    {
        ZoomCamera.gameObject.transform.position = transform.position;
        ZoomCamera.gameObject.transform.rotation = transform.rotation;
    }
    private void OnChanginRoom()
    {
        gameObject.SetActive(false);
    }
    private void OnDeactivateMask()
    {
        gameObject.SetActive(false);
    }
    void Update()
    {
        // Aggiorna la posizione e l'orientamento della seconda telecamera se necessario
    }
}
