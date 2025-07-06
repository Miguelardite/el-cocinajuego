using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Fridge : PropGeneric
{
    bool freezeChicken;
    public void Awake()
    {
        isActive = false; // Activo = abierto e inactivo = cerrado
        freezeChicken = true;
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
        if (freezeChicken && !isActive && chicken.GetComponent<Chicken>().frozenPercent < 50)
        {
            chicken.GetComponent<Chicken>().frozenPercent += Time.deltaTime;
        }
    }   
}