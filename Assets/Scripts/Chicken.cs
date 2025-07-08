using NUnit.Framework;
using UnityEngine;

public class Chicken : MonoBehaviour
{
    public float frozenPercent;
    public float cookingPercent;
    public float seasoning;
    public bool followMouse;
    public Sprite[] sprites;
    public void Awake()
    {
        followMouse = false;
        frozenPercent = 50;
        cookingPercent = 0;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            SetCursor();
            if (Vector2.Distance(transform.position, HoldManager.Instance.mouseWorldPos) < 3f)
            {
                if (!followMouse && HoldManager.Instance.heldObject == null)
                {
                    // Si no se está siguiendo el ratón y no hay objeto sostenido, se agarra el pollo
                    HoldManager.Instance.heldObject = gameObject;
                    followMouse = true;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0f;
                    transform.position = mousePos;
                }
                else if (HoldManager.Instance.heldObject != null && HoldManager.Instance.heldObject != gameObject)
                {
                    // Si ya hay un objeto sostenido, no se puede agarrar otro
                    Debug.Log("Ya tienes un objeto en las manos.");
                }
                else
                {
                    bool dropped = false;

                    Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 0.5f);

                    foreach (Collider2D collider in colliders)
                    {
                        if (collider.CompareTag("CanDropChickenHere"))
                        {
                            transform.SetParent(collider.transform);
                            transform.position = collider.transform.position;
                            followMouse = false;
                            dropped = true;
                            Debug.Log("Pollo soltado en: " + collider.name);
                            HoldManager.Instance.heldObject = null;
                            break;
                        }
                    }

                    if (!dropped)
                    {
                        Debug.Log("No se puede soltar en esta zona.");
                    }
                }
            }
        }

        if (followMouse)
        {
            SetCursor();
            transform.position = HoldManager.Instance.mouseWorldPos;
        }

        ChangeSprite();
    }

    void ChangeSprite()
    {
        if (frozenPercent > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
        }
        else
        {
            if (cookingPercent > 70)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[3];
            }
            else if (cookingPercent >= 0 && cookingPercent <= 50)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            }
            else if (cookingPercent > 50 && cookingPercent <= 70)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = sprites[2];
            }
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
