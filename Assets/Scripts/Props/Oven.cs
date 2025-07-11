
using UnityEngine;

public class Oven : PropGeneric
{
    bool cookChicken, first;
    public GameObject lavadora;
    public AudioSource cook;
    public void Awake()
    {
        isOn = false; // Activo = abierto e inactivo = cerrado
        cookChicken = false;
        first = true;
    }
    public override void Action()
    {
        if (chicken.transform.parent == transform)
        {
            cookChicken = true;
        }
        else
        {
            cookChicken = false;
        }
    }
    private void Update()
    {
        if (cookChicken && !isOn && chicken.GetComponent<Chicken>().frozenPercent <= 0)
        {
            chicken.GetComponent<Chicken>().cookingPercent += Time.deltaTime/2;

            if (first)
            {
                first = false;
                lavadora.GetComponent<Washing>().iniciaContador();
            }
            if (!cook.isPlaying)
            {
                cook.Play();
            }
        }
        else
        {
            if (cook.isPlaying)
            {
                cook.Stop();
            }
        }
    }
}
