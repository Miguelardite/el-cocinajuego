using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class StartScreen : MonoBehaviour
{
    public TextMeshProUGUI spaceText;
    public float blinkSpeed = 1.5f;
    private float alpha = 1f;
    private bool fadingOut = true, started = false;
    public VideoPlayer videoPlayer;

    public string scene;

    void Update()
    {
        Blink();
        // Check for space key press to start the game
        if (Input.GetKeyDown(KeyCode.Space) && !started)
        {
            StartGame();
        }
    }
    void Blink()
    {
        float fadeAmount = Time.deltaTime / (blinkSpeed / 2f);

        Color currentColor = spaceText.color;

        if (fadingOut)
        {
            alpha -= fadeAmount;
            if (alpha <= 0f)
            {
                alpha = 0f;
                fadingOut = false;
            }
        }
        else
        {
            alpha += fadeAmount;
            if (alpha >= 1f)
            {
                alpha = 1f;
                fadingOut = true;
            }
        }

        currentColor.a = alpha;
        spaceText.color = currentColor;
    }
    void StartGame()
    {
        started = true;
        if (videoPlayer != null)
        {
            videoPlayer.Play();
            videoPlayer.loopPointReached += LoadScene;
        }
    }
    void LoadScene(VideoPlayer vp)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(scene);
    }
}
