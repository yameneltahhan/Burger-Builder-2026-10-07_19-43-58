using UnityEngine;
using System.Collections.Generic;

public class BurgerManager : MonoBehaviour
{
    public List<Ingredient> ingredients = new List<Ingredient>();

    public void AddIngredient(Ingredient ingredient)
    {
        // Ajouter l'ingrédient à la liste
        ingredients.Add(ingredient);

        // Calculer sa hauteur
        float y = -3f + (ingredients.Count * 0.2f);

        // Déplacer l'ingrédient
        ingredient.transform.position = new Vector3(0, y, 0);

        // Mettre le nouvel ingrédient devant les précédents
        SpriteRenderer sprite = ingredient.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = ingredients.Count;

        Debug.Log("Ingredient added: " + ingredient.name);
    }
}