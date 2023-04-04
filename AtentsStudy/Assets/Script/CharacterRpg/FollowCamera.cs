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
        // Ÿ�ٰ� ī�޶��� �Ÿ�
        Dir = transform.position - myTarget.position;
        // �Ÿ��� ������ ���� ����
        targetDist = Dist = Dir.magnitude;
        // ���� ����ȭ
        Dir.Normalize();
        // ȸ������ �����Ѵ�???????????????, �����??????????
        Dir = transform.InverseTransformDirection(Dir);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // ���� ���� ������ ����
    Quaternion rotX = Quaternion.identity, rotY = Quaternion.identity;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            float x = -Input.GetAxis("Mouse Y") * RotSpeed;
            float y = Input.GetAxis("Mouse X") * RotSpeed;

            // ���� �� ������ ����
            //Quaternion rot = Quaternion.Euler(0, y, 0) * Quaternion.Euler(x, 0, 0);
            //Dir = rot * Dir;

            rotX *= Quaternion.Euler(x, 0, 0);
            rotY *= Quaternion.Euler(0, y, 0);

            float angle = rotX.eulerAngles.x;
            if (angle > 180f) angle -= 360f;
            angle = Mathf.Clamp(angle, -60f, 80f);
            rotX = Quaternion.Euler(angle, 0, 0);

            //transform.rotation = Quaternion.LookRotation(-Dir);
            // ���ʹϾ�� ������ ������ ���Ͱ� ȸ���Ѵ�..?
        }

        targetDist -= Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        targetDist = Mathf.Clamp(targetDist, 1f, 15f);

        Dist = Mathf.Lerp(Dist, targetDist, Time.deltaTime * 3f);

        // ȸ���� ���� ������ �ſ� �߿���.
        Vector3 dir = rotY * rotX * Dir;
        // ī�޶� ���δ� ������ ���� ����, ������ ���� ������
        float radius = 0.5f;
        // ������ ���� ��������ŭ Ray�� �� ����
        if ( Physics.Raycast(new Ray(myTarget.position,dir),out RaycastHit hit, Dist + radius, crashMask) )
        {
            // ��������ŭ �ݴ� �������� �Ű���
            //transform.position = hit.point + -dir * radius;
            // hit.distance : ������ ����?�Ÿ�? �Ÿ�
            Dist = hit.distance - radius;
        }
        transform.position = myTarget.position + dir * Dist;
        transform.LookAt(myTarget);

    }
}
