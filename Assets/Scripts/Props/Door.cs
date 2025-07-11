using UnityEngine;

public class Door : MonoBehaviour
{
    bool isOpen = false;
    public Sprite openSprite;
    public Sprite closeSprite;
    public AudioSource doorSound;

    private void Awake()
    {
        isOpen = false;
    }
    public void OnMouseDown()
    {
        isOpen = !isOpen;
        if (isOpen)
        {
            GetComponent<SpriteRenderer>().sprite = openSprite;
            doorSound.Play();
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = closeSprite;
            doorSound.Play();
        }
    }
}
