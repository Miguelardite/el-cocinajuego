using UnityEngine;

public class Defroster : PropGeneric
{
    bool defrostChicken;
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
    }   
}
