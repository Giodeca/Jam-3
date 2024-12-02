using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemClickHandler : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Inventory _invertory;
    [SerializeField] private Image _image;
    [SerializeField] private Image imageColor;
    public void OnItemClicked()
    {
        ItemDragHandler dragHandler = gameObject.transform.Find("ItemImage").GetComponent<ItemDragHandler>(); //this is very evil

        IInventoryItem item = dragHandler.Item;

        _invertory.UseItem(item);

        if (item != null)
            item.OnUse();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemClicked();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {


    }

    public void OnPointerExit(PointerEventData eventData)
    {

    }
}
