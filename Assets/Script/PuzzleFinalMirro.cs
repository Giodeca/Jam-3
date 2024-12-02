using UnityEngine;

public class PuzzleFinalMirro : MonoBehaviour
{
    public GameObject[] final;


    private void OnMouseDown()
    {
        if (GameManager.Instance.canClickFinalMirror == true && GameManager.Instance.BloodSorceryActive == true)
        {
            foreach (GameObject gameObject in final)
            {
                gameObject.SetActive(true);
            }
        }
    }

}
