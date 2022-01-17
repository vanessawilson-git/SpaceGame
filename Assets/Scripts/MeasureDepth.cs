using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Windows.Kinect;

public class MeasureDepth : MonoBehaviour
{
    public delegate void NewTriggerPoints(List<Vector2> triggerPoints);

    public static event NewTriggerPoints OnTriggerPoints = null;

    public MultiSourceManager mMultiSource;
    public Texture2D mDepthTexture;

    // Cutoffs.
    [Range(0, 1.0f)] public float mDepthSensitivity = 1;
    [Range(-10, 10f)] public float mWallDepth = -10;

    [Range(-1, 1f)] public float mTopCutOff = 1;
    [Range(-1, 1f)] public float mBottomCutOff = -1;
    [Range(-1, 1f)] public float mLeftCutOff = -1;
    [Range(-1, 1f)] public float mRightCutOff = 1;

    // Depth.
    private ushort[] mDepthData = null;
    private CameraSpacePoint[] mCameraSpacePoints = null;
    private ColorSpacePoint[] mColorSpacePoints = null;
    private List<ValidPoint> mValidPoints = null;
    private List<Vector2> mTriggerPoints = null;

    // Kinect.
    private KinectSensor mSensor = null;
    private CoordinateMapper mMapper = null;
    private Camera mCamera = null;

    private readonly Vector2Int mDepthResolution = new Vector2Int(512, 424);
    private Rect mRect;

    private void Awake()
    {
        mSensor = KinectSensor.GetDefault();
        mMapper = mSensor.CoordinateMapper;
        mCamera = Camera.main;

        int arraySize = mDepthResolution.x * mDepthResolution.y;

        mCameraSpacePoints = new CameraSpacePoint[arraySize];
        mColorSpacePoints = new ColorSpacePoint[arraySize];
    }

    private void Update()
    {
        mValidPoints = DepthToColor();
        mTriggerPoints = FilterToTrigger(mValidPoints);

        if (OnTriggerPoints != null && mTriggerPoints.Count != 0)
        {
            OnTriggerPoints(mTriggerPoints);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            //mRect = CreateRect(mValidPoints);
            mDepthTexture = CreateTexture(mValidPoints);
        }
    }

    private void OnGUI()
    {
        GUI.Box(mRect, "");

        if (mTriggerPoints == null)
            return;

        foreach (Vector2 point in mTriggerPoints)
        {
            Rect rect = new Rect(point, new Vector2(10, 10));
            GUI.Box(rect, "");
        }
    }

    

    private List<ValidPoint> DepthToColor()
    {
        // Points to return.
        List<ValidPoint> validPoints = new List<ValidPoint>();

        // Get Depth.
        mDepthData = mMultiSource.GetDepthData();

        // Map.

        mMapper.MapDepthFrameToCameraSpace(mDepthData, mCameraSpacePoints);
        mMapper.MapDepthFrameToColorSpace(mDepthData, mColorSpacePoints);

        // Filter.
        for (int i = 0; i < mDepthResolution.x / 4; i++)
        {
            for (int j = 0; j < mDepthResolution.y / 4; j++)
            {
                int sampleIndex = (j * mDepthResolution.x) + i;
                sampleIndex *= 4;

                //CutOff.
                if (mCameraSpacePoints[sampleIndex].X < mLeftCutOff)
                    continue;
                if (mCameraSpacePoints[sampleIndex].X > mRightCutOff)
                    continue;
                if (mCameraSpacePoints[sampleIndex].Y > mTopCutOff)
                    continue;
                if (mCameraSpacePoints[sampleIndex].Y < mBottomCutOff)
                    continue;

                // Creating point.
                ValidPoint newPoint = new ValidPoint(mColorSpacePoints[sampleIndex], mCameraSpacePoints[sampleIndex].Z);

                if (mCameraSpacePoints[sampleIndex].Z >= mWallDepth)
                    newPoint.mWithinWallDepth = true;

                // Add.
                validPoints.Add(newPoint);
            }
        }

        validPoints.ForEach(data =>
        {
            //data.colorSpace.Y = -data.colorSpace.Y + Screen.height + Screen.height;
            data.colorSpace.Y = 1080 - data.colorSpace.Y;
        });

        return validPoints;
    }

    private List<Vector2> FilterToTrigger(List<ValidPoint> validPoints)
    {
        List<Vector2> triggerPoints = new List<Vector2>();
        foreach (ValidPoint point in validPoints)
        {
            if (!point.mWithinWallDepth)
            {
                if (point.z < mWallDepth * mDepthSensitivity)
                {
                    Vector2 screenPoint = ScreenToCamera(new Vector2(point.colorSpace.X, point.colorSpace.Y));

                    triggerPoints.Add(screenPoint);
                }
            }
        }

        return triggerPoints;
    }

    private Texture2D CreateTexture(List<ValidPoint> validPoints)
    {
        Texture2D newTexture = new Texture2D(1920, 1080, TextureFormat.Alpha8, false);

        for (int x = 0; x < 1920; x++)
        {
            for (int y = 0; y < 1080; y++)
            {
                newTexture.SetPixel(x, y, Color.clear);
            }
        }

        foreach (ValidPoint point in mValidPoints)
        {
            //sets color of the dots do yo make invisable convert black to clear
            newTexture.SetPixel((int) point.colorSpace.X, (int) point.colorSpace.Y, Color.black);
        }

        newTexture.Apply();

        return newTexture;
    }

    private Rect CreateRect(List<ValidPoint> points)
    {
        if (points.Count == 0)
        {
            return new Rect();
        }

        Vector2 topLeft = getTopLeft(points);
        Vector2 bottomRight = getBottomRight(points);

        Vector2 screenTopLeft = ScreenToCamera(topLeft);
        Vector2 screenBottomRight = ScreenToCamera(bottomRight);

        int width = (int) (screenBottomRight.x - screenTopLeft.x);
        int height = (int) (screenBottomRight.y - screenTopLeft.y);

        Vector2 size = new Vector2(width, height);
        Rect rect = new Rect(screenTopLeft, size);

        return rect;
    }

    //private void OnDrawGizmos()
    //{

    //    Gizmos.DrawLine()

    //}

    private Vector2 getTopLeft(List<ValidPoint> points)
    {
        Vector2 topleft = new Vector2(int.MaxValue, int.MaxValue);

        foreach (ValidPoint point in points)
        {
            // Most left X.
            if (point.colorSpace.X < topleft.x)
            {
                topleft.x = point.colorSpace.X;
            }

            // Most top Y.
            if (point.colorSpace.Y < topleft.y)
            {
                topleft.y = point.colorSpace.Y;
            }
        }

        return topleft;
    }

    private Vector2 getBottomRight(List<ValidPoint> points)
    {
        Vector2 bottomRight = new Vector2(int.MaxValue, int.MaxValue);

        foreach (ValidPoint point in points)
        {
            // Most left X.
            if (point.colorSpace.X > bottomRight.x)
            {
                bottomRight.x = point.colorSpace.X;
            }

            // Most top Y.
            if (point.colorSpace.Y > bottomRight.y)
            {
                bottomRight.y = point.colorSpace.Y;
            }
        }

        return bottomRight;
    }

    private Vector2 ScreenToCamera(Vector2 screenPosition)
    {
        Vector2 normalizedScreen = new Vector2(Mathf.InverseLerp(0, 1920, screenPosition.x),
            Mathf.InverseLerp(0, 1080, screenPosition.y));
        Vector2 screenPoint =
            new Vector2(normalizedScreen.x * mCamera.pixelWidth, normalizedScreen.y * mCamera.pixelHeight);
        return screenPoint;
    }

    

}

public class ValidPoint
{
    public ColorSpacePoint colorSpace;
    public float z = 0.0f;

    public bool mWithinWallDepth = false;

    public ValidPoint(ColorSpacePoint newColorSpace, float newZ)
    {
        colorSpace = newColorSpace;
        z = newZ;
    }
}

