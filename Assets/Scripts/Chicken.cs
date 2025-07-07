using UnityEngine;

public class Chicken : MonoBehaviour
{
    public float frozenPercent;
    public float cookingPercent;
    public float seasoning;
    public bool followMouse;
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
                if (!followMouse)
                {
                    transform.SetParent(null);
                    HoldManager.Instance.heldObject = gameObject;
                    followMouse = true;
                    Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    mousePos.z = 0f;
                    transform.position = mousePos;
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
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else
        {
            if (cookingPercent > 70)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
            }
            else if (cookingPercent >= 0 && cookingPercent <= 50)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            }
            else if (cookingPercent > 50 && cookingPercent <= 70)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
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
