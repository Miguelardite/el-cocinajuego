using UnityEngine;

public class Chicken : MonoBehaviour
{
    public float frozenPercent;
    public float cookingPercent;
    public bool followMouse;
    public void Awake()
    {
        followMouse = false;
        frozenPercent = 50;
        cookingPercent = 0;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (!followMouse)
            {
                transform.SetParent(null);
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
                        break;
                    }
                }

                if (!dropped)
                {
                    Debug.Log("No se puede soltar en esta zona.");
                }
            }
        }

        if (followMouse)
        {
            Camera mainCamera = Camera.main;
            Vector3 mouseScreenPos = Input.mousePosition;
            mouseScreenPos.z = Mathf.Abs(mainCamera.transform.position.z);
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
            mouseWorldPos.z = transform.position.z;
            transform.position = mouseWorldPos;
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
}
