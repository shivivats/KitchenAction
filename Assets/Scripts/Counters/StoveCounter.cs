using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress {

    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;

    public class OnStateChangedEventArgs : EventArgs {
        public State state;
    }

    public enum State {
        Idle,
        Frying,
        Fried,
        Burned
    }
    
    [SerializeField] private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField] private BurningRecipeSO[] burningRecipeSOArray;

    private State state;
    private float fryingTimer;
    private FryingRecipeSO fryingRecipeSO;
    private float burningTimer;
    private BurningRecipeSO burningRecipeSO;
    
    
    private void Start() {
        state = State.Idle;
    }

    private void Update() {
        if (HasKitchenObject()) {
            switch (state) {
                case State.Idle:
                    break;
                case State.Frying:
                    fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });

                    if (fryingTimer > fryingRecipeSO.fryingTimerMax) {
                        // Frying complete

                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(fryingRecipeSO.output, this);

                        state = State.Fried;
                        burningTimer = 0f;
                        burningRecipeSO = GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() {
                            state = state
                        });
                    }

                    break;
                case State.Fried:
                    burningTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                        progressNormalized = burningTimer / burningRecipeSO.burningTimerMax
                    });

                    if (burningTimer > burningRecipeSO.burningTimerMax) {
                        // Burned

                        GetKitchenObject().DestroySelf();

                        KitchenObject.SpawnKitchenObject(burningRecipeSO.output, this);

                        state = State.Burned;

                        OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() {
                            state = state
                        });

                        OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                            progressNormalized = 0f
                        });

                    }

                    break;
                case State.Burned:
                    break;
            }
        }
    }

    public override void Interact(Player player) {
        if (!HasKitchenObject()) {
            // there is no kitchen object here
            if (player.HasKitchenObject()) {
                // if the player is carrying something
                if (HasFryingRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    // player is carrying something that can be fried
                    player.GetKitchenObject().SetKitchenObjectParent(this);

                    // store the current frying recipe
                    fryingRecipeSO = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

                    state = State.Frying;
                    fryingTimer = 0f;
                    
                    OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() {
                        state = state
                    });
                    
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                        progressNormalized = fryingTimer / fryingRecipeSO.fryingTimerMax
                    });
                }
            } else {
                // the player is not carrying anything
            }
        } else {
            // there is a kitchen object here
            if (player.HasKitchenObject()) {
                // player is carrying something
            } else {
                // player is not carrying anything
                GetKitchenObject().SetKitchenObjectParent(player);

                // reset state
                state = State.Idle;
                
                OnStateChanged?.Invoke(this, new OnStateChangedEventArgs() {
                    state = state
                });
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs() {
                    progressNormalized = 0f
                });
                
            }
        }
    }
    
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inKitchenObjectSO) {
        FryingRecipeSO fryingRecipeSO = GetFryingRecipeSOWithInput(inKitchenObjectSO);
        if (fryingRecipeSO != null) {
            return fryingRecipeSO.output;
        }

        return null;
    }

    private bool HasFryingRecipeWithInput(KitchenObjectSO inKitchenObjectSO) {
        return GetFryingRecipeSOWithInput(inKitchenObjectSO) != null;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO inKitchenObjectSO) {
        foreach (FryingRecipeSO recipe in fryingRecipeSOArray) {
            if (recipe.input == inKitchenObjectSO) {
                return recipe;
            }
        }

        return null;
    }
    
    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inputKitchenObjectSO) {
        foreach (BurningRecipeSO burningRecipeSO in burningRecipeSOArray) {
            if (burningRecipeSO.input == inputKitchenObjectSO) {
                return burningRecipeSO;
            }
        }
        return null;
    }
}
