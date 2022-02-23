using Challenges._1._Basic_Progress_Bar.Scripts;
using System.Collections;
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
            tester.SetCurrentMinAndMax(initialThresholds[0], initialThresholds[initialThresholds.Length - 1]);
        }

        #region Editable Area

        [SerializeField]
        ComplexProgressBarTester tester;

        private int currentValue;
        private int currentThresholdIndex;
        private bool trigger = false;
        private bool isCalculating = false;

        public void SetThresholds(int[] thresholds)
        {
            initialThresholds = thresholds;
            previousThresholdText.text = thresholds[0].ToString();
            nextThresholdText.text = thresholds[1].ToString();
            basicProgressBar.ForceValue(0f);
            currentValue = thresholds[0];
            currentThresholdIndex = 0;
        }

        public void ForceValue(int value)
        {
            if (value == currentValue || isCalculating)
                return;

            Debug.Log("Value: " + value);

            int newIndex = GetThresholdIndex(value);
            float percent = (float)(value - initialThresholds[newIndex]) / (initialThresholds[newIndex + 1] - initialThresholds[newIndex]);

            if (value > currentValue)
            {
                StartCoroutine(Loop(newIndex - currentThresholdIndex, percent, true, true));
            }
            else
            {
                StartCoroutine(Loop(currentThresholdIndex - newIndex, percent, false, true));
            }

            currentValue = value;

        }

        public void SetTargetValue(int value, float? speedOverride = null)
        {
            
            if (value == currentValue || isCalculating)
                return;

            Debug.Log("Value: " + value);

            int newIndex = GetThresholdIndex(value);
            float percent = (float)(value - initialThresholds[newIndex]) / (initialThresholds[newIndex + 1] - initialThresholds[newIndex]);
            
            if (value > currentValue)
            {
                StartCoroutine(Loop(newIndex - currentThresholdIndex, percent, true, false, speedOverride));
            }
            else
            {
                StartCoroutine(Loop(currentThresholdIndex - newIndex, percent, false, false, speedOverride));
            }

            currentValue = value;
        }

        private int GetThresholdIndex(int value)
        {
            int index = -1;

            foreach (int threshold in initialThresholds)
            {
                if (value >= threshold)
                    index++;
                else
                    break;
            }

            return index;
        }

        private IEnumerator Loop(int loopCount, float percent, bool isIncrease, bool force, float? speedOverride = null)
        {
            isCalculating = true;

            while (loopCount > 0)
            {
                if (force)
                    basicProgressBar.ForceValue(isIncrease ? 1f : 0f, true);
                else
                    basicProgressBar.SetTargetValue(isIncrease ? 1f : 0f, speedOverride);

                while (!trigger)
                    yield return null;

                trigger = false;
                basicProgressBar.ForceValue(isIncrease ? 0f : 1f);
                UpdateThresholds(isIncrease);
                loopCount--;
            }

            if (force)
                basicProgressBar.ForceValue(percent, true);
            else
                basicProgressBar.SetTargetValue(percent, speedOverride);

            while (!trigger)
                yield return null;

            trigger = false;
            isCalculating = false;
        }

        private void UpdateThresholds(bool isIncrease)
        {
            if (isIncrease)
                currentThresholdIndex++;
            else
                currentThresholdIndex--;

            previousThresholdText.text = initialThresholds[currentThresholdIndex].ToString();
            nextThresholdText.text = initialThresholds[currentThresholdIndex + 1].ToString();
        }

        public void TriggerOnBarComplete()
        {
            trigger = true;
        }

        #endregion
    }
}