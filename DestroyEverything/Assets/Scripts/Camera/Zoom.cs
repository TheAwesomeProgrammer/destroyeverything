using UnityEngine;
using System.Collections;

public class Zoom : MonoBehaviour
{
    public float Speed;
    public float MinZoom = 2;
    public float MaxZoom = 10;

    private Camera mCamera;

	// Use this for initialization
	void Start ()
	{
	    mCamera = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	    float tScrollViewInput = Input.GetAxis("Mouse ScrollWheel");
        if(tScrollViewInput > 0)
        {
            ZoomIn();
        }
        else if(tScrollViewInput < 0)
        {
            ZoomOut();
        }
	}

    void ZoomIn()
    {
        if(mCamera.orthographicSize > MinZoom)
        {
            mCamera.orthographicSize -= Speed*Time.deltaTime;
        }
    }

    void ZoomOut()
    {
        if (mCamera.orthographicSize < MaxZoom)
        {
            mCamera.orthographicSize += Speed * Time.deltaTime;
        }
    }
}
