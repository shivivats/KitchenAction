using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour {
    
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Awake() {
        iconTemplate.gameObject.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSO) {
        recipeNameText.text = recipeSO.name;

        // Clean up the container
        foreach (Transform child in iconContainer) {
            if (child != iconTemplate) {
                Destroy(child.gameObject);
            }
        }

        // spawn icons for ingredients
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList) {
            Transform iconTransform = Instantiate(iconTemplate, iconContainer);
            iconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;
            iconTransform.gameObject.SetActive(true);
        }
    }
    
    
}
