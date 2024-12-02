using System.Collections.Generic;
using UnityEngine;

public class UpdateFade : MonoBehaviour
{
    private ObjectFade fade;
    [SerializeField] private GameObject playerRef;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private float rayDistance = 5f;
    [SerializeField] private int rayCount = 2;

    private HashSet<ObjectFade> objectsHitLastFrame = new HashSet<ObjectFade>();

    private void Update()
    {
        FaderCalc();
    }

    private void FaderCalc()
    {
        if (playerRef != null)
        {
            Vector3 foreward = playerRef.transform.forward;
            Vector3 origin = playerRef.transform.position;

            float algleIncrement = 100f / (rayCount - 1);
            float startAngle = -50;

            HashSet<ObjectFade> objectsHitThisFrame = new HashSet<ObjectFade>();

            for (int i = 0; i < rayCount; i++)
            {
                float currentAngle = startAngle + (i * algleIncrement);
                Vector3 direction = Quaternion.Euler(0, currentAngle, 0) * foreward;

                RaycastHit hit;

                if (Physics.Raycast(origin, direction, out hit, rayDistance, wallLayer))
                {
                    Debug.DrawRay(origin, direction * hit.distance, Color.red);

                    ObjectFade hitObjectFade = hit.collider.gameObject.GetComponent<ObjectFade>();
                    if (hitObjectFade != null)
                    {
                        hitObjectFade.DoFade = true;
                        objectsHitThisFrame.Add(hitObjectFade);
                        for (int j = 0; j < hitObjectFade.gameObject.transform.childCount; j++)
                        {

                            hitObjectFade.transform.GetChild(j).GetComponent<ObjectFade>().DoFade = true;
                            objectsHitThisFrame.Add(hitObjectFade.gameObject.transform.GetChild(j).GetComponent<ObjectFade>());
                        }
                        //Debug.Log(hit.collider.gameObject.name);
                    }
                }
                else
                {
                    Debug.DrawRay(origin, direction * rayDistance, Color.green);
                }
            }

            // Set DoFade to false for objects not hit this frame
            foreach (ObjectFade obj in objectsHitLastFrame)
            {
                if (!objectsHitThisFrame.Contains(obj))
                {
                    obj.DoFade = false;
                    for (int i = 0; i < obj.transform.childCount; i++)
                    {
                        if (obj.transform.GetChild(i).GetComponent<ObjectFade>() != null)
                            obj.transform.GetChild(i).GetComponent<ObjectFade>().DoFade = false;
                    }
                }
            }

            // Update the last hit objects for the next frame
            objectsHitLastFrame = objectsHitThisFrame;
        }
    }
}
