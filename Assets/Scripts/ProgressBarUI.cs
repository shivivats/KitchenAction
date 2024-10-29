using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour {
   [SerializeField] private CuttingCounter cuttingCounter;
   [SerializeField] private Image bar;

   private void Start() {
      cuttingCounter.OnCuttingProgress += CuttingCounter_OnCuttingProgress;

      bar.fillAmount = 0f;
      Hide();
   }

   private void CuttingCounter_OnCuttingProgress(object sender, CuttingCounter.CuttingProgressEventArgs e) {
      
      bar.fillAmount = e.cuttingPercentage;

      if (e.cuttingPercentage == 0f || e.cuttingPercentage == 1f) {
         Hide();
      } else {
         Show();
      }
   }

   private void Show() {
      gameObject.SetActive(true);
   }

   private void Hide() {
      gameObject.SetActive(false);
   }

   
}
