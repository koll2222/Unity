using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public LayerMask crashMask;
    public Transform myTarget;
    Vector3 Dir = Vector3.zero;
    float Dist = 0.0f;
    float targetDist = 0.0f;
    public float RotSpeed = 5f;
    public float ZoomSpeed = 10f;
    private void Awake()
    {
        transform.LookAt(myTarget);
        rotX = Quaternion.Euler(transform.rotation.eulerAngles.x, 0, 0);
        // 타겟과 카메라의 거리
        Dir = transform.position - myTarget.position;
        // 거리와 방향을 따로 저장
        targetDist = Dist = Dir.magnitude;
        // 벡터 정규화
        Dir.Normalize();
        // 회전값을 제거한다???????????????, 역행렬??????????
        Dir = transform.InverseTransformDirection(Dir);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // 기준 축을 저장할 변수
    Quaternion rotX = Quaternion.identity, rotY = Quaternion.identity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float x = -Input.GetAxis("Mouse Y") * RotSpeed;
            float y = Input.GetAxis("Mouse X") * RotSpeed;

            // 기준 축 문제가 있음
            //Quaternion rot = Quaternion.Euler(0, y, 0) * Quaternion.Euler(x, 0, 0);
            //Dir = rot * Dir;

            rotX *= Quaternion.Euler(x, 0, 0);
            rotY *= Quaternion.Euler(0, y, 0);

            float angle = rotX.eulerAngles.x;
            if (angle > 180f) angle -= 360f;
            angle = Mathf.Clamp(angle, -60f, 80f);
            rotX = Quaternion.Euler(angle, 0, 0);

            //transform.rotation = Quaternion.LookRotation(-Dir);
            // 쿼터니언과 벡터의 연산은 벡터가 회전한다..?
        }

        targetDist -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        targetDist = Mathf.Clamp(targetDist, 1f, 15f);

        Dist = Mathf.Lerp(Dist, targetDist, Time.deltaTime * 3f);

        // 회전은 결합 순서가 매우 중요함.
        Vector3 dir = rotY * rotX * Dir;
        // 카메라를 감싸는 가상의 구를 생각, 가상의 구의 반지름
        float radius = 0.5f;
        // 가상의 구의 반지름만큼 Ray를 더 쏴봄
        if ( Physics.Raycast(new Ray(myTarget.position,dir),out RaycastHit hit, Dist + radius, crashMask) )
        {
            // 반지름만큼 반대 방향으로 옮겨줌
            //transform.position = hit.point + -dir * radius;
            // hit.distance : 레이의 길이?거리? 거리
            Dist = hit.distance - radius;
        }
        transform.position = myTarget.position + dir * Dist;
        transform.LookAt(myTarget);

    }
}
