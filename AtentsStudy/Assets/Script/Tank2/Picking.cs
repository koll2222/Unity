using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour
{
    /* 마우스 클릭으로 이동
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
                // 1은 비트로 보면 전부 000...1 이 됨
                if( ((1 << hit.transform.gameObject.layer) & enemyMask) == 0 )
                { 
                    StopAllCoroutines();
                    targetPos = transform.position;             // 현재 위치
                    targetRot = transform.rotation.eulerAngles; // 현재 각? Quaternion 값이기에 euler 값으로 변형

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
        {/* 시작 지점의 좌표, 도착 지점의 좌표, 좌표간의 거리, 도착 판정, while문 끝내는 방법.
         * Lerp를 사용하면 조금 편해짐
         */
            ////float a = Vector3.Distance(transform.localPosition, pos);  // 좌표간의 거리
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

        // 강사님 풀이
        // 방향을 나타내는 Vector3와 위치를 나타내는 Vector3를 구분해서 인지

        // 선형 보관을 이용해서 가,감속 구현
        //targetPos = transform.position; // 현재 위치

        // transform.position 에서 pos 로의 방향 벡터값
        Vector3 dir = pos - transform.position;
        // transform.position 에서 pos 로의 거리, Vector3.magnitude : 벡터의 길이
        float dist = dir.magnitude;
        // 방향을 나타내는 벡터는 길이가 1인 벡터로 바꿔서 사용해야 함 : 벡터의 정규화(normalize)
        // 거리를 재고 실행해야 함.
        dir.Normalize();

        //Rotating이 끝날 때 까지 기다렸다가 실행하게 됨
        StartCoroutine(Rotating(dir));
        // 회전시키기, 회전 또한 정규화를 먼저 해야 함
        {
            //float d = Vector3.Dot(transform.forward, dir);
            //float r = Mathf.Acos(d);
            ////    y : 180 = x : pi        파이는 왜 나오지??
            //// -> y / 180 = x / pi
            //// -> y = 180 * ( x / pi )
            //float angle = 180.0f * (r / Mathf.PI);
            ////float angle2 = Vector3.Angle(transform.forward, dir);   //위 식을 유니티 함수에서 지원해 줌

            //float rotDir = 1.0f;
            //if (Vector3.Dot(transform.right, dir) < 0.0f)
            //{
            //    rotDir *= -1.0f;
            //}

            //transform.Rotate(Vector3.up * angle * rotDir);
        }

        
        while (dist > 0.0f) //거리를 조건으로
        {
            float delta = MoveSpeed * Time.deltaTime;    //초당 MoveSpeed로 이동해야 할 경우 프레임당 이동해야 할 거리
            if(dist - delta < 0.0f)
            {
                delta = dist;   //남은 거리를 대입
            }
            //transform.Translate(dir * delta, Space.World);    //등속 이동   //dir 계산을 월드 기준으로 했기에 이동도 월드 기준으로 해야 함

            // 선형보관 Lerp, 구형보관 Slerp : Sphere Lerp
            //선형 보관을 이용해 가,감속
            targetPos += dir * delta;
            //transform.position = Vector3.Lerp(transform.position, targetPos, Velocity * Time.deltaTime);

            dist -= delta;
            yield return null;
        }
    }

    // 마우스 클릭한 지점을 바라보게 회전
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
            /* 가,감속 */
            targetRot.y += delta * rotDir;
            angle -= delta;

            yield return null;
        }
    }
}
