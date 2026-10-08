
using UnityEngine;
using System.Collections.Generic;

public class CommandeClient : MonoBehaviour
{
    [Header("Liste de la commande")]
    public List<Ingredient> ingredients = new List<Ingredient>();

    [Header("Prefabs des ingrédients")]
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

    [Header("Les 7 emplacements du panneau")]
    public SpriteRenderer[] emplacements = new SpriteRenderer[7];

    [Header("Position et taille")]
    public float positionX = -0.6f;
    public float premierY = 0.9f;
    public float espacementY = 0.4f;
    public float taille = 0.6f;


    public Transform flecheCommande;

    void Start()
    {
        GenererCommande();
    }

    public void GenererCommande()
    {
        // Effacer l'ancienne commande
        ingredients.Clear();

        // Choisir entre 2 et 5 garnitures
        int nombreIngredients = Random.Range(2, 6);

        // Ajouter le pain du bas
        ingredients.Add(painbas);

        // Ajouter les garnitures aléatoires
        for (int i = 0; i < nombreIngredients; i++)
        {
            int ingredientAleatoire = Random.Range(0, 8);

            switch (ingredientAleatoire)
            {
                case 0:
                    ingredients.Add(viande);
                    break;
                case 1:
                    ingredients.Add(fromage);
                    break;
                case 2:
                    ingredients.Add(salade);
                    break;
                case 3:
                    ingredients.Add(oignon);
                    break;
                case 4:
                    ingredients.Add(tomate);
                    break;
                case 5:
                    ingredients.Add(ketchup);
                    break;
                case 6:
                    ingredients.Add(mayonnaise);
                    break;
                case 7:
                    ingredients.Add(poisson);
                    break;
            }
        }

        // Ajouter le pain du haut
        ingredients.Add(painhaut);

        // Afficher la commande dans le panneau
        AfficherCommande();

        Debug.Log("Commande générée : "
                  + ingredients.Count + " ingrédients");
    }

    void AfficherCommande()
    {
        // Parcourir les 7 emplacements
        for (int i = 0; i < emplacements.Length; i++)
        {
            SpriteRenderer emplacement = emplacements[i];

            if (emplacement == null)
                continue;

            // Cacher les emplacements inutilisés
            if (i >= ingredients.Count)
            {
                emplacement.enabled = false;
                continue;
            }

            Ingredient ingredient = ingredients[i];

            if (ingredient == null)
            {
                Debug.LogWarning("Ingrédient non assigné !");
                emplacement.enabled = false;
                continue;
            }

            // Récupérer le sprite du prefab
            SpriteRenderer spriteIngredient =
                ingredient.GetComponent<SpriteRenderer>();

            if (spriteIngredient == null)
            {
                Debug.LogWarning(
                    "SpriteRenderer manquant : " + ingredient.name
                );
                emplacement.enabled = false;
                continue;
            }

            // Afficher le bon ingrédient
            emplacement.sprite = spriteIngredient.sprite;
            emplacement.enabled = true;

            // Calculer la position
            float y = premierY + i * espacementY;

            emplacement.transform.localPosition =
               new Vector3(positionX, y, 0f);

            // Régler la taille
            emplacement.transform.localScale =
                new Vector3(taille, taille, 1f);

            // Afficher devant le panneau
            emplacement.sortingOrder = 20 + i;
        }
    }
    public void AvancerFleche()
    {
        flecheCommande.localPosition += new Vector3(0f, 0.4f, 0f);
    }
}
