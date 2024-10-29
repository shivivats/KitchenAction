using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object here
            if (player.HasKitchenObject()) {
                // if the player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player is carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                }
            }
            else {
                // the player is not carrying anything
            }
        }
        else {
            // there is a kitchen object here
            if (player.HasKitchenObject()) {
                // player is carrying something
            }
            else {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }

    public override void InteractAlternate(Player player) {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO())) {
            // cut the kitchen object if there's one here and that can be cut
            
            KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
            GetKitchenObject().DestroySelf();
            
            // spawn the sliced object
            KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
            
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inKitchenObjectSO) {
        foreach (CuttingRecipeSO recipe in cuttingRecipeSOArray) {
            if (recipe.input == inKitchenObjectSO) {
                return recipe.output;
            }            
        }
        return null;
    }

    private bool HasRecipeWithInput(KitchenObjectSO inKitchenObjectSO) {
        foreach (CuttingRecipeSO recipe in cuttingRecipeSOArray) {
            if (recipe.input == inKitchenObjectSO) {
                return true;
            }            
        }
        return false;
    }
}