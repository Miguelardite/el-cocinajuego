using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fan : MonoBehaviour
{
    bool isActive; // Indica si el ventilador está activo o no
    public string propMisionName; // Nombre de la misión asociada al ventilador
    public GameObject postIt;
    public AudioSource audioSource;

    private void Awake()
    {
        isActive = true;
        propMisionName = "Apaga el ventilador";
        //el sonido del ventilador se reproduce por defecto
    }
    public void OnMouseDown()
    {
        isActive = !isActive; // Cambia el estado del ventilador al hacer clic
        Action(); // Llama al método Action para manejar el comportamiento del ventilador
    }
    public void Action()
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
