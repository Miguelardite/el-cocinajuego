using UnityEngine;

public abstract class PropGeneric : MonoBehaviour
{
    internal bool isActive;
    public string propMisionName;
    public void OnMouseDown()
    {
        isActive = !isActive;
        Action();
    }
    public abstract void Action();
}
