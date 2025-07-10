using UnityEngine;
using UnityEngine.UI;

public class Cookbook : MonoBehaviour
{
    public Collider2D[] colliders;
    public Canvas cookbookCanvas;
    private void Awake()
    {
        //get all colliders in the scene
        colliders = FindObjectsOfType<Collider2D>();
    }
    public void OpenCookbook()
    {
        CameraMovement.instance.canMove = false;
        HoldManager.Instance.canGrab = false;

        foreach (Collider2D collider in colliders)
        {
            collider.enabled = false;
        }
    }
    public void CloseCookbook()
    {
        CameraMovement.instance.canMove = true;
        HoldManager.Instance.canGrab = true;
        foreach (Collider2D collider in colliders)
        {
            collider.enabled = true;
        }
        cookbookCanvas.gameObject.SetActive(false);
    }
    public void OnMouseDown()
    {
        cookbookCanvas.gameObject.SetActive(true);
        OpenCookbook();
    }
}
