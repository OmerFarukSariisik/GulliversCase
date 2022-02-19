using Challenges._1._Basic_Progress_Bar.Scripts;
using TMPro;
using UnityEngine;

namespace Challenges._5._Complex_Loading_Bar.Scripts
{
    /// <summary>
    /// Uses the basic progress bar to provide an interface of a loading bar with inherent thresholds.
    /// You can imagine this like a player level bar, say your experience thresholds are [0,150,400,1500,8000]
    /// If you jump from 90XP to 1800XP, you would expect the progress bar to loop multiple times until it reaches the desired percentage
    ///
    /// The previous and next threshold texts should be update depending on where the progress currently is,
    /// if the progress bar needs to loop several times, the threshold text should be updated as it passes through each threshold.
    ///
    /// </summary>
    public class LoopableProgressBar : MonoBehaviour, ILoopableProgressBar
    {
        [SerializeField] private ProgressBar basicProgressBar;
        [SerializeField] private int[] initialThresholds;
        [SerializeField] private TMP_Text previousThresholdText;
        [SerializeField] private TMP_Text nextThresholdText;

        private void Start()
        {
            if(basicProgressBar==null) Debug.LogError("Basic progress bar is missing");
            if(previousThresholdText==null) Debug.LogError("Previous Threshold Text is missing");
            if(nextThresholdText==null) Debug.LogError("Next Threshold Text is missing");
            //Fallback
            if (initialThresholds.Length < 2)
            {
                Debug.LogWarning("Initial threshold size was less than 2, replacing it with [0,10]");
                initialThresholds = new int[] {0, 10};
            }
            SetThresholds(initialThresholds);
            ForceValue(initialThresholds[0]);
        }

        #region Editable Area

        public void SetThresholds(int[] thresholds)
        {
        }

        public void ForceValue(int value)
        {
        }

        public void SetTargetValue(int value, float? speedOverride = null)
        {
        }

        #endregion
    }
}