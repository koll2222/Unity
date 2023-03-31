using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform myTarget;
    Vector3 Dir = Vector3.zero;
    float Dist = 0.0f;
    public float RotSpeed = 5f;
    public float ZoomSpeed = 10f;
    private void Awake()
    {
        // 타겟과 카메라의 거리
        Dir = transform.position - myTarget.position;
        // 거리와 방향을 따로 저장
        Dist = Dir.magnitude;
        Dir.Normalize();
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float x = -Input.GetAxis("Mouse Y") * RotSpeed;
            float y = Input.GetAxis("Mouse X") * RotSpeed;

            Quaternion rot = Quaternion.Euler(0, y, 0) * Quaternion.Euler(x, 0, 0);
            Dir = rot * Dir;

            //transform.LookAt(myTarget);
            //transform.forward = -Dir;
            transform.rotation = Quaternion.LookRotation(-Dir);

        }

        Dist -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        transform.position = myTarget.position + Dir * Dist;
    }
}
