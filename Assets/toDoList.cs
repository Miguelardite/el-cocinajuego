using UnityEngine;
using TMPro;

public class toDoList : MonoBehaviour
{
    public TextMeshProUGUI tmpText;

    public float duration = 30f;

    public float maxDilate = 1f;

    private static readonly int ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
    private float elapsed = 0f;
    private Material matInstance;

    void Start()
    {
        matInstance = Instantiate(tmpText.fontMaterial);
        tmpText.fontMaterial = matInstance;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);

        float dilate = Mathf.Lerp(0f, maxDilate, t);
        matInstance.SetFloat(ID_FaceDilate, dilate);

        Color c = tmpText.color;
        c.a = 1f - t;
        tmpText.color = c;

        if (elapsed >= duration)
        {
            tmpText.text = "";
            enabled = false;
        }
    }
}
