using UnityEngine;

public class Defroster : PropGeneric
{
    bool defrostChicken;
    public AudioSource heat, beep;
    bool beepPlayed;
    public void Awake()
    {
        isActive = false; // Activo = abierto e inactivo = cerrado
        defrostChicken = false;
    }
    public override void Action()
    {
        if (chicken.transform.parent == transform)
        {
            defrostChicken = true;
        }
        else
        {
            defrostChicken = false;
        }
    }
    private void Update()
    {
        if (defrostChicken && !isActive && chicken.GetComponent<Chicken>().frozenPercent > 0)
        {
            chicken.GetComponent<Chicken>().frozenPercent -= Time.deltaTime;
        }
        //si está cerrado con el pollo dentro, se reproduce el sonido de calor

        if (!isActive && chicken.transform.parent == transform && chicken.GetComponent<Chicken>().frozenPercent > 0)
        {
            if (!heat.isPlaying)
            {
                heat.Play();
            }
        }
        else if (!isActive && chicken.transform.parent == transform && chicken.GetComponent<Chicken>().frozenPercent <= 0)
        {
            if (heat.isPlaying)
            {
                heat.Stop();
            }
            if (!beep.isPlaying && !beepPlayed)
            {
                beep.Play();
                beepPlayed = true;
            }
        }
        else if (!isActive && chicken.transform.parent != transform)
        {
            if (beep.isPlaying)
            {
                beep.Stop();
            }
        }
        else
        {
            if (heat.isPlaying)
            {
                heat.Stop();
            }
            if (beep.isPlaying)
            {
                beep.Stop();
            }
        }

        //isActive controla si el defrost está abierto o cerrado. true = abierto, false = cerrado
    }
}
