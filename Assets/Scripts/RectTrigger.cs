using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RectTrigger : MonoBehaviour
{
    [Range(0, 10)]
    public int mSensitivity = 3;
    public Color mColor = Color.clear;

    public bool mIsTriggered = false;

    private Camera mCamera = null;
    private RectTransform mRectTransform = null;
    private Image mImage = null;

    private void Awake()
    {
        MeasureDepth.OnTriggerPoints += OnTriggerPoints;

        mCamera = Camera.main;
        mRectTransform = GetComponent<RectTransform>();
        mImage = GetComponent<Image>();
    }
    private void OnDestroy()
    {
        MeasureDepth.OnTriggerPoints -= OnTriggerPoints;
    }

    private void OnTriggerPoints(List<Vector2> triggerPoints)
    {
        if (!enabled)
        {
            return;
        }

        int count = 0;

        foreach (Vector2 point in triggerPoints)
        {
            Vector2 flippedY = new Vector2(point.x, mCamera.pixelHeight- point.y);

            if (RectTransformUtility.RectangleContainsScreenPoint(mRectTransform, flippedY))
            {
                count++;
            }
            if (count > mSensitivity)
            {
                mIsTriggered = true;
                mImage.color = mColor;
            }
            else
            {
                mIsTriggered = false;
                var tempColor = Color.black;
                tempColor.a = 0.5f;
                mImage.color = tempColor;
            }


        }
    }
}
