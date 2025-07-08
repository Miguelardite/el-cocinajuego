using Unity.VisualScripting;
using UnityEngine;

public class SeasoningBox : MonoBehaviour
{
    public GameObject chicken;
    public GameObject worktop;
    public GameObject condimentPrefab;
    public GameObject condiment;

    public Sprite openBag;
    bool followMouse;

    void Update()
    {
        //click derecho para abrir la caja de condimentos

        if (Input.GetMouseButtonDown(1))
        {
            if (followMouse)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(condiment.transform.position, 0.5f);

                foreach (Collider2D collider in colliders)
                {
                    Debug.Log("Collider: " + collider.name);
                    if (collider.CompareTag("CanDropCondimentHere"))
                    {
                        DropCondiment();
                    }
                }
            }
            else
            {
                SetCursor();
                if (Vector2.Distance(transform.position, HoldManager.Instance.mouseWorldPos) < 3f && HoldManager.Instance.heldObject == null)
                {
                    followMouse = true;
                    GrabCondiment();
                }
            }
        }
        
        if (Input.GetMouseButtonDown(0))
        {
            if (followMouse)
            {
                UseCondiment();
            }
        }

        if (followMouse)
        {
            SetCursor();
            condiment.transform.position = HoldManager.Instance.mouseWorldPos;
        }
    }
    void GrabCondiment()
    {
        //instancia el condimento
        condiment = Instantiate(condimentPrefab, HoldManager.Instance.mouseWorldPos, Quaternion.identity);
        HoldManager.Instance.heldObject = condiment;
    }
    void UseCondiment()
    {
        if (chicken.transform.parent == worktop.transform)
        {
            // Verifica si el condimento está cerca del pollo
            if (Vector2.Distance(condiment.transform.position, chicken.transform.position) < 3f && chicken.GetComponent<Chicken>().frozenPercent <=0)
            {
                Debug.Log("Condiento usado");
                chicken.GetComponent<Chicken>().seasoning += 5;
                condiment.GetComponent<SpriteRenderer>().sprite = openBag;
            }
            else if (Vector2.Distance(condiment.transform.position, chicken.transform.position) < 3f && chicken.GetComponent<Chicken>().frozenPercent > 0)
            {
                Debug.Log("Condimento no puede ser usado en pollo congelado");
            }
            else
            {
                Debug.Log("Condimento no está cerca del pollo");
            }
        }
        else
        {
            Debug.Log("El pollo no está en la mesa de trabajo");
        }
    }
    void DropCondiment()
    {
        // Si el condimento está siendo arrastrado, lo soltamos
        if (followMouse)
        {
            followMouse = false;
            HoldManager.Instance.heldObject = null;
            Destroy(condiment);
            condiment = null;
            Debug.Log("Condimento soltado");
        }
    }
    void SetCursor()
    {
        Camera mainCamera = Camera.main;
        Vector3 mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = Mathf.Abs(mainCamera.transform.position.z);
        HoldManager.Instance.mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        HoldManager.Instance.mouseWorldPos.z = transform.position.z;
    }
}
