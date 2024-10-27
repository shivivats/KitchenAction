using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private bool isWalking;

    [SerializeField] private GameInput gameInput;
    
    private void Update()
    {
        gameInput.GetMovementVector();
    }

    public bool IsWalking()
    {
        return isWalking;
    }
}
