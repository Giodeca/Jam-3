using UnityEngine;

public class HasKeyReturn : MonoBehaviour
{
    public GameObject obj;


    public void CollectItem()
    {
        IInventoryItem bookItem = obj.GetComponent<IInventoryItem>();
        GameManager.Instance.inventory.AddItem(bookItem);

    }
}
