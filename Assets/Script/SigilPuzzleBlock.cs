using UnityEngine;

public class SigilPuzzleBlock : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("CirclePuzzle"))
        {
            Debug.Log("pORCOdIO");
        }
    }
}
