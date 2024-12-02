using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Hud : MonoBehaviour
{
    public Inventory Inventory;
    public GameObject closeMiniGame;
    public GameObject closeInventoryButton;
    public Button SorceryActivation;
    public bool firstActivationBlood;
    public GameObject textFeedback;

    private void OnEnable()
    {
        Inventory.ItemAdded += InventoryScript_ItemAdded;
        Inventory.ItemRemoved += InventoryScript_ItemRemoved;
    }
    private void OnDisable()
    {
        Inventory.ItemAdded -= InventoryScript_ItemAdded;
        Inventory.ItemRemoved -= InventoryScript_ItemRemoved;
    }
    void Start()
    {

    }

    private void InventoryScript_ItemAdded(object sender, InventoryEventArgs e)
    {
        Transform invetoryPanel = transform.Find("InventoryPanel");


        foreach (Transform slot in invetoryPanel)
        {
            //Se ho poi child faccio GETchild(0).GetChild(0)
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (!image.enabled)
            {
                image.enabled = true;
                image.sprite = e.Item.Image;

                itemDragHandler.Item = e.Item;

                break;
            }
        }
    }

    private void InventoryScript_ItemRemoved(object sender, InventoryEventArgs e)
    {
        Transform invetoryPanel = transform.Find("InventoryPanel");
        foreach (Transform slot in invetoryPanel)
        {
            Transform imageTransform = slot.GetChild(0).GetChild(0);
            Image image = imageTransform.GetComponent<Image>();
            ItemDragHandler itemDragHandler = imageTransform.GetComponent<ItemDragHandler>();

            if (itemDragHandler.Item != null && itemDragHandler.Item.Equals(e.Item))
            {
                image.enabled = false;
                image.sprite = null;
                itemDragHandler.Item = null;

                break;
            }
        }
    }

    public void CloseInventoryUi()
    {
        closeInventoryButton.SetActive(false);
        SpawnManager.Instance.itemActive = false;
        EventManager.ReactivateThings?.Invoke();

    }


    public void ActivateVision()
    {

        if (SpawnManager.Instance.abilityRunning == false)
        {

            if (GameManager.Instance.isVisionActive == false)
            {
                GameManager.Instance.isVisionActive = true;

                SpawnManager.Instance.abilityRunning = true;
                EventManager.ActivateVision?.Invoke();
            }
        }
        else if (SpawnManager.Instance.abilityRunning == true && GameManager.Instance.isVisionActive == true)
        {
            GameManager.Instance.isVisionActive = false;
            SpawnManager.Instance.abilityRunning = false;
            EventManager.DeactivationVision?.Invoke();

        }
        else
        {
            if (SpawnManager.Instance.isFeedBackTextRunning == false)
            {
                SpawnManager.Instance.isFeedBackTextRunning = true;
                StartCoroutine(TextAnim());
            }
        }


    }

    IEnumerator TextAnim()
    {
        textFeedback.SetActive(true);
        yield return new WaitForSeconds(2f);
        textFeedback.SetActive(false);
        SpawnManager.Instance.isFeedBackTextRunning = false;
    }
    public void Dominance()
    {
        Debug.Log(SpawnManager.Instance.abilityRunning);
        Debug.Log(SpawnManager.Instance.canActivateDominace);
        if (SpawnManager.Instance.abilityRunning == false)
        {
            if (SpawnManager.Instance.canActivateDominace == true && GameManager.Instance.roomIndex == 1)
            {
                textFeedback.SetActive(false);
                SpawnManager.Instance.abilityRunning = true;
                GameManager.Instance.abiility.GetComponent<ButtonColorChanger>().enabled = false;
                GameManager.Instance.abiility.image.color = Color.white;

                SpawnManager.Instance.canActivateDominace = false;
                EventManager.DominanceStart?.Invoke();

            }
        }
        else
        {
            if (SpawnManager.Instance.isFeedBackTextRunning == false)
            {
                SpawnManager.Instance.isFeedBackTextRunning = true;
                StartCoroutine(TextAnim());
            }
        }

    }

    public void CloseDominance()
    {
        if (SpawnManager.Instance.canActivateDominace == false)
        {
            EventManager.DominanceEnd?.Invoke();
            SpawnManager.Instance.canActivateDominace = true;
            SpawnManager.Instance.abilityRunning = false;
        }

    }
    public void BloodSorcery()
    {
        if (SpawnManager.Instance.abilityRunning == false)
        {
            if (!firstActivationBlood)
            {
                SorceryActivation.GetComponent<ButtonColorChanger>().enabled = false;
                SorceryActivation.image.color = Color.white;
                SorceryActivation.GetComponent<DialogueTrigger>().enabled = true;
                GameManager.Instance.BloodSorceryActive = true;
                firstActivationBlood = true;
            }

        }
        else
        {
            if (SpawnManager.Instance.isFeedBackTextRunning == false)
            {
                SpawnManager.Instance.isFeedBackTextRunning = true;
                StartCoroutine(TextAnim());
            }
        }

    }
    public void CloseMiniGame()
    {
        EventManager.CirclePuzzleFinish?.Invoke();
    }

    public void EndScene()
    {
        SceneManager.LoadScene("Chronicle2-Remake");
    }
}
