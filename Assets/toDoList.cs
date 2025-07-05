using UnityEngine;
using TMPro;

public class toDoList : MonoBehaviour
{
    public TextMeshProUGUI tmpText1;
    public TextMeshProUGUI tmpText2;
    public TextMeshProUGUI tmpText3;

    public float duration = 30f;

    public float maxDilate = 1f;

    private float elapsed1 = 0f;
    private float elapsed2 = 0f;
    private float elapsed3 = 0f;

    private Material matInstance;

    void Awake()
    {
        //matInstance = Instantiate(tmpText.fontMaterial);
        //tmpText.fontMaterial = matInstance;
    }

    void Update()
    {
        elapsed1 += Time.deltaTime;
        elapsed2 += Time.deltaTime;
        elapsed3 += Time.deltaTime;

        float t1 = Mathf.Clamp01(elapsed1 / duration);
        float t2 = Mathf.Clamp01(elapsed2 / duration);
        float t3 = Mathf.Clamp01(elapsed3 / duration);


        Color c = tmpText1.color;
        c.a = 1f - t1;
        tmpText1.color = c;
        c.a = 1f - t2;
        tmpText2.color = c;
        c.a = 1f - t3;
        tmpText3.color = c;

        if (elapsed1 >= duration)
        {
            tmpText1.text = "";
        }
        if (elapsed2 >= duration)
        {
            tmpText2.text = "";
        }
        if (elapsed3 >= duration)
        {
            tmpText3.text = "";
        }
    }
}
