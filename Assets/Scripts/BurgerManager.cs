
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using System.Collections;

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

    [Header("flash de machine")]
    public FlashCommande flashCommande;

    public int totalEtoiles = 0;

    public bool partieTerminee = false;
    private int indexIngredient = 0;
    private bool animationEnCours = false;

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

        if (flashCommande != null)
        {
            flashCommande.FlashRouge();
        }

        if (audioManager != null)
        {
            audioManager.JouerSonMauvaiseReponse();
        }

        // Faire disparaître le burger avec animation
        StartCoroutine(ErreurIngredientAvecAnimation());

        return false;
    }

    IEnumerator AnimationDisparitionBurger()
    {
        float duree = 0.3f;
        float temps = 0f;

        // Sauvegarder les tailles de départ
        List<Vector3> taillesInitiales = new List<Vector3>();

        foreach (Ingredient ingredient in ingredients)
        {
            taillesInitiales.Add(ingredient.transform.localScale);
        }

        // Réduire progressivement tous les ingrédients
        while (temps < duree)
        {
            temps += Time.deltaTime;

            float progression = Mathf.Clamp01(temps / duree);

            for (int i = 0; i < ingredients.Count; i++)
            {
                if (ingredients[i] != null)
                {
                    ingredients[i].transform.localScale =
                        Vector3.Lerp(
                            taillesInitiales[i],
                            Vector3.zero,
                            progression
                        );
                }
            }

            yield return null;
        }

        // Supprimer le burger après l'animation
        ViderBurger();
    }

    IEnumerator TerminerCommandeAvecAnimation()
    {
        animationEnCours = true;

        yield return StartCoroutine(AnimationDisparitionBurger());

        commandeClient.GenererCommande();
        indexIngredient = 0;
        ReinitialiserFleche();

        animationEnCours = false;
    }

    IEnumerator ErreurIngredientAvecAnimation()
    {
        // Bloquer les clics pendant l'animation
        animationEnCours = true;

        // Attendre la disparition du burger
        yield return StartCoroutine(AnimationDisparitionBurger());

        // Recommencer la même commande depuis le début
        indexIngredient = 0;

        // Remettre la flèche au début
        ReinitialiserFleche();

        // Autoriser à nouveau les clics
        animationEnCours = false;
    }

    // Méthode principale appelée par les boutons
    public void ChoisirIngredient(Ingredient ingredientChoisi)
    {
        if (partieTerminee || animationEnCours)
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

        if(audioManager != null)
        {
            if (ingredientChoisi == painbas || ingredientChoisi == painhaut)
            {
                audioManager.JouerSonIngredient("Pain");
            }
            else if (ingredientChoisi == salade)
            {
                audioManager.JouerSonIngredient("Salade");
            }
            else if (ingredientChoisi == tomate)
            {
                audioManager.JouerSonIngredient("Tomate");
            }
            else if (ingredientChoisi == fromage)
            {
                audioManager.JouerSonIngredient("Fromage");
            }
            else if (ingredientChoisi == viande)
            {
                audioManager.JouerSonIngredient("Viande");
            }
            else if (ingredientChoisi == poisson)
            {
                audioManager.JouerSonIngredient("Poisson");
            }
            else if (ingredientChoisi == oignon)
            {
                audioManager.JouerSonIngredient("Oignon");
            }
            else if (ingredientChoisi == ketchup || ingredientChoisi == mayonnaise)
            {
                audioManager.JouerSonIngredient("SauceBurger");
            }
        }

        // Passer à l'ingrédient suivant
        indexIngredient++;

        // Vérifier si la commande est terminée
        if (indexIngredient >= commandeClient.ingredients.Count)
        {
            Debug.Log("Commande terminée avec succès !");
            if (flashCommande != null)
            {
                flashCommande.FlashVert();
            }
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

            // Lancer la coroutine pour terminer la commande avec animation
            StartCoroutine(TerminerCommandeAvecAnimation());
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
        float y = -1.9f + (ingredients.Count * 0.2f);

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
