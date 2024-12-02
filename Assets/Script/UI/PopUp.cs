using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class PopUp : MonoBehaviour, IPointerClickHandler
{
    [NonSerialized] public List<string> text;
    private TextMeshProUGUI TextMeshPro;
    private int index;
    private ItemCollectable item;
    //public bool isTimeToCollect;
    //public GameObject objToCollect;
    private void Awake()
    {
        TextMeshPro = GetComponentInChildren<TextMeshProUGUI>();

    }

    public void AssignItem(ItemCollectable item)
    {
        this.item = item;
    }
    public void AssignText(string[] text)
    {
        this.text = new List<string>();
        for (int i = 0; i < text.Length; i++)
        {
            this.text.Add(text[i]);
        }
        index = 0;
        TextMeshPro.text = text[index];
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        index++;
        if (index > text.Count - 1)
        {
            gameObject.SetActive(false);
            if (item != null)
            {
                GameManager.Instance.inventory.AddItem(item);
                EventManager.DeactivateMask?.Invoke();
            }
            return;
        }
        TextMeshPro.text = text[index];
    }
}
