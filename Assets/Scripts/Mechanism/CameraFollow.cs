using UnityEngine;
using System.Collections;
using Prime31;


public class CameraFollow : MonoBehaviour
{
	public Transform target;
    public Transform rightUpCorner;
    public Transform leftDownCorner;
    public float smoothDampTime = 0.2f;
	[HideInInspector]
	public new Transform transform;
	public Vector3 cameraOffset;
	public bool useFixedUpdate = true;//默认采用FixUpdate
	
	private Vector3 _smoothDampVelocity;
	private float lastZCam;
	
	
	void Awake()
	{
		transform = gameObject.transform;
		lastZCam = transform.position.z;
	}

    private void Start()
    {
        rightUpCorner = GameObject.Find("RightUpCorner").transform;
        leftDownCorner = GameObject.Find("LeftDownCorner").transform;
    }


    void LateUpdate()
	{
		if( !useFixedUpdate )
			updateCameraPosition();
	}


	void FixedUpdate()
	{
		if( useFixedUpdate )
			updateCameraPosition();
	}


	void updateCameraPosition()
	{
        Vector3 temp = transform.position;
        Vector3 targetVec = target.position - cameraOffset;
        //先尝试移动一下镜头，然后判断是否超出屏幕边界
        transform.position = Vector3.SmoothDamp(transform.position, targetVec, ref _smoothDampVelocity, smoothDampTime);

        Vector3 CamrightUpCorner = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(Screen.width + 50, Screen.height + 50, 0));
        Vector3 CamleftDownCorner = GetComponent<Camera>().ScreenToWorldPoint(new Vector3(-50, -50, 0));

        if(rightUpCorner && leftDownCorner)
        {
            if (CamrightUpCorner.x > rightUpCorner.position.x || CamleftDownCorner.x < leftDownCorner.position.x)//超出边界，不可以移动X
            {
                transform.position = new Vector3(temp.x, transform.position.y, transform.position.z);
            }
            if (CamrightUpCorner.y > rightUpCorner.position.y || CamleftDownCorner.y < leftDownCorner.position.y)//超出边界，不可以移动Y
            {
                transform.position = new Vector3(transform.position.x, temp.y, transform.position.z);
            }
        }
        
        Vector3 fixZ = transform.position;
		fixZ = new Vector3(transform.position.x, transform.position.y,  lastZCam);
		transform.position = fixZ;
	}
	
}
