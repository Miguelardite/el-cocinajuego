using UnityEngine;

public class libro : MonoBehaviour
{
    public GameObject postIt;

    public void firstPressed()
    {
        postIt.GetComponent<toDoList>().iniciaTarea("-Defrost in the microwave");
    }

    public void secondPressed()
    {
        postIt.GetComponent<toDoList>().iniciaTarea("-Season with 15g of spices");
    }

    public void thirdPressed()
    {
        postIt.GetComponent<toDoList>().iniciaTarea("-Put it in the oven for 7 min");
    }
}
