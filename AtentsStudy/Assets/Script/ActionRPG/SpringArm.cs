using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ZoomData
{
    public float ZoomSpeed;
    public Vector2 ZoomRange;
    public float ZoomLerpSpeed;
    public float curDist;
    public float desireDist;

    public ZoomData(float speed)
    {
        ZoomSpeed = speed;
        ZoomRange = new Vector2(1, 7);
        ZoomLerpSpeed = 3f;
        curDist = 0f;
        desireDist = 0f;
    }
}
public class SpringArm : MonoBehaviour
{
    [field:SerializeField]public float testValue { get; set; }  // DefaultProperty
    [SerializeField] public LayerMask crashMask;
    [SerializeField] float Offset = 0.5f;
    [SerializeField] float RotSpeed = 180.0f;
    [SerializeField] Transform myCam = null;
    
    Vector3 curRot = Vector3.zero;
    public Vector2 LookUpRange = new Vector2(-60, 90);

    public ZoomData myZoomData = new ZoomData(3f);
    // Start is called before the first frame update
    void Start()
    {
        myCam = GetComponentInChildren<Camera>().transform;
        curRot = transform.localRotation.eulerAngles;
        myZoomData.desireDist = myZoomData.curDist = myCam.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            curRot.x = Mathf.Clamp(curRot.x - Input.GetAxis("Mouse Y") * RotSpeed * Time.deltaTime, LookUpRange.x, LookUpRange.y);
            curRot.y += Input.GetAxis("Mouse X") * RotSpeed * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(curRot.x,0,0);
            transform.parent.localRotation = Quaternion.Euler(0, curRot.y, 0);
        }
        myZoomData.desireDist = Mathf.Clamp(myZoomData.desireDist + -Input.GetAxis("Mouse ScrollWheel") * myZoomData.ZoomSpeed, myZoomData.ZoomRange.x, myZoomData.ZoomRange.y);

        if (Physics.Raycast(transform.position, -transform.forward, out RaycastHit hit, myZoomData.curDist + Offset, crashMask))
        {
            myZoomData.curDist = hit.distance - Offset;
        }
        else
        {
            myZoomData.curDist = Mathf.Lerp(myZoomData.curDist, myZoomData.desireDist, Time.deltaTime * myZoomData.ZoomLerpSpeed);
        }
        myCam.localPosition = Vector3.back * myZoomData.curDist;
    }
}
