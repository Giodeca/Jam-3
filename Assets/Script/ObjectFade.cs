using UnityEngine;

public class ObjectFade : MonoBehaviour
{
    [SerializeField] private float fadeSpeed, fadeAmount;
    float originalOpacity;
    Material mat;
    MeshRenderer mesh;
    public bool DoFade;


    private void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        mat = GetComponent<Renderer>().material;
        originalOpacity = mat.color.a;

    }

    private void Update()
    {


        if (DoFade)
        {
            FadeNow();

        }
        else
            ResetFade();

    }




    void FadeNow()
    {
        mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, fadeAmount, fadeSpeed * Time.deltaTime));
        mat.color = smoothColor;
    }
    void ResetFade()
    {
        mesh.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        Color currentColor = mat.color;
        Color smoothColor = new Color(currentColor.r, currentColor.g, currentColor.b, Mathf.Lerp(currentColor.a, originalOpacity, fadeSpeed * Time.deltaTime));
        mat.color = smoothColor;
    }
}
