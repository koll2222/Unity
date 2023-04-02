using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPlayer : RPGProperty
{

    public NavMeshAgent myNav;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("Speed", myNav.velocity.magnitude / myNav.speed );
    }

    public void OnWarp(Vector3 pos)
    {
        myNav.Warp(pos);
    }

    public void OnMove(Vector3 pos)
    {
        //StartCoroutine(Moving(pos));
        //myNav.SetDestination(pos);
        if (myAnim.GetBool("isAir")) return;
        StopAllCoroutines();
        StartCoroutine(JumpableMoving(pos));
    }
    IEnumerator Moving(Vector3 pos)
    {
        // navigation에서 움직임
        myNav.SetDestination(pos);
        // 움직이는 애니메이션
        myAnim.SetBool("isMoving", true);
        // myNav.pathPending : SetDestination은 코루틴으로 돌아가기에 연산중인지 아닌지 판별이 필요, 연산중 true 끝 false
        // || 남은 거리 > 멈출 거리?
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            yield return null;
        }
        myAnim.SetBool("isMoving", false);
    }

    IEnumerator JumpableMoving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            // OffMeshLink는 트리거처럼 해당 범위에 들어가면 인식됨
            if (myNav.isOnOffMeshLink)
            {
                myAnim.SetBool("isAir", true);
                // 완전 멈추는게 아닌 pause 임
                myNav.isStopped = true;
                // 도착 지점
                Vector3 endPos = myNav.currentOffMeshLinkData.endPos;
                Vector3 dir = endPos - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();

                while(dist > 0.0f)
                {
                    float delta = myNav.speed * Time.deltaTime;
                    if(dist - delta < 0.0f)
                    {
                        delta = dist;
                    }
                    dist -= delta;
                    transform.Translate(dir * delta, Space.World);
                    
                    yield return null;
                }
                myAnim.SetBool("isAir", false);
                myNav.CompleteOffMeshLink();
                myNav.isStopped = false;
                //myNav.velocity = dir * myNav.speed;

            }
            yield return null;
        }
    }
    
}
