using System.Collections;
using UnityEngine;

public class LiquidLogic : MonoBehaviour
{
    public Renderer mesh;
    public float liquidAmount;
    public Material liquidMaterial;

    private void Start()
    {
        liquidMaterial = mesh.material;

        StartCoroutine(LiquidGoingUp(2, -2, 5));
        Debug.Log("Here");
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
            StartCoroutine(LiquidGoingUp(2, -2, 5));
    }

    private IEnumerator LiquidGoingUp(float startValue, float endValue, float duration)
    {
        float elapseTime = 0;
        while (elapseTime < duration)
        {
            elapseTime += Time.deltaTime;
            liquidAmount = Mathf.Lerp(startValue, endValue, elapseTime / duration);
            SetLiquidAmount(liquidAmount);
            yield return null;
        }

        SetLiquidAmount(endValue);

    }

    public void SetLiquidAmount(float amount)
    {
        liquidMaterial.SetFloat("_LiquidAmount", amount);
    }


}
//CheckForPipeCollision();
//private void CheckForPipeCollision()
//{

//    Debug.Log("Here2");
//    RaycastHit hit;
//    if (Physics.Raycast(upChecK.transform.position, Vector3.up, out hit, radious, mask))
//    {
//        Debug.Log("Here3");
//        if (hit.collider.CompareTag("Pipe"))
//        {
//            hit.collider.gameObject.GetComponent<LiquidLogic>().enabled = true;
//            Debug.Log("Here3");
//        }
//    }

//}