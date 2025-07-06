using UnityEngine;

public class Chicken : MonoBehaviour
{
    public float frozenPercent = 50;
    public float cookingPercent = 0;
    public bool followMouse;
    public void Awake()
    {
        followMouse = false;
    }

    public void OnMouseDown()
    {
        if (!followMouse)
        {
            transform.SetParent(null);
            followMouse = true;
        }
        else
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D[] hits = Physics2D.OverlapPointAll(mousePos);
            bool dropped = false;

            foreach (Collider2D hit in hits)
            {
                if (hit.gameObject == gameObject)
                    continue;

                if (hit.CompareTag("CanDropChickenHere"))
                {
                    transform.position = hit.transform.position;
                    transform.SetParent(hit.transform);
                    followMouse = false;
                    dropped = true;
                    break;
                }
            }

            if (!dropped)
            {
                Debug.Log("No se puede soltar en esta zona.");
            }
        }
    }
    private void Update()
    {
        if (followMouse)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
        ChangeSprite();
    }

    void ChangeSprite()
    {
        if (frozenPercent > 0)
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
            Debug.Log("Pollo congelado");
        }
        else
        {
            if (cookingPercent > 100)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.black;
                Debug.Log("Pollo quemado");
            }
            else if (cookingPercent >= 0 && cookingPercent <= 50)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
                Debug.Log("Pollo crudo");
            }
            else if (cookingPercent > 50 && cookingPercent <= 100)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                Debug.Log("Pollo listo");
            }
        }
    }
}
