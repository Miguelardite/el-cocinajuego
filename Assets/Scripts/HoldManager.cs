using UnityEngine;

public class HoldManager : MonoBehaviour
{
    public GameObject heldObject;
    public static HoldManager Instance;
    public Vector3 mouseWorldPos;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
