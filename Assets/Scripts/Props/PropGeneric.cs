using UnityEngine;

public abstract class PropGeneric : MonoBehaviour
{
    internal bool isActive;
    public string propMisionName;
    public string tagChicken = "CanDropChickenHere";
    public GameObject chicken;

    public void OnMouseDown()
    {
        isActive = !isActive;

        if (isActive)
        {
            Debug.Log("Abierto");
            gameObject.tag = tagChicken;
            if (chicken.transform.parent == transform)
            {
                chicken.SetActive(true);
            }
        }
        else
        {
            Debug.Log("Cerrado");
            gameObject.tag = "Untagged";
            if (chicken.transform.parent == transform)
            {
                chicken.SetActive(false);
            }
        }
        Action();
    }
    public abstract void Action();
}
