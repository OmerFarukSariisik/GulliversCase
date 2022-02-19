using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Challenges._2._Clickable_Object.Scripts
{
    public delegate void OnClickableClicked(ClickableObject targetObject, ClickableObject.InteractionMethod method);
    public delegate void OnClickableClickedUnspecified();
    /// <summary>
    /// This exists to make sure you don't remove any functions, DO NOT EDIT
    /// </summary>
    public interface IClickableObject
    {
        /// <summary>
        /// Checks if the given interaction method is valid for this clickable object.
        /// </summary>
        /// <param name="method"></param>
        /// <returns></returns>
        bool IsInteractionMethodValid(ClickableObject.InteractionMethod method);

        /// <summary>
        /// Updates the interaction method of the clickable object. Can contain more than one value due to bitflags
        /// </summary>
        void SetInteractionMethod(ClickableObject.InteractionMethod method);

        /// <summary>
        /// Will invoke the given callback when the clickable object is interacted with alongside the method of interaction
        /// </summary>
        /// <param name="callback">Function to invoke</param>
        void RegisterToClickable(OnClickableClicked callback);

        /// <summary>
        /// Will unregister a previously provided callback
        /// </summary>
        /// <param name="callback">Function previously given</param>
        void UnregisterFromClickable(OnClickableClicked callback);

        /// <summary>
        /// Will invoke the given callback when the clickable object is tapped. 
        /// </summary>
        /// <param name="onTapCallback"></param>
        /// <exception cref="InvalidInteractionMethodException">If tapping is not allowed for this clickable</exception>
        void RegisterToClickableTap(OnClickableClickedUnspecified onTapCallback);

        /// <summary>
        /// Will invoke the given callback when the clickable object is tapped. 
        /// </summary>
        /// <param name="onTapCallback"></param>
        /// <exception cref="InvalidInteractionMethodException">If double tapping is not allowed for this clickable</exception>
        void RegisterToClickableDoubleTap(OnClickableClickedUnspecified onTapCallback);
    }
    /// <summary>
    /// DO NOT EDIT
    /// </summary>
    public class ClickableTester : MonoBehaviour
    {
        private Dictionary<ClickableObject, float> _tapTimes = new Dictionary<ClickableObject, float>();
        [SerializeField]
        private List<ClickableObject> clickableObjects;
        [SerializeField]
        private ClickableObject randomizedClickableObject;

        private ClickableObject.InteractionMethod _selectedInteractionMethodsForRandom;

        [SerializeField]
        private TMP_Text infoText;

        void Start()
        {
            if (infoText == null)
            {
                Debug.LogError("ClickableTester: InfoText was not provided. Null reference");
            }
            SetupClickableObjects();

            if (randomizedClickableObject==null)
            {
                Debug.LogError("ClickableTester: Randomized clickable is null");
                return;
            }

            SetupRandomizedClickableObject();
        }

        private void SetupRandomizedClickableObject()
        {
            _selectedInteractionMethodsForRandom = 0;
            foreach (ClickableObject.InteractionMethod interactionMethod in Enum.GetValues(
                typeof(ClickableObject.InteractionMethod)))
            {
                if (Random.value < 0.5f)
                {
                    _selectedInteractionMethodsForRandom = _selectedInteractionMethodsForRandom & interactionMethod;
                }
            }

            randomizedClickableObject.SetInteractionMethod(_selectedInteractionMethodsForRandom);
            
            foreach (ClickableObject.InteractionMethod interactionMethod in Enum.GetValues(
                typeof(ClickableObject.InteractionMethod)))
            {
                if (_selectedInteractionMethodsForRandom.HasFlag(interactionMethod) && !randomizedClickableObject.IsInteractionMethodValid(interactionMethod))
                {
                    Debug.LogError($"SetInteractionMethod does not work properly. {interactionMethod} should be valid for randomizedClickable");
                }else if (!_selectedInteractionMethodsForRandom.HasFlag(interactionMethod) &&
                          randomizedClickableObject.IsInteractionMethodValid(interactionMethod))
                {
                    Debug.LogError($"SetInteractionMethod does not work properly. {interactionMethod} should be invalid for randomizedClickable");
                }
            }
            
            SetupClickableObject(randomizedClickableObject);
        }

        private void SetupClickableObjects()
        {
            foreach (var clickableObject in clickableObjects)
            {
                if (clickableObject == null)
                {
                    Debug.LogError(
                        "ClickableTester: Found a null ClickableObject in the clickableObject list. Make sure you haven't accidentally removed something from the list.");
                    continue;
                }

                SetupClickableObject(clickableObject);
            }
        }

        private void SetupClickableObject(ClickableObject clickableObject)
        {
            clickableObject.RegisterToClickable(ClickableClicked);
            _tapTimes.Add(clickableObject,-100);
            TestForOnTap(clickableObject);
            TestForOnDoubleTap(clickableObject);
        }

        private static void TestForOnTap(ClickableObject clickableObject)
        {
            if (clickableObject.IsInteractionMethodValid(ClickableObject.InteractionMethod.Tap))
            {
                try
                {
                    clickableObject.RegisterToClickableTap(() => { });
                }
                catch (InvalidInteractionMethodException exception)
                {
                    Debug.LogError(
                        $"Couldn't register to {clickableObject.name}'s OnClickableTap event when it should be allowed.");
                }
            }
            else
            {
                try
                {
                    clickableObject.RegisterToClickableTap(() => { });
                    Debug.LogError(
                        $"Could register to {clickableObject.name}'s OnClickableTap event when it shouldn't be allowed.");
                }
                catch (InvalidInteractionMethodException exception)
                {
                }
            }
        }
        
        private static void TestForOnDoubleTap(ClickableObject clickableObject)
        {
            if (clickableObject.IsInteractionMethodValid(ClickableObject.InteractionMethod.DoubleTap))
            {
                try
                {
                    clickableObject.RegisterToClickableDoubleTap(() => { });
                }
                catch (InvalidInteractionMethodException exception)
                {
                    Debug.LogError(
                        $"Couldn't register to {clickableObject.name}'s OnClickableDoubleTap event when it should be allowed.");
                }
            }
            else
            {
                try
                {
                    clickableObject.RegisterToClickableDoubleTap(() => { });
                    Debug.LogError(
                        $"Could register to {clickableObject.name}'s OnClickableDoubleTap event when it shouldn't be allowed.");
                }
                catch (InvalidInteractionMethodException exception)
                {
                }
            }
        }

        private void OnDestroy()
        {
            foreach (var clickableObject in clickableObjects)
            {
                clickableObject.UnregisterFromClickable(ClickableClicked);
            }
        }

        
        private void ClickableClicked(ClickableObject clickedObject, ClickableObject.InteractionMethod interactionMethod)
        {
            
            if (Time.time - _tapTimes[clickedObject] < 0.05f)
            {
                Debug.LogWarning("Tap was called very shortly after last input. Make sure you're not calling multiple callbacks at the same time");
            }
            _tapTimes[clickedObject] = Time.time;
            infoText.gameObject.SetActive(true);
            infoText.text = $"{clickedObject.name}: {interactionMethod}";
        }
    }
}
