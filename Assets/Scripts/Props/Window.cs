using UnityEngine;

public class Window : MonoBehaviour
{
    public bool isOpen = false;

    private void Awake()
    {
        isOpen = false;
    }
    public void OnMouseDown()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            transform.position += new Vector3(0, 4, 0);
        }
        else
        {
            transform.position -= new Vector3(0, 4, 0);
        }
    }
}
