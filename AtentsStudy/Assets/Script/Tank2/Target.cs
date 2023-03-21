using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //선형 보관을 이용해 좌우로 움직이게 만들기

    //float Range = 2.0f;         //타겟이 움직이는 범위
    //Vector3 startPos, destPos;  //시작 지점, 도착 지점
    //float t = 0.0f;             //보관 시간을 저장 할 변수 [0~1]
    //float Dir = 1.0f;           //값의 증가 감소 방향 1이면 증가 -1이면 감소

    //코루틴을 지정하여 멈추게 하기 위해 Coroutine을 참조하는 변수 생성
    Coroutine move = null;

    public GameObject destroyEffect = null;
    
    // Start is called before the first frame update
    void Start()
    {
        //t = 0.5f;   //중앙에 위치
        //destPos = startPos = transform.position;
        //startPos.x -= Range / 2.0f;     //전체 폭의 절반이 시작 지점????
        //destPos.x += Range / 2.0f;      //

        //코루틴 함수의 실행 명령 StartCoroutine("함수명"())
        //move = StartCoroutine(Moving());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            //Moving()을 멈추는 함수
            StopCoroutine(move);
        }

        //t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f);
        ////Mathf.Approximately(t,0.0f) t가 0.0f에 근사하다면
        //if (Mathf.Approximately(t,0.0f) || Mathf.Approximately(t, 1.0f))
        //{
        //    Dir *= -1.0f;
        //}
        //// Lerp(a,b,t) t 는 시간값 0~1
        //transform.position = Vector3.Lerp(startPos, destPos, t);
        ////위 코드를 코루틴?으로 변경해봄
    }
    bool isQuitting = false;
    //종료될 때 호출
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    //파괴될 때 실행, 프로그램이 종료될 때도 실행이 됨
    private void OnDestroy()
    {
        //생성될 때 위치값과 회전값을 넣어줄 수 있음, Quaternion.identity : 회전값이 0임
        if (!isQuitting) Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }
    //코루틴 함수는 계속 진행 중인 함수, ing를 붙여주면 나중에 보기 편함
    IEnumerator Moving()
    {
        /* 일반적인 함수는 return시 함수 종료, 코루틴 함수는 yield return으로 반환해도 종료되지 않음, {}블록 스코프가 닫혀야 끝남
         * 코루틴 함수가 호출 후 yield return 시 해당 프레임에서만 종료, 다음 프레임에서 yield return 다음부터 다시 실행 됨
         * 사라지지 않는 함수이기에 한번만 실행하면 됨. 즉, Update()에서의 실행은 과부화의 원인이 될 수 있음.
         * 시스템상 성능이 증가하진 않으나 코드의 가독성이 증가해 사용을 많이 함.
         */

        //
        float Range = 2.0f;         //타겟이 움직이는 범위
        Vector3 startPos, destPos;  //시작 지점, 도착 지점
        float t = 0.0f;             //보관 시간을 저장 할 변수 [0~1]
        float Dir = 1.0f;           //값의 증가 감소 방향 1이면 증가 -1이면 감소

        t = 0.5f;   //중앙에 위치
        destPos = startPos = transform.position;
        startPos.x -= Range / 2.0f;     //전체 폭의 절반이 시작 지점????
        destPos.x += Range / 2.0f;      //

        //일반적으로 while(true)는 무한 루프에 빠지지만, 코루틴에서는 yield return이 있기에 해당 프레임에서 종료가 되어 무한 루프에 빠지지 않음
        while (true)
        {
            t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f);
            //Mathf.Approximately(t,0.0f) t가 0.0f에 근사하다면
            if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f))
            {
                Dir *= -1.0f;
            }
            // Lerp(a,b,t) t 는 시간값 0~1
            transform.position = Vector3.Lerp(startPos, destPos, t);

            yield return null;  // IEnumerator의 반환 형식
        }
    }
}
