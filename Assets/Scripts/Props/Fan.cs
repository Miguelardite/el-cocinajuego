using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Fan : MonoBehaviour
{
    bool isActive; 
    public string propMisionName; 
    public GameObject postIt;
    public AudioSource audioSource;
    public Sprite[] sprites;
    public float timer;
    private void Awake()
    {
        isActive = true;
        propMisionName = "Apaga el ventilador";
    }
    private void Update()
    {
        if (isActive)
        {
            timer += Time.deltaTime;
            if (timer >= 0.1f)
            {
                timer = 0f;
                transform.Rotate(0f, 0f, 90f);
            }
        }
    }
    public void OnMouseDown()
    {
        isActive = !isActive;
        Action(); 
    }
    
    public void Action()
    {
        //si el ventilador est� activo, har� ruido y ejecutar� la animaci�n. Si est� apagado, cada cierto tiempo
        //saldr�n mensajes de que hace calor
        if (isActive)
        {
            Debug.Log("Encendido");
            if (audioSource != null)
            {
                audioSource.Play();
                GetComponent<SpriteRenderer>().sprite = sprites[0];
            }
        }
        else
        {
            Debug.Log("Apagado");
            InvokeRepeating("ItsHot", 0f, Random.Range(15f, 25f));
            if (audioSource != null)
            {
                audioSource.Stop();
                GetComponent<SpriteRenderer>().sprite = sprites[1];
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
