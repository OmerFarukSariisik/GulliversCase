using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenges._1._Basic_Progress_Bar.Scripts
{
    // You are NOT allowed to modify this script
    
    /// <summary>
    /// This interface exists to ensure that you implement these functions.
    /// </summary>
    public interface IProgressBar
    {
        void ForceValue(float value);
        void SetTargetValue(float value, float? speedOverride = null);
    }
    public class ProgressBarTester : MonoBehaviour
    {
        [SerializeField]
        private ProgressBar progressBar;

        private void Start()
        {
            if (typeof(ProgressBar).GetInterface(nameof(IProgressBar))==null)
            {
                Debug.LogError("ProgressBar does not implement the IProgressBar interface!");
            }
        }

        public void OnForceSetRandomClicked()
        {
            var randomValue = Random.value;
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
            var randomValue = Random.value;
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
