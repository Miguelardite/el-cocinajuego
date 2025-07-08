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




    void Awake()
    {
        cameras = new List<CinemachineCamera>
        {
            frontal,
            derecha,
            trasera,
            izquierda
        };

        SwitchCamera(frontal);
        currentCameraIndex = 0;
    }
    public void SwitchCamera(CinemachineCamera newCamera)
    {
        frontal.gameObject.SetActive(false);
        derecha.gameObject.SetActive(false);
        izquierda.gameObject.SetActive(false);
        trasera.gameObject.SetActive(false);
        arriba.gameObject.SetActive(false);
        newCamera.gameObject.SetActive(true);
    }

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
                blinkScript.Blink(currentCameraIndex);
            }
        }
    }
}
