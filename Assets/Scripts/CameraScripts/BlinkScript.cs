using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class BlinkScript : MonoBehaviour
{
    public Vector3 blinkPositionArriba;
    public Vector3 blinkPositionDerecha;
    public Vector3 blinkPositionIzquierda;
    public Vector3 blinkPositionTrasera;
    public Vector3 blinkPositionFrontal;

    public float blinkDuration; // Duration of the blink effect

    public List<Vector3> blinkPositions;

    private void Awake()
    {
        blinkDuration = 0.125f;
        blinkPositions = new List<Vector3>
        {
            blinkPositionFrontal,
            blinkPositionDerecha,
            blinkPositionTrasera,
            blinkPositionIzquierda
        };
    }

    public void Blink(int cameraIndex)
    {
        if (cameraIndex < 0 || cameraIndex >= blinkPositions.Count)
        {
            Debug.LogError("Invalid camera index for blink effect.");
            return;
        }
        Vector3 targetPosition = blinkPositions[cameraIndex];
        StartCoroutine(BlinkCoroutine(targetPosition));
    }
    public void BlinkArriba()
    {
        StartCoroutine(BlinkCoroutine(blinkPositionArriba));
    }

    private System.Collections.IEnumerator BlinkCoroutine(Vector3 targetPosition)
    {
        gameObject.transform.position = targetPosition;
        gameObject.GetComponent<SpriteRenderer>().enabled = true;
        //haz que el sprite haga un blur 
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0); // Optional: make it semi-transparent
        yield return new WaitForSeconds(blinkDuration/4);
        StartCoroutine(BlurCoroutine(0.5f, true));
    }
    private System.Collections.IEnumerator BlurCoroutine(float blurValue, bool goingup)
    {
        gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, blurValue);
        yield return new WaitForSeconds(blinkDuration / 4);

        Debug.Log(gameObject.GetComponent<SpriteRenderer>().color.a);
        if (blurValue < 1f && goingup) 
        {
            StartCoroutine(BlurCoroutine(blurValue+0.5f, true));
        }
        else if (blurValue > 0)
        {
            StartCoroutine(BlurCoroutine(blurValue - 0.5f, false));
        }
        else if (blurValue <= 0)
        {
            Debug.Log("Blink effect completed, resetting sprite.");
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 1); // Reset transparency
            gameObject.GetComponent<SpriteRenderer>().enabled = false; // Disable the sprite after the blink effect
        }
    }
}
