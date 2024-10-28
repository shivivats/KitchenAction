using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO cutKitchenObjectSO;

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object here
            if (player.HasKitchenObject()) {
                // if the player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
        if (HasKitchenObject()) {
            // cut the kitchen object if there's one here
            GetKitchenObject().DestroySelf();
            
            // spawn the sliced object
            KitchenObject.SpawnKitchenObject(cutKitchenObjectSO, this);
            
        }
            
        
            
    }
}