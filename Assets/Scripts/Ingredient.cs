using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Vector3 positionBurger;
    public BurgerManager burgerManager;

    private void OnMouseDown()
    {
        Debug.Log("Ingredient clicked: ");
        
        burgerManager.AddIngredient(this);
    }
}