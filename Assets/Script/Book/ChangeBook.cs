using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ChangeBook : MonoBehaviour
{
    [SerializeField] private SBook book;
    TextMeshProUGUI[] Pages = new TextMeshProUGUI[2];

    private void Awake()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            Pages[i] = transform.GetChild(i).gameObject.GetComponent<TextMeshProUGUI>();
        }
    }

    private void Start()
    {
        ChangeBookText(book);
        
    }

    private void ChangeBookText(SBook book)
    {
        Pages[0].text = book.FirstPage;
        Pages[1].text = book.SecondPage;
    }
}
