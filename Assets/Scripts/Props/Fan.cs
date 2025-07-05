using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fan : PropGeneric
{
    public GameObject postIt;
    public AudioSource audioSource;

    private void Awake()
    {
        isActive = true;
        propMisionName = "Apaga el ventilador";
        //el sonido del ventilador se reproduce por defecto
    }

    public override void Action()
    {
        //si el ventilador está activo, hará ruido y ejecutará la animación. Si está apagado, cada cierto tiempo
        //saldrán mensajes de que hace calor
        if (isActive)
        {
            Debug.Log("Encendido");
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
        else
        {
            Debug.Log("Apagado");
            InvokeRepeating("ItsHot", 0f, Random.Range(15f, 25f));
            if (audioSource != null)
            {
                audioSource.Stop();
            }
        }
    }
    void ItsHot()
    {
        if (postIt != null)
        {
            postIt.GetComponent<toDoList>().iniciaTarea("Hace calor");
        }
    }
}
