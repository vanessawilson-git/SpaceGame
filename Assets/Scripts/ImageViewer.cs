using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageViewer : MonoBehaviour
{
    public MeasureDepth mMeasureDepth;
    public MultiSourceManager Multisource;

    public RawImage RawImg;
    public RawImage RawDepth;

    // Update is called once per frame
    void Update()
    {
        RawImg.texture = Multisource.GetColorTexture();

      RawDepth.texture = mMeasureDepth.mDepthTexture;
    }
}
