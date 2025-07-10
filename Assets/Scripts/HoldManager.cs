using UnityEngine;

public class HoldManager : MonoBehaviour
{
    public GameObject heldObject;
    public static HoldManager Instance;
    public Vector3 mouseWorldPos;
    public bool canGrab;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        canGrab = true;
    }
}
