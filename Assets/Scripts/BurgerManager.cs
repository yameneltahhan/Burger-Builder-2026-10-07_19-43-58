using UnityEngine;
using System.Collections.Generic;

public class BurgerManager : MonoBehaviour
{
    public List<Ingredient> ingredients = new List<Ingredient>();
    public Ingredient viande;
    public Ingredient fromage;
    public Ingredient salade;
    public Ingredient oignon;
    public Ingredient tomate;
    public Ingredient painbas;
    public Ingredient painhaut;
    public Ingredient ketchup;
    public Ingredient mayonnaise;
    public Ingredient poisson;

    public void AddIngredient(Ingredient ingredient)
    {
        // Ajouter l'ingrédient à la liste
        ingredients.Add(ingredient);

        // Calculer sa hauteur
        float y = -3f + (ingredients.Count * 0.2f);

        // Déplacer l'ingrédient
        ingredient.transform.position = new Vector3(-5f, y, 0);
        // Agrandir l'ingrédient à 150 %
        ingredient.transform.localScale = Vector3.one * 4.5f;

        // Mettre le nouvel ingrédient devant les précédents
        SpriteRenderer sprite = ingredient.GetComponent<SpriteRenderer>();
        sprite.sortingOrder = ingredients.Count;

        Debug.Log("Ingredient added: " + ingredient.name);
    }

    public void AjouterViande()
    {
        Ingredient nouvelleViande = Instantiate(viande);
        AddIngredient(nouvelleViande);
    }

    public void AjouterFromage()
    {
        Ingredient nouveauFromage = Instantiate(fromage);
        AddIngredient(nouveauFromage);
    }

    public void AjouterSalade()
    {
        Ingredient nouvelleSalade = Instantiate(salade);
        AddIngredient(nouvelleSalade);
    }

    public void AjouterOignon()
    {
        Ingredient nouveauOignon = Instantiate(oignon);
        AddIngredient(nouveauOignon);
    }

    public void AjouterTomate()
    {
        Ingredient nouvelleTomate = Instantiate(tomate);
        AddIngredient(nouvelleTomate);
    }

    public void AjouterPainBas()
    {
        Ingredient nouveauPainBas = Instantiate(painbas);
        AddIngredient(nouveauPainBas);
    }

    public void AjouterPainHaut()
    {
        Ingredient nouveauPainHaut = Instantiate(painhaut);
        AddIngredient(nouveauPainHaut);
    }

    public void AjouterKetchup()
    {
        Ingredient nouveauKetchup = Instantiate(ketchup);
        AddIngredient(nouveauKetchup);
    }

    public void AjouterMayonnaise()
    {
        Ingredient nouvelleMayonnaise = Instantiate(mayonnaise);
        AddIngredient(nouvelleMayonnaise);
    }

    public void AjouterPoisson()
    {
        Ingredient nouveauPoisson = Instantiate(poisson);
        AddIngredient(nouveauPoisson);
    }
}