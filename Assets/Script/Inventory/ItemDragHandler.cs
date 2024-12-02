using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDragHandler : MonoBehaviour, /*IDragHandler, IEndDragHandler,*/IPointerEnterHandler, IPointerExitHandler
{
    public IInventoryItem Item { get; set; }
    private Vector3 startPosition;
    private Transform startParent;
    public Image image;

    public void OnPointerEnter(PointerEventData eventData)
    {
        image.color = Color.gray;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        image.color = Color.white;
    }

    //void Start()
    //{
    //    startPosition = transform.localPosition;
    //    startParent = transform.parent;
    //}

    //public void OnDrag(PointerEventData eventData)
    //{
    //    transform.position = Input.mousePosition;
    //}

    //public void OnEndDrag(PointerEventData eventData)
    //{
    //    if (transform.parent == startParent)
    //    {
    //        transform.localPosition = startPosition;
    //    }
    //    else
    //    {
    //        transform.localPosition = Vector3.zero;
    //    }
    //}
}
