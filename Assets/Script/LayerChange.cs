using UnityEngine;

public class LayerChange : MonoBehaviour
{
    public int newLayer; // Il nuovo layer da assegnare all'oggetto

    private void OnEnable()
    {
        EventManager.ChangeLayer += OnChangeLayer;
    }

    private void OnDisable()
    {
        EventManager.ChangeLayer -= OnChangeLayer;
    }

    void OnChangeLayer()
    {

        gameObject.layer = newLayer;
        Debug.Log($"Layer cambiato a {newLayer} per {gameObject.name}");
    }
}
