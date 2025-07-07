using UnityEngine;

public class Washing : MonoBehaviour
{
    private float elapsed;
    private bool active, first;
    public AudioSource audio;
    public bool completada;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        elapsed = 0f;
        first = true;
        active = false;
        completada = false;
    }

    // Update is called once per frame
    void Update()
    {
        elapsed += Time.deltaTime;
        if (first && elapsed >= 10f && audio != null)
        {
            audio.Play();
            first = false;
            active = true;
        }
    }

    public void OnMouseDown()
    {
        if (active)
        {
            active = false;
            completada = true;
            if (audio != null) audio.Stop();
            //añadir cambio de sprite, animacion, lo que sea
        }
    }
}
