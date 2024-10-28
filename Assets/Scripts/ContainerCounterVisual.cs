using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    private Animator animator;

    private const string OPEN_CLOSE = "OpenClose";
    
    [SerializeField] private ContainerCounter containerCounter;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabedObject += ContainerCounter_OnOnPlayerGrabedObject;
    }

    private void ContainerCounter_OnOnPlayerGrabedObject(object sender, EventArgs e)
    {
        animator.SetTrigger(OPEN_CLOSE);
    }
}
