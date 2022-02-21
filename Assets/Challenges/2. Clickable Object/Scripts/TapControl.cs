using Challenges._2._Clickable_Object.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TapControl : MonoBehaviour
{
    [SerializeField]
    Camera mainCam;

    Touch touch;

    private int tapCount = 0;
    private float maxDoubleTapTime = 0.05f;
    private float newTime;
    private float touchDuration = 0f;

    private ClickableObject lastClicked;
    private bool isDoubleTap = false;

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            TouchControl();
        }
        else
        {
            touchDuration = 0f;
        }
    }

    private void TouchControl()
    {
        touchDuration += Time.deltaTime;

        touch = Input.GetTouch(0);
        Ray ray = mainCam.ScreenPointToRay(touch.position);

        if (touch.phase == TouchPhase.Began && Physics.Raycast(ray, out RaycastHit hitInfo))
        {
            ClickableObject clickedObj = hitInfo.transform.GetComponent<ClickableObject>();
            if (clickedObj != null)
            {
                //Double tapped to different objects?
                if (tapCount == 1 && lastClicked != clickedObj)
                    tapCount = 0;

                lastClicked = clickedObj;
            }
        }
        else if(touch.phase == TouchPhase.Ended && Physics.Raycast(ray, out RaycastHit hitInfo2))
        {
            ClickableObject clickedObj = hitInfo2.transform.GetComponent<ClickableObject>();
            if (clickedObj == lastClicked)
            {
                if (touchDuration < 0.2f)
                {
                    tapCount++;
                    StartCoroutine(SingleOrDouble(lastClicked));
                }
                else
                {
                    clickedObj.Tapped(3);
                }
            }
        }
    }

    private IEnumerator SingleOrDouble(ClickableObject clickedObj)
    {
        yield return new WaitForSeconds(0.3f);

        if (tapCount == 1)
        {
            clickedObj.Tapped(1);
            tapCount = 0;
        }
            
        else if (tapCount > 1)
        {
            //First coroutine lets the second coroutine to do the work.
            if (!isDoubleTap)
            {
                isDoubleTap = true;
            }
            else
            {
                clickedObj.Tapped(2);
                tapCount = 0;
                isDoubleTap = false;
            }
        }
    }
}
