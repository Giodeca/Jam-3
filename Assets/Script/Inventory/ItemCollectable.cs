using UnityEngine;

public class ItemCollectable : MonoBehaviour, IInventoryItem
{
    public string Name
    {
        get
        {
            return "Key";

        }
    }

    public GameObject ToIstantiate;
    public Sprite _Image = null;

    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDrop()
    {
        Debug.Log("Here");
        RaycastHit hit;
        Ray ray = GameManager.Instance.mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
        }
    }

    public virtual void OnUse()
    {
        if (GameManager.Instance.blockInventory == false)
        {
            if (SpawnManager.Instance.DominanceIsActive == false)
            {
                if (SpawnManager.Instance.itemActive == false)
                {
                    EventManager.ObjAssignation?.Invoke(ToIstantiate);
                    EventManager.DeactivateThings?.Invoke();
                    SpawnManager.Instance.itemActive = true;
                    EventManager.DeactivateMask?.Invoke();
                }
            }
        }


    }
}
