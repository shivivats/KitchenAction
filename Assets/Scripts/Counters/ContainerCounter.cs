using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter, IKitchenObjectParent {
    public event EventHandler OnPlayerGrabedObject;

    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public override void Interact(Player player) {
        if (!player.HasKitchenObject()) {
            // spawn an object and give it to the player
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);

            OnPlayerGrabedObject?.Invoke(this, EventArgs.Empty);
        }
    }
}