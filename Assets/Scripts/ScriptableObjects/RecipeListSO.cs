using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// We commented this out after creating one RecipeListSO so we cannot create another!
// We only want one of these in our game by design!
//[CreateAssetMenu()]
public class RecipeListSO : ScriptableObject {
    public List<RecipeSO> recipeSOList;
}
