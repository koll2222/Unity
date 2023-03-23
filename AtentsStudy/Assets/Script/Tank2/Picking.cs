using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour
{
    /* ���콺 Ŭ������ �̵�
     */

    public LayerMask pickMask;
    public LayerMask enemyMask;
    public float MoveSpeed = 10.0f;
    public float Velocity = 10.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        targetPos = transform.position;
        targetRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity,pickMask))
            {
                // if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Ground"))
                // 1�� ��Ʈ�� ���� ���� 000...1 �� ��
                if( ((1 << hit.transform.gameObject.layer) & enemyMask) == 0 )
                { 
                    StopAllCoroutines();
                    targetPos = transform.position;             // ���� ��ġ
                    targetRot = transform.rotation.eulerAngles; // ���� ��? Quaternion ���̱⿡ euler ������ ����

                    StartCoroutine(MovingToPos(hit.point)); 
                }

                
            }
        }
        transform.position = Vector3.Lerp(transform.position, targetPos, Velocity * Time.deltaTime);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(targetRot), 10.0f * Time.deltaTime);
    }
    Vector3 targetPos;
    IEnumerator MovingToPos(Vector3 pos)
    {
        {/* ���� ������ ��ǥ, ���� ������ ��ǥ, ��ǥ���� �Ÿ�, ���� ����, while�� ������ ���.
         * Lerp�� ����ϸ� ���� ������
         */
            ////float a = Vector3.Distance(transform.localPosition, pos);  // ��ǥ���� �Ÿ�
            //float dt = 0.0f;

            //while (true)
            //{
            //    dt = Mathf.Clamp(dt + Time.deltaTime, 0f, 20.0f);

            //    //transform.Rotate(pos * 10f * Time.deltaTime);
            //    //Vector3 pos_xz = new Vector3(pos.x, 0, pos.z);
            //    //transform.Translate(pos * Time.deltaTime);
            //    transform.position = Vector3.Lerp(transform.localPosition, pos, dt * Time.deltaTime);
            //    yield return null;
            //}
        }

        // ����� Ǯ��
        // ������ ��Ÿ���� Vector3�� ��ġ�� ��Ÿ���� Vector3�� �����ؼ� ����

        // ���� ������ �̿��ؼ� ��,���� ����
        //targetPos = transform.position; // ���� ��ġ

        // transform.position ���� pos ���� ���� ���Ͱ�
        Vector3 dir = pos - transform.position;
        // transform.position ���� pos ���� �Ÿ�, Vector3.magnitude : ������ ����
        float dist = dir.magnitude;
        // ������ ��Ÿ���� ���ʹ� ���̰� 1�� ���ͷ� �ٲ㼭 ����ؾ� �� : ������ ����ȭ(normalize)
        // �Ÿ��� ��� �����ؾ� ��.
        dir.Normalize();

        //Rotating�� ���� �� ���� ��ٷȴٰ� �����ϰ� ��
        StartCoroutine(Rotating(dir));
        // ȸ����Ű��, ȸ�� ���� ����ȭ�� ���� �ؾ� ��
        {
            //float d = Vector3.Dot(transform.forward, dir);
            //float r = Mathf.Acos(d);
            ////    y : 180 = x : pi        ���̴� �� ������??
            //// -> y / 180 = x / pi
            //// -> y = 180 * ( x / pi )
            //float angle = 180.0f * (r / Mathf.PI);
            ////float angle2 = Vector3.Angle(transform.forward, dir);   //�� ���� ����Ƽ �Լ����� ������ ��

            //float rotDir = 1.0f;
            //if (Vector3.Dot(transform.right, dir) < 0.0f)
            //{
            //    rotDir *= -1.0f;
            //}

            //transform.Rotate(Vector3.up * angle * rotDir);
        }

        
        while (dist > 0.0f) //�Ÿ��� ��������
        {
            float delta = MoveSpeed * Time.deltaTime;    //�ʴ� MoveSpeed�� �̵��ؾ� �� ��� �����Ӵ� �̵��ؾ� �� �Ÿ�
            if(dist - delta < 0.0f)
            {
                delta = dist;   //���� �Ÿ��� ����
            }
            //transform.Translate(dir * delta, Space.World);    //��� �̵�   //dir ����� ���� �������� �߱⿡ �̵��� ���� �������� �ؾ� ��

            // �������� Lerp, �������� Slerp : Sphere Lerp
            //���� ������ �̿��� ��,����
            targetPos += dir * delta;
            //transform.position = Vector3.Lerp(transform.position, targetPos, Velocity * Time.deltaTime);

            dist -= delta;
            yield return null;
        }
    }

    // ���콺 Ŭ���� ������ �ٶ󺸰� ȸ��
    Vector3 targetRot;
    IEnumerator Rotating(Vector3 dir)
    {
        {
         //while (true)
         //{
         //    float rotDir = 1.0f;
         //    if(Vector3.Dot(transform.right, dir) < 0.0f)
         //    {
         //        rotDir *= -1;
         //    }
         //    float angle = Vector3.Angle(transform.forward, dir);
         //    transform.Rotate(Vector3.up * angle * rotDir * Time.deltaTime * Velocity);

            //    yield return null;
            //}
        }
        float d = Vector3.Dot(transform.forward, dir);
        float r = Mathf.Acos(d);
        float angle = r * Mathf.Rad2Deg;
        float rotDir = 1.0f;
        if(Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir *= -1.0f;
        }
        while (angle > 0.0f)
        {
            float delta = 360.0f * Time.deltaTime;
            if(angle - delta < 0.0f)
            {
                delta = angle;
            }
            //transform.Rotate(Vector3.up * rotDir * delta);
            /* ��,���� */
            targetRot.y += delta * rotDir;
            angle -= delta;

            yield return null;
        }
    }
}
