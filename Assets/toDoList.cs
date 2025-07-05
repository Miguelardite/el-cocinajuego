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
    
    private bool enabled1, enabled2, enabled3;

    void Awake()
    {
        enabled1 = false;
        enabled2 = false;
        enabled3 = false;
    }

    void inicia(int pos, string text)
    {
        switch (pos)
        {
            case 1:
                enabled1 = false;
                elapsed1 = 0f;
                tmpText1.text = text;       //iniciar tareas nuevas
                break;
            case 2:                         //1 = top, 2 = mid, 3 = bot
                enabled2 = false;
                elapsed2 = 0f;
                tmpText2.text = text;
                break;
            case 3:
                enabled3 = false;
                elapsed3 = 0f;
                tmpText3.text = text;
                break;
        }
    }

    public void iniciaTarea(string text)
    {
        if (enabled1) inicia(1, text);        //toda esta parrafada es para ver donde poner la tarea nueva
        else if (enabled2) inicia(2, text);   //si hay un espacio libre la pone ahi, si no, sustituye la tarea mas antigua
        else if (enabled3) inicia(3, text);
        else if (elapsed1 > elapsed2 && elapsed1 > elapsed3) inicia(1, text);
        else if (elapsed2 > elapsed1 && elapsed2 > elapsed3) inicia(2, text);
        else inicia(3, text);
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
            enabled1 = true;
        }
        if (elapsed2 >= duration)
        {
            tmpText2.text = "";
            enabled2 = true;
        }
        if (elapsed3 >= duration)
        {
            tmpText3.text = "";
            enabled3 = true;
        }
    }
}
