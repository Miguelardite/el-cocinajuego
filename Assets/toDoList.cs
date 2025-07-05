using UnityEngine;
using TMPro;

public class toDoList : MonoBehaviour
{
    public TextMeshProUGUI tmpText;

    public float duration = 30f;

    public float maxDilate = 1f;

    // IDs de las propiedades del shader SDF
    private static readonly int ID_FaceDilate = Shader.PropertyToID("_FaceDilate");
    private float elapsed = 0f;
    private Material matInstance;

    void Start()
    {
        if (tmpText == null)
            tmpText = GetComponent<TextMeshProUGUI>();

        // Instanciamos el material para no modificar el compartido
        matInstance = Instantiate(tmpText.fontMaterial);
        tmpText.fontMaterial = matInstance;
    }

    void Update()
    {
        elapsed += Time.deltaTime;
        float t = Mathf.Clamp01(elapsed / duration);

        // Interpolamos el FaceDilate desde 0 hasta maxDilate
        float dilate = Mathf.Lerp(0f, maxDilate, t);
        matInstance.SetFloat(ID_FaceDilate, dilate);

        // Opcional: también puedes hacer un fade de alpha
        Color c = tmpText.color;
        c.a = 1f - t;
        tmpText.color = c;

        // Al final de los 30s, destruye u oculta
        if (elapsed >= duration)
        {
            // O bien
            // Destroy(gameObject);
            // O bien
            tmpText.text = "";
            enabled = false;
        }
    }
}
