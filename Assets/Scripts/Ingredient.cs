
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public Vector3 positionBurger;
    public BurgerManager burgerManager;

    private void OnMouseDown()
    {
        Debug.Log("Ingredient clicked: " + gameObject.name);

        // Rechercher BurgerManager si la référence est vide
        if (burgerManager == null)
        {
            burgerManager = FindFirstObjectByType<BurgerManager>();
        }

        // Vérifier que BurgerManager existe
        if (burgerManager == null)
        {
            Debug.LogWarning("BurgerManager introuvable !");
            return;
        }

        // Ne pas ajouter d'ingrédients si le jeu est terminé
        if (!burgerManager.isActiveAndEnabled)
        {
            return;
        }

        burgerManager.AddIngredient(this);
    }
}
