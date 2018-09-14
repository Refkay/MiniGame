using UnityEngine;
using System.Collections;

public class CameraControl : MonoBehaviour {



    public enum CameraType
    {
        Vertical,
        Horizontal,
        Normal
    }

    public CameraType cameraType;
    public float dampTime = 1.5f;
    public Transform target;
    // 相机移动速度，初始速度清零
    private Vector3 velocity = Vector3.zero;

    // 相机单例
    private static CameraControl instance;
    public static CameraControl Instance
    {
        get
        {
            return instance;
        }
    }

    // 屏幕的默认宽高的1/100 (预编译)
#if UNITY_ANDROID
	private static float devHeight = 8.54f;
	private static float devWidth = 4.8f;
#elif UNITY_IPHONE
	private static float devHeight = 9.6f;
	private static float devWidth = 6.4f;
#else
    private static float devHeight = 19.20f;
    private static float devWidth = 10.80f;
#endif

    // Use this for initialization
    void Awake()
    {
        instance = this;
        // 屏幕适配
        //float screenHeight = Screen.height;

        ////Debug.Log ("screenHeight = " + screenHeight);

        ////this.GetComponent<Camera>().orthographicSize = screenHeight / 200.0f;

        //float orthographicSize = this.GetComponent<Camera>().orthographicSize;

        //float aspectRatio = Screen.width * 1.0f / Screen.height;

        //float cameraWidth = orthographicSize * 2 * aspectRatio;

        ////Debug.Log ("cameraWidth = " + cameraWidth);

        //if (cameraWidth < devWidth)
        //{
        //    orthographicSize = devWidth / (2 * aspectRatio);
        //    Debug.Log("new orthographicSize = " + orthographicSize);
        //    this.GetComponent<Camera>().orthographicSize = orthographicSize;
        //}

    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (target)
        {
            SetCamera();
        }
        else
        {
            SetTarget();
        }
    }

    // 设置相机
    void SetCamera()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(target.position);
        Vector3 delta = target.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = transform.position + delta;
        switch (cameraType)
        {
            case CameraType.Vertical:// 竖直相机
                transform.position = Vector3.SmoothDamp(transform.position,
                                                         new Vector3(transform.position.x, destination.y, destination.z),
                                                         ref velocity, dampTime);
                break;
            case CameraType.Horizontal:// 水平相机
                transform.position = Vector3.SmoothDamp(transform.position,
                                                         new Vector3(destination.x, transform.position.y, destination.z),
                                                         ref velocity, dampTime);
                break;
            case CameraType.Normal:// 无限制的相机
                transform.position = Vector3.SmoothDamp(transform.position,
                                                         destination,
                                                         ref velocity, dampTime);
                break;
            default:
                break;
        }
    }
    // 设置目标
    void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
