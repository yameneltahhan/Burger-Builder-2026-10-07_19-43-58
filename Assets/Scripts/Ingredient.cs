using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Vector3 positionBurger;

    private void OnMouseDown()
    {
        Debug.Log("Ingredient clicked: ");
        transform.position = positionBurger;
    }
}