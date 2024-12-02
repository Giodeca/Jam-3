using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ChangePostIt : MonoBehaviour
{
    [SerializeField] SPostIt PostIt;
    TextMeshProUGUI TMPro;

    private void Awake()
    {
        TMPro = GetComponent<TextMeshProUGUI>();
        ChangePostItText(PostIt);
    }

    private void ChangePostItText(SPostIt PostIt)
    {
        TMPro.text = PostIt.text;
    }
}
