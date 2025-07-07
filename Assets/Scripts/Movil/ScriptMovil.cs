using UnityEngine;
using UnityEngine.InputSystem;

public class ScriptMovil : MonoBehaviour, InputSystem_Actions.IUIActions
{
    public GameObject phone;
    public Vector3 showPosition;
    public Vector3 hidePosition;
    public bool isMovilActive = false;
    public bool canMove = true;
    private InputSystem_Actions inputActions;

    private void Awake()
    {
        inputActions = new InputSystem_Actions();
        inputActions.UI.SetCallbacks(this);
        // Set the initial position of the phone
        phone.transform.localPosition = hidePosition;
    }
    private void OnEnable()
    {
        inputActions.Enable();
    }
    private void OnDisable()
    {
        inputActions.Disable();
    }
    public void OnToggleMovil(InputAction.CallbackContext context)
    {
        if (context.performed && canMove)
        {
            canMove = false;
            Debug.Log("Toggle Movil Input Action Triggered");
            if (isMovilActive)
            {
                StartCoroutine(PhoneCoroutine(false));
            }
            else
            {
                StartCoroutine(PhoneCoroutine(true));
            }
        }
    }
    private System.Collections.IEnumerator PhoneCoroutine(bool goingup)
    {
        Debug.Log("Moving phone coroutine started. Going up: " + goingup);  
        if (goingup)
        {
            Debug.Log("Moving phone up");
            while (phone.transform.localPosition != showPosition)
            {
                /*phone.transform.localPosition = Vector3.MoveTowards(phone.transform.localPosition, showPosition, 150f);
                yield return new WaitForSeconds(2);
                isMovilActive = true;*/
                StartCoroutine(MoveCoroutine(1, 0.1f));
                yield return null;
            }
        }
        else
        {
            Debug.Log("Moving phone down");
            while (phone.transform.localPosition != hidePosition)
            {
                /*phone.transform.localPosition = Vector3.MoveTowards(phone.transform.localPosition, hidePosition, 150f);
                yield return new WaitForSeconds(2);
                isMovilActive = false;*/
                StartCoroutine(MoveCoroutine(-1, 0.1f));
                yield return null;
            }
        }
    }
    private System.Collections.IEnumerator MoveCoroutine(int direction, float time)
    {
        if (time <= 1f && phone.transform.localPosition.y<=-120f && phone.transform.localPosition.y >= -500f)
        {
            Vector3 targetPosition = phone.transform.localPosition + new Vector3(0, 380 / 20 * direction, 0);
            phone.transform.localPosition = targetPosition;
            if (phone.transform.localPosition.y==-101)
            {
                phone.transform.localPosition = new Vector3(-180, -120, 0);
                time = 2f;
                isMovilActive = true;
                canMove = true;
            }
            else if (phone.transform.localPosition.y == -519)
            {
                phone.transform.localPosition = new Vector3(-180, -500, 0);
                time = 2f;
                isMovilActive =false;
                canMove = true;
            }
            else
            {
                yield return new WaitForSeconds(0.1f);
                StartCoroutine(MoveCoroutine(direction, time + 0.1f));
            }

        }
        else
        {
            StopCoroutine(MoveCoroutine(direction, time));
            yield return null;
        }
    }

}
