using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenges._5._Complex_Loading_Bar.Scripts
{
    public interface ILoopableProgressBar
    {
        void SetThresholds(int[] thresholds);
        void ForceValue(int value);
        void SetTargetValue(int value, float? speedOverride = null);
    }
    public class ComplexProgressBarTester : MonoBehaviour
    {
        [SerializeField]
        private LoopableProgressBar progressBar;

        private void Start()
        {
            if (typeof(LoopableProgressBar).GetInterface(nameof(ILoopableProgressBar))==null)
            {
                Debug.LogError("ProgressBar does not implement the IProgressBar interface!");
            }

        }

        private int _currentMinimum;
        private int _currentMaximum;

        public void RandomizeThreshold()
        {
            List<int> thresholds = new List<int>();
            int thresholdAmount = Random.Range(4, 10);
            for (int i = 0; i < thresholdAmount; i++)
            {
                var last = thresholds.Count > 0 ? thresholds[thresholds.Count - 1] : 0;
                var val = 10*Random.Range(1, 10);
                thresholds.Add(last+val);
            }

            _currentMinimum = thresholds[0];
            _currentMaximum = thresholds[thresholds.Count - 1];
            progressBar.SetThresholds(thresholds.ToArray());

            var st = "Thresholds: [ "+string.Join("--", thresholds)+" ]";
            Debug.Log(st);
        }

        public void OnForceSetRandomClicked()
        {
            var randomValue = Random.Range(_currentMinimum, _currentMaximum+1);
            try
            {
                progressBar.ForceValue(randomValue);
            }
            catch (Exception e)
            {
                Debug.LogError("An exception was thrown at ForceValue");
                Debug.LogException(e);
            }
        }
        public void OnSetRandomClicked()
        {
            var randomValue =  Random.Range(_currentMinimum, _currentMaximum+1);
            try
            {
                progressBar.SetTargetValue(randomValue);
            }
            catch (Exception e)
            {
                Debug.LogError("An exception was thrown at SetTargetValue");
                Debug.LogException(e);
            }
        }
    }
}
