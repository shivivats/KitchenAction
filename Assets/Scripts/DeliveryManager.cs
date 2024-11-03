using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour {
    
    public static DeliveryManager Instance { get; private set; }

    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    
    // We could have also referenced a list of all the recipes here directly
    // But this approach has the advantage of if we need another object to refer to all the recipes then we dont need to modify them one by one but rather we can just modify this RecipeListSO
    [SerializeField] private RecipeListSO recipeListSO;
    
    private List<RecipeSO> waitingRecipeSOList;
    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax = 4f;
    private int waitingRecipesMax = 4;

    private void Awake() {
        Instance = this;
        waitingRecipeSOList = new List<RecipeSO>();
    }

    private void Update() {
        spawnRecipeTimer -= Time.deltaTime;
        if (spawnRecipeTimer < 0f) {
            spawnRecipeTimer = spawnRecipeTimerMax;

            if (waitingRecipeSOList.Count < waitingRecipesMax) {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingRecipeSOList.Add(waitingRecipeSO);
                
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject) {
        foreach (RecipeSO waitingRecipeSO in waitingRecipeSOList) {
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count) {
                // same number of ingredients
                bool plateContentsMatchesRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList) {
                    // cycling through all the ingredients in the recipe
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
                        // cycling through all the ingredients on the plate
                        if (plateKitchenObjectSO == recipeKitchenObjectSO) {
                            // ingredient matches!
                            ingredientFound = true;
                            break;
                        }
                    }

                    if (!ingredientFound) {
                        // one ingredient from the recipe is not found in the plate!
                        plateContentsMatchesRecipe = false;
                    }
                }

                if (plateContentsMatchesRecipe) {
                    // Player delivered the correct recipe
                    Debug.Log("Player delivered the correct recipe!");
                    waitingRecipeSOList.Remove(waitingRecipeSO);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
        }
        
        // No matches found!
        // Player did not deliver a correct recipe
        Debug.Log("Player did not deliver a correct recipe!");
    }

    public List<RecipeSO> GetWaitingRecipeSOList() {
        return waitingRecipeSOList;
    }
    
}
