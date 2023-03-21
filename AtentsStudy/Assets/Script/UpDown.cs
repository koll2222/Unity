using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    float a = 0.0f;
    float b = 0.0f;
    float curDist = 0.0f;   //curent distance
    float dist = 2.5f;
    Vector3 dir = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //위로 2.5미터 올라간 뒤에 다시 원래자리로 돌아오는 걸 반복하게 만드세요.
        // #방향과 거리로 생각을 해야함. 위치로 풀어갈 시 문제가 생김.
        {/*
            Vector3 dir = new Vector3(0,1,0);
            if (a < 2.5f)
            {
                a += dir.y * Time.deltaTime;
                b = a;
                transform.Translate(dir * Time.deltaTime);
            }
            else
            {
                b -= dir.y * Time.deltaTime;
                if (b <= 0)
                {
                    a = 0 - b;
                    return;
                }
                transform.Translate(-dir * Time.deltaTime);
            }*/
        }
        //강사님 풀이
        {
            /*// 오차값이 존재함. 예외처리를 해야 함.
            float delta = Time.deltaTime;

            //if (curDist + delta >= 2.5f) // 이동한 거리에 미리 이동할 거리를 더해서 검사
            //{
            //    delta = 2.5f - curDist; // 2.5에서 이동한 거리만큼을 빼주면 2.5에 도달하기 위한 남은 거리가 나옴
            //    dir = -dir;
            //    curDist = 0.0f;
            //    Debug.Log(transform.position.y);
            //}
            //else
            //{
            //    curDist += delta;
            //}
            //transform.Translate(dir * delta);

            // dir = -dir을 하고 이동을 시키면 남은 이동거리만큼 반대 방향으로 가게 되는 것 아닌가?? -해답 요함-
            if (dist - delta <= 0.0f)   // 남은 이동거리에 이동할 거리를 빼서 검사
            {
                delta = dist;           // delta에 dist값 대입, dist는 남은 이동거리가 저장되어 있음.
                dist = 2.5f;            // 남은 이동거리 초기화
                dir = -dir;
            }
            dist -= delta;
            transform.Translate(dir * delta);
            */
        }
        float delta = Time.deltaTime;
        if (dist - delta <= 0.0f)   // 남은 이동거리에 이동할 거리를 빼서 검사
        {
            delta = dist;           // delta에 dist값 대입, dist는 남은 이동거리가 저장되어 있음.
            dist = 2.5f;            // 남은 이동거리 초기화
            transform.Translate(dir * delta,Space.World);
            dir = -dir;
            Debug.Log(transform.position.y);
        }
        else
        {
            dist -= delta;
            transform.Translate(dir * delta,Space.World);
        }

        //Rotate : 축을 기준으로 회전을 시킴 현재 dir은 y값만 있으니 y축을 기준으로 회전
        transform.Rotate(dir * 360.0f * Time.deltaTime,Space.World);

    }
}
