using UnityEngine;

public class TrashCan : MonoBehaviour
{
    public int trashQuantity;
    public bool isOpen = false;
    public Sprite[] sprites;
    public GameObject condimentPrefab;
    void Awake()
    {
        trashQuantity = 0;
    }
    private void OnMouseDown()
    {
        if (isOpen)
        {
            isOpen = false;
            GetComponent<SpriteRenderer>().sprite = sprites[0];
            Debug.Log("Trash can closed.");
            gameObject.tag = "Untagged";
        }
        else
        {
            isOpen = true;
            GetComponent<SpriteRenderer>().sprite = sprites[1];
            Debug.Log("Trash can opened.");
            gameObject.tag = "CanDropCondimentHere";
        }
    }
    private void Update()
    {
        if (isOpen && Input.GetMouseButtonDown(1) && HoldManager.Instance.heldObject == condimentPrefab)
        {
            AddTrash();
        }
    }
    public void AddTrash()
    {
        if (isOpen)
        {
            trashQuantity++;
            Debug.Log("Trash added. Current quantity: " + trashQuantity);
        }
        else
        {
            Debug.Log("Cannot add trash, the can is closed.");
        }
    }
}
