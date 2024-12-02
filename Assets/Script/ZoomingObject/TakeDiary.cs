using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeDiary : MonoBehaviour
{
    private void Start()
    {
        IInventoryItem item = GetComponent<IInventoryItem>();
        GameManager.Instance.inventory.AddItem(item);
    }
}
