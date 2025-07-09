using UnityEngine;

public abstract class PropGeneric : MonoBehaviour
{
    internal bool isActive;
    public string propMisionName;
    public string tagChicken = "CanDropChickenHere";
    public GameObject chicken;
    public Sprite[] sprites;

    public void OnMouseDown()
    {
        if (HoldManager.Instance.heldObject == null)
        {
            isActive = !isActive;

            if (isActive)
            {
                Debug.Log("Abierto");
                GetComponent<SpriteRenderer>().sprite = sprites[1];
                gameObject.tag = tagChicken;
                if (chicken.transform.parent == transform)
                {
                    chicken.SetActive(true);
                }
            }
            else
            {
                Debug.Log("Cerrado");
                GetComponent<SpriteRenderer>().sprite = sprites[0];
                gameObject.tag = "Untagged";
                if (chicken.transform.parent == transform)
                {
                    chicken.SetActive(false);
                }
            }
            Action();
        }
        else
        {
            Debug.Log("Debes tener las manos vacías para abrir y cerrar objetos");
        }
    }
    public abstract void Action();
}
