using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateIconsUI : MonoBehaviour {
   [SerializeField] private PlateKitchenObject plateKitchenObject;
   [SerializeField] private Transform iconTemplate;

   private void Awake() {
      iconTemplate.gameObject.SetActive(false);
   }

   private void Start() {
      plateKitchenObject.OnIngredientAdded += PlateKitchenObject_OnIngredientAdded;
   }

   private void PlateKitchenObject_OnIngredientAdded(object sender, PlateKitchenObject.OnIngredientAddedEventArgs e) {
      // update the icons in this display
      UpdateVisual();
   }

   private void UpdateVisual() {
      // destroy all the current children (except the template) before spawning the updated ones
      foreach (Transform child in transform) {
         if (child == iconTemplate) {
            continue;
         }
         Destroy(child.gameObject);
      }
      foreach (KitchenObjectSO kitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList()) {
         Transform iconTransform = Instantiate(iconTemplate, transform);
         iconTransform.gameObject.SetActive(true);
         iconTransform.GetComponent<PlateIconsSingleUI>().SetKitchenObjectSO(kitchenObjectSO);
      }
   }
}
