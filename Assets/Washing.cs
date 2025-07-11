using UnityEngine;

public class Washing : MonoBehaviour
{
    private float elapsed;
    public bool active, first, oven;
    public AudioSource audio, centrif, abrir;
    public bool completada;

    public GameObject cover;

    public void iniciaContador()
    {
        elapsed = 0f;
        oven = true;
    }

    void Awake()
    {
        first = true;
        active = false;
        completada = false;
        oven = false;
        cover.SetActive(false);
    }

    void Update()
    {
        if (oven)
        {
            elapsed += Time.deltaTime;
            if (first && elapsed >= 10f && audio != null)
            {
                audio.Play();
                first = false;
                active = true;
                if (centrif != null) centrif.Stop();
            }
        }
    }

    public void OnMouseDown()
    {
        if (active)
        {
            active = false;
            completada = true;
            if (audio != null) audio.Stop();
            if (abrir != null) abrir.Play();
            cover.SetActive(true);
        }
    }
}
