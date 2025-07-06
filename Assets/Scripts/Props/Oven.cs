
using UnityEngine;

public class Oven : PropGeneric
{
    bool cookChicken;
    public void Awake()
    {
        isActive = false; // Activo = abierto e inactivo = cerrado
        cookChicken = false;
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
        if (cookChicken && !isActive && chicken.GetComponent<Chicken>().frozenPercent <= 0)
        {
            chicken.GetComponent<Chicken>().cookingPercent += Time.deltaTime;
        }
    }
}
