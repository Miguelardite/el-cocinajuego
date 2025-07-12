using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using TMPro;

public class ScriptMovil : MonoBehaviour
{
    public GameObject phone;
    public Vector3 showPosition;
    public Vector3 hidePosition;
    public bool isMovilActive = false;
    public bool canMove = true;
    public GameObject cookbookcanvas;
    public GameObject menu;
    public GameObject tistos;
    public GameObject wassap;
    public GameObject postIt;
    public TextMeshProUGUI hora;
    public AudioSource notif, catMusic;
    public Collider2D[] colliders;
    [SerializeField]
    private float elapsed;
    public bool basura, ventilador, ventana;
    public bool finish = false;
    private List<string> textos = new ();
    private string[] horas = { "18:30", "18:31", "18:32", "18:33", "18:34", "18:35", "18:36", "18:37", "18:38", "18:39", "18:40", "18:41", "18:42", "18:43", "18:44" };
    bool canToggleMovil = true;

    private void Awake()
    {
        colliders = FindObjectsOfType<Collider2D>();
        phone.transform.localPosition = hidePosition;
        GoToMenu();
        textos.Add("-Cook the chicken");
        textos.Add("-Turn off the washing machine");
        elapsed = 0f;
        basura = false;
        ventilador = false;
        ventana = false;
    }

    public void GoToWsp()
    {
        menu.SetActive(false);
        tistos.SetActive(false);
        wassap.SetActive(true);
        for(int i = Mathf.Max(0, textos.Count - 3); i < textos.Count; ++i)
        {
            postIt.GetComponent<toDoList>().iniciaTarea(textos[i]);
        }
    }
    public void GoToTistos()
    {
        Time.timeScale = 2f;
        menu.SetActive(false);
        tistos.SetActive(true);
        wassap.SetActive(false);
        catMusic.Play();
    }
    public void GoToMenu()
    {
        Time.timeScale = 1f;
        menu.SetActive(true);
        tistos.SetActive(false);
        wassap.SetActive(false);
        if (catMusic.isPlaying)
        {
            catMusic.Stop();
        }
    }
    public void OnToggleMovil()
    {
        if (canToggleMovil)
        {
            canToggleMovil = false;
            Invoke(nameof(ResetToggleMovil), 0.5f);
            if (canMove && elapsed < 299f && !cookbookcanvas.gameObject.activeSelf)
            {
                canMove = false;
                Debug.Log("Toggle Movil Input Action Triggered");
                if (isMovilActive)
                {
                    StartCoroutine(PhoneCoroutine(false));
                    CameraMovement.instance.canMove = true;
                    HoldManager.Instance.canGrab = true;
                    foreach (Collider2D collider in colliders)
                    {
                        collider.enabled = true;
                    }
                }
                else
                {
                    CameraMovement.instance.canMove = false;
                    HoldManager.Instance.canGrab = false;

                    foreach (Collider2D collider in colliders)
                    {
                        collider.enabled = false;
                    }
                    GoToMenu();
                    StartCoroutine(PhoneCoroutine(true));
                }
            }
        }
    }
    private void ResetToggleMovil()
    {
        canToggleMovil = true;
    }
    private System.Collections.IEnumerator PhoneCoroutine(bool goingup)
    {
        Debug.Log("Moving phone coroutine started. Going up: " + goingup);  
        if (goingup)
        {
            Debug.Log("Moving phone up");
            while (phone.transform.localPosition != showPosition)
            {
                StartCoroutine(MoveCoroutine(1, 0.1f));
                yield return null;
            }
        }
        else
        {
            Debug.Log("Moving phone down");
            while (phone.transform.localPosition != hidePosition)
            {
                StartCoroutine(MoveCoroutine(-1, 0.1f));
                yield return null;
            }
        }
    }
    private System.Collections.IEnumerator MoveCoroutine(int direction, float time)
    {
        if (time <= 1f && phone.transform.localPosition.y<=-120f && phone.transform.localPosition.y >= -500f)
        {
            Vector3 targetPosition = phone.transform.localPosition + new Vector3(0, 380 / 20 * direction, 0);
            phone.transform.localPosition = targetPosition;
            if (phone.transform.localPosition.y==-101)
            {
                phone.transform.localPosition = new Vector3(-180, -120, 0);
                time = 2f;
                isMovilActive = true;
                canMove = true;
            }
            else if (phone.transform.localPosition.y == -519)
            {
                phone.transform.localPosition = new Vector3(-180, -500, 0);
                time = 2f;
                isMovilActive = false;
                canMove = true;
                GoToMenu();
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(MoveCoroutine(direction, time + 0.1f));
            }

        }
        else
        {
            StopCoroutine(MoveCoroutine(direction, time));
            yield return null;
        }
    }

    void Update()
    {
        if (!finish)
        {
            elapsed += Time.deltaTime;
            
            int aux = (int)elapsed / 20;
            hora.text = horas[aux];
            if (elapsed >= 90 && !ventilador)
            {
                textos.Add("-Turn off the fan");
                notif.Play();
                ventilador = true;
            }
            if (elapsed >= 150 && !ventana)
            {
                textos.Add("-Open the window");
                notif.Play();
                ventana = true;
            }
            if (elapsed >= 180 && !basura)
            {
                textos.Add("-Close the trash");
                notif.Play();
                basura = true;
            }
            //Mas tareas con el tiempo? 
        }
        if (elapsed >= 299f && !finish)
        {
            if (isMovilActive)
            {
                StartCoroutine(PhoneCoroutine(false));
                CameraMovement.instance.canMove = true;
                HoldManager.Instance.canGrab = true;
                foreach (Collider2D collider in colliders)
                {
                    collider.enabled = true;
                }
            }
            finish = true;
            Debug.Log("Finishing the game, calling MomChecklist.Instance.CheckThings() now.");
            MomChecklist.Instance.StartMom();
        }
    }

}
