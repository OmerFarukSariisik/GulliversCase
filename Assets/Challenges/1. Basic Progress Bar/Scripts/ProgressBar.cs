using Challenges._5._Complex_Loading_Bar.Scripts;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Challenges._1._Basic_Progress_Bar.Scripts
{
    /// <summary>
    /// Edit this script for the ProgressBar challenge.
    /// </summary>
    public class ProgressBar : MonoBehaviour, IProgressBar
    {
        /// <summary>
        /// You can add more options
        /// </summary>
        private enum ProgressSnapOptions
        {
            SnapToLowerValue,
            SnapToHigherValue,
            DontSnap
        }
        
        /// <summary>
        /// You can add more options
        /// </summary>
        private enum TextPosition
        {
            BarCenter,
            Progress,
            NoText
        }

        private enum SpeedType
        {
            Regular,
            Accelerated
        }

        /// <summary>
        /// These settings below must function
        /// </summary>
        [Header("Options")]
        [SerializeField]
        [Range(1,20)]
        private float baseSpeed;
        [SerializeField]
        private ProgressSnapOptions snapOptions;
        [SerializeField]
        private TextPosition textPosition;

        [Header("MyFields")]
        [SerializeField]
        private SpeedType speedType;
        
        [SerializeField]
        private RectTransform fillBar;
        [SerializeField]
        private RectTransform percentage;
        private TextMeshProUGUI percentageText;

        private TextPosition oldTextPosition;

        #region BarPercentageCalculation
        private float barHorizontalOffset;
        private float barWidth;

        private float lastValue = 0f;
        private bool isCalculating = false;
        #endregion

        [SerializeField] private LoopableProgressBar loopableProgressBar;

        private void Awake()
        {
            percentageText = percentage.GetComponent<TextMeshProUGUI>();

            barHorizontalOffset = fillBar.offsetMin.x;
            barWidth = GetComponent<RectTransform>().rect.width - barHorizontalOffset*2f;
        }

        private void OnValidate()
        {
            if (oldTextPosition != textPosition)
            {
                switch (textPosition)
                {
                    case TextPosition.BarCenter:
                        percentage.gameObject.SetActive(true);
                        percentage.SetParent(transform, false);
                        percentage.offsetMax = new Vector2(0f, percentage.offsetMax.y);
                        break;
                    case TextPosition.Progress:
                        percentage.gameObject.SetActive(true);
                        percentage.SetParent(fillBar.transform);
                        percentage.offsetMax = new Vector2(5f, percentage.offsetMax.y);
                        break;
                    case TextPosition.NoText:
                        percentage.gameObject.SetActive(false);
                        break;
                    default:
                        break;
                }

                oldTextPosition = textPosition;
                Debug.Log("Changed to: " + oldTextPosition);
            }
        }

        /// <summary>
        /// Sets the progress bar to the given normalized value instantly.
        /// </summary>
        /// <param name="value">Must be in range [0,1]</param>
        public void ForceValue(float value)
        {
            SetPercentInstantly(value, false);
        }

        public void ForceValue(float value, bool control)
        {
            SetPercentInstantly(value, control);
        }

        /// <summary>
        /// The progress bar will move to the given value
        /// </summary>
        /// <param name="value">Must be in range [0,1]</param>
        /// <param name="speedOverride">Will override the base speed if one is given</param>
        public void SetTargetValue(float value, float? speedOverride = null)
        {
            if (isCalculating)
                return;

            if (speedOverride != null)
                baseSpeed = (float)speedOverride;

            if (value > lastValue)
            {
                if (snapOptions == ProgressSnapOptions.SnapToHigherValue)
                    SetPercentInstantly(value, true);
                else
                    StartCoroutine(IncreasePercentSmoothly(value));
            }
            else if(value < lastValue)
            {
                if (snapOptions == ProgressSnapOptions.SnapToLowerValue)
                    SetPercentInstantly(value, true);
                else
                    StartCoroutine(DecreasePercentSmoothly(value));
            }
        }

        private void SetPercentInstantly(float value, bool control, bool isSmooth = false)
        {
            float newBarOffset = barWidth - (barWidth * value) + barHorizontalOffset;
            fillBar.offsetMax = new Vector2(-newBarOffset, fillBar.offsetMax.y);

            int percentage = (int)(value * 100f);
            percentageText.text = percentage.ToString() + "%";

            if(!isSmooth)
                lastValue = value;

            if(control)
                loopableProgressBar.TriggerOnBarComplete();
        }

        private IEnumerator IncreasePercentSmoothly(float value)
        {
            isCalculating = true;

            while (lastValue < value)
            {
                if (speedType == SpeedType.Accelerated)
                {
                    float dif = value - lastValue;
                    lastValue += (dif) / (50 - baseSpeed);
                    if (dif < 0.005)
                        break;
                }
                else
                    lastValue += Time.deltaTime * baseSpeed / 10f;

                SetPercentInstantly(lastValue, false, true);

                yield return null;
            }

            SetPercentInstantly(value, false);
            isCalculating = false;
            loopableProgressBar.TriggerOnBarComplete();
        }

        private IEnumerator DecreasePercentSmoothly(float value)
        {
            isCalculating = true;

            while (lastValue > value)
            {
                if (speedType == SpeedType.Accelerated)
                {
                    float dif = lastValue - value;
                    lastValue -= (dif) / (50 - baseSpeed);
                    if (dif < 0.005)
                        break;
                }
                else
                    lastValue -= Time.deltaTime * baseSpeed / 10f;

                SetPercentInstantly(lastValue,false, true);

                yield return null;
            }

            SetPercentInstantly(value, false);
            isCalculating = false;
            loopableProgressBar.TriggerOnBarComplete();
        }
    }
}
