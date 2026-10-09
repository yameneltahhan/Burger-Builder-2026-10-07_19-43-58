
using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class BurgerManager : MonoBehaviour
{
    [Header("Ingrédients du joueur")]
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

    [Header("Commande client")]
    public CommandeClient commandeClient;

    [Header("Gestion des sons")]
    public AudioManager audioManager;

    [Header("Score")]
    public TMP_Text texteEtoiles;

    public int totalEtoiles = 0;

    public bool partieTerminee = false;
    private int indexIngredient = 0;

    void Start()
    {
        // Afficher le score initial
        AfficherScore();
    }

    // Calculer les étoiles selon la taille de la commande
    public int CalculerEtoiles(int nombreIngredients)
    {
        if (nombreIngredients <= 4)
        {
            return 1;
        }
        else if (nombreIngredients <= 6)
        {
            return 2;
        }
        else
        {
            return 3;
        }
    }

    // Mettre à jour le texte du score
    public void AfficherScore()
    {
        if (texteEtoiles != null)
        {
            texteEtoiles.text = "          "+ totalEtoiles;
        }
    }

    // Vérifier l'ingrédient choisi
    public bool VerifierIngredient(Ingredient ingredientChoisi)
    {
        if (commandeClient == null ||
            commandeClient.ingredients.Count == 0 ||
            indexIngredient >= commandeClient.ingredients.Count)
        {
            Debug.LogWarning("Commande non disponible !");
            return false;
        }

        Ingredient ingredientAttendu =
            commandeClient.ingredients[indexIngredient];

        if (ingredientChoisi == ingredientAttendu)
        {
            Debug.Log("Bon ingrédient : " + ingredientChoisi.name);
            return true;
        }

        Debug.Log("Mauvais ingrédient !");

        if (audioManager != null)
        {
            audioManager.JouerSonMauvaiseReponse();
        }

        // Supprimer le burger
        ViderBurger();

        // Recommencer la même commande
        indexIngredient = 0;

        // Remettre la flèche au début
        ReinitialiserFleche();

        return false;
    }

    // Méthode principale appelée par les boutons
    public void ChoisirIngredient(Ingredient ingredientChoisi)
    {
        if (partieTerminee)
        {
            return;
        }
        // Vérifier avant de créer l'ingrédient
        if (!VerifierIngredient(ingredientChoisi))
        {
            return;
        }

        // Créer le bon ingrédient
        Ingredient nouvelIngredient = Instantiate(ingredientChoisi);

        // L'ajouter au burger
        AddIngredient(nouvelIngredient);

        // Passer à l'ingrédient suivant
        indexIngredient++;

        // Vérifier si la commande est terminée
        if (indexIngredient >= commandeClient.ingredients.Count)
        {
            Debug.Log("Commande terminée avec succès !");
            // Jouer le son de réussite
            if (audioManager != null)
            {
                audioManager.JouerSonBonneReponse();
            }
            // Calculer les étoiles gagnées
            int etoilesGagnees =
                CalculerEtoiles(commandeClient.ingredients.Count);

            // Ajouter les étoiles au score total
            totalEtoiles += etoilesGagnees;

            // Actualiser l'affichage
            AfficherScore();

            Debug.Log("Étoiles gagnées : " + etoilesGagnees);
            Debug.Log("Total étoiles : " + totalEtoiles);

            // Supprimer le burger terminé
            ViderBurger();

            // Générer une nouvelle commande
            commandeClient.GenererCommande();

            // Recommencer au premier ingrédient
            indexIngredient = 0;

            // Remettre la flèche au début
            ReinitialiserFleche();
        }
        else
        {
            // Faire monter la flèche
            commandeClient.AvancerFleche();
        }
    }

    // Afficher un ingrédient sur le plateau
    public void AddIngredient(Ingredient ingredient)
    {
        ingredients.Add(ingredient);

        // Calculer la hauteur
        float y = -3f + (ingredients.Count * 0.2f);

        // Position du burger
        ingredient.transform.position =
            new Vector3(-5f, y, 0f);

        // Taille des ingrédients
        ingredient.transform.localScale =
            Vector3.one * 4.5f;

        // Ordre d'affichage
        SpriteRenderer sprite =
            ingredient.GetComponent<SpriteRenderer>();

        if (sprite != null)
        {
            sprite.sortingOrder = ingredients.Count;
        }

        Debug.Log("Ingrédient ajouté : " + ingredient.name);
    }

    // Supprimer tous les ingrédients du burger
    public void ViderBurger()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            if (ingredient != null)
            {
                Destroy(ingredient.gameObject);
            }
        }

        ingredients.Clear();
    }

    // Remettre la flèche à sa position initiale
    public void ReinitialiserFleche()
    {
        if (commandeClient != null &&
            commandeClient.flecheCommande != null)
        {
            Vector3 position =
                commandeClient.flecheCommande.localPosition;

            position.y = 0.85f;

            commandeClient.flecheCommande.localPosition =
                position;
        }
    }

    // Fonctions des boutons d'ingrédients

    public void AjouterViande()
    {
        ChoisirIngredient(viande);
    }

    public void AjouterFromage()
    {
        ChoisirIngredient(fromage);
    }

    public void AjouterSalade()
    {
        ChoisirIngredient(salade);
    }

    public void AjouterOignon()
    {
        ChoisirIngredient(oignon);
    }

    public void AjouterTomate()
    {
        ChoisirIngredient(tomate);
    }

    public void AjouterPainBas()
    {
        ChoisirIngredient(painbas);
    }

    public void AjouterPainHaut()
    {
        ChoisirIngredient(painhaut);
    }

    public void AjouterKetchup()
    {
        ChoisirIngredient(ketchup);
    }

    public void AjouterMayonnaise()
    {
        ChoisirIngredient(mayonnaise);
    }

    public void AjouterPoisson()
    {
        ChoisirIngredient(poisson);
    }
}
