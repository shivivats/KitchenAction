using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {

    public event EventHandler<CuttingProgressEventArgs> OnCuttingProgress;

    public class CuttingProgressEventArgs : System.EventArgs {
        public float cuttingPercentage;
    }

    public event EventHandler OnCut;
    
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeSOArray;

    private int cuttingProgress;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object here
            if (player.HasKitchenObject()) {
                // if the player is carrying something
                if (HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player is carrying something that can be cut
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    OnCuttingProgress?.Invoke(this, new CuttingProgressEventArgs() {
                        cuttingPercentage = 0.0f
                    });
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
            
            CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            
            cuttingProgress++;
            
            OnCuttingProgress?.Invoke(this, new CuttingProgressEventArgs() {
                cuttingPercentage = (float)cuttingProgress / cuttingRecipeSO.cuttingAmountMax
            });
            
            OnCut?.Invoke(this, EventArgs.Empty); 
            
            if (cuttingProgress >= cuttingRecipeSO.cuttingAmountMax) {
                KitchenObjectSO outputKitchenObjectSO = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            
                GetKitchenObject().DestroySelf();
            
                // spawn the sliced object
                KitchenObject.SpawnKitchenObject(outputKitchenObjectSO, this);
                
                
            }
            
          
            
        }
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inKitchenObjectSO) {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeSOWithInput(inKitchenObjectSO);
        if (cuttingRecipeSO != null) {
            return cuttingRecipeSO.output;
        }
        return null;
        
    }

    private bool HasRecipeWithInput(KitchenObjectSO inKitchenObjectSO) {
        return GetCuttingRecipeSOWithInput(inKitchenObjectSO) != null;
    }

    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inKitchenObjectSO) {
        foreach (CuttingRecipeSO recipe in cuttingRecipeSOArray) {
            if (recipe.input == inKitchenObjectSO) {
                return recipe;
            }            
        }
        return null;
    }
}