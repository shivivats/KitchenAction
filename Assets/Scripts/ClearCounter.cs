using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearCounter : BaseCounter {
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

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
}