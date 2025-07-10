using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Fridge : PropGeneric
{
    bool freezeChicken;
    private bool lastActive = false;
    private bool playing;
    public float openTime;
    public AudioSource pitido;
    public void Awake()
    {
        isOn = false; // Activo = abierto e inactivo = cerrado
        freezeChicken = true;
        playing = false;
        openTime = 0f;
    }
    public override void Action()
    {
        if (chicken.transform.parent == transform)
        {
            freezeChicken = true;
        }
        else
        {
            freezeChicken = false;
        }
    }
    private void Update()
    {
        if (isOn)
        {
            if (!lastActive) openTime = 0f;
            openTime += Time.deltaTime;
            if (openTime >= 20f && !playing)
            {
                pitido.Play();
                playing = true;
            }
        }
        else
        {
            playing = false;
            pitido.Stop();
        }
        lastActive = isOn;

        if (freezeChicken && !isOn && chicken.GetComponent<Chicken>().frozenPercent < 50)
        {
            chicken.GetComponent<Chicken>().frozenPercent += Time.deltaTime;
        }
    }   
}