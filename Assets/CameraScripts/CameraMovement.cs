using System.Collections.Generic;
using NUnit.Framework;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem; // Ensure you have the Input System package installed

public class CameraMovement : MonoBehaviour, InputSystem_Actions.ICamerasActions
{
    public CinemachineCamera frontal;
    public CinemachineCamera derecha;
    public CinemachineCamera izquierda;
    public CinemachineCamera trasera;
    public CinemachineCamera arriba;
    public List<CinemachineCamera> cameras;
    public int currentCameraIndex;
    public BlinkScript blinkScript;

    //Cada pared es una lista con sus fotos
    public List<List<GameObject>> images;
    public List<GameObject> imagesFrontal;
    public List<GameObject> imagesDerecha;
    public List<GameObject> imagesTrasera;
    public List<GameObject> imagesIzquierda;
    public List<GameObject> imagesArriba;


    //usa el input system para cambiar entre las camaras
    void Awake()
    {
        // Ensure all cameras are added to the list
        cameras = new List<CinemachineCamera>
        {
            frontal,
            derecha,
            trasera,
            izquierda
        };
        images = new List<List<GameObject>>
        {
            imagesFrontal,
            imagesDerecha,
            imagesTrasera,
            imagesIzquierda,
        };
        // Set the initial camera to frontal
        SwitchCamera(frontal);
        currentCameraIndex = 0;
    }
    // Switches the active camera to the specified camera
    public void SwitchCamera(CinemachineCamera newCamera)
    {
        // Disable all cameras
        frontal.gameObject.SetActive(false);
        derecha.gameObject.SetActive(false);
        izquierda.gameObject.SetActive(false);
        trasera.gameObject.SetActive(false);
        arriba.gameObject.SetActive(false);
        // Enable the new camera
        newCamera.gameObject.SetActive(true);
    }

    //Comprueba si el usuario ha pulsado una tecla para cambiar de camara
    public void OnMirarArriba(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!arriba.gameObject.activeSelf)
            {
                SwitchCamera(arriba);
                blinkScript.BlinkArriba();
            }

        }
    }
    public void OnMirarAbajo(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (arriba.gameObject.activeSelf)
            {
                SwitchCamera(cameras[currentCameraIndex]);
                blinkScript.Blink(currentCameraIndex);
            }

        }
    }
    public void OnGirarDerecha(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!arriba.gameObject.activeSelf)
            {
                if (currentCameraIndex < cameras.Count - 1)
                {
                    currentCameraIndex++;
                }
                else
                {
                    currentCameraIndex = 0; // Loop back to the first camera
                }
                SwitchCamera(cameras[currentCameraIndex]);
                /*images[currentCameraIndex].ForEach(image => image.SetActive(true)); // Activate images for the new camera
                images[(currentCameraIndex + 1) % cameras.Count].ForEach(image => image.SetActive(false)); // Deactivate images for the next camera
                images[(currentCameraIndex + 2) % cameras.Count].ForEach(image => image.SetActive(false)); // Deactivate images for the next camera
                images[(currentCameraIndex + 3) % cameras.Count].ForEach(image => image.SetActive(false)); // Deactivate images for the next camera*/
                blinkScript.Blink(currentCameraIndex);
            }
        }
    }
    public void OnGirarIzquierda(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (!arriba.gameObject.activeSelf)
            {
                if (currentCameraIndex > 0)
                {
                    currentCameraIndex--;
                }
                else
                {
                    currentCameraIndex = cameras.Count - 1; // Loop back to the last camera
                }
                SwitchCamera(cameras[currentCameraIndex]);
                /*images[currentCameraIndex].ForEach(image => image.SetActive(true)); // Activate images for the new camera
                images[(currentCameraIndex + 1) % cameras.Count].ForEach(image => image.SetActive(false)); // Deactivate images for the next camera
                images[(currentCameraIndex + 2) % cameras.Count].ForEach(image => image.SetActive(false)); // Deactivate images for the next camera
                images[(currentCameraIndex + 3) % cameras.Count].ForEach(image => image.SetActive(false)); // Deactivate images for the next camera*/
                blinkScript.Blink(currentCameraIndex);
            }
        }
    }
    private System.Collections.IEnumerator BlurCoroutine()
    {
        yield return new WaitForSeconds(blinkScript.blinkDuration / 4);

    }
}
