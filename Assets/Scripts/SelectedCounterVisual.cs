using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour {

    [FormerlySerializedAs("clearCounter")] [SerializeField] private BaseCounter baseCounter;
    [SerializeField] private GameObject[] visualGameObjects;
    
    private void Start() {
        Player.Instance.OnSelectedCounterChanged += Player_OnOnSelectedCounterChanged;
    }

    private void Player_OnOnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e) {
        if (e.selectedCounter == baseCounter) {
            Show();
        }
        else {
            Hide();
        }
    }

    private void Show() {
        foreach (GameObject visualGameObject in visualGameObjects) {
            visualGameObject.SetActive(true);    
        }
        
    }

    private void Hide() {
        foreach (GameObject visualGameObject in visualGameObjects) {
            visualGameObject.SetActive(false);
        }
    }
}