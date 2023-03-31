using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RPGMovement : RPGProperty
{
    Coroutine coMove = null;

    protected void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        if (coMove != null)
        {
            StopCoroutine(coMove);
            coMove = null;
        }
        coMove = StartCoroutine(MovingToPos(pos, done));
    }
    protected void FollowTarget(Transform target)
    {
        StopAllCoroutines();
        StartCoroutine(FollowingTarget(target));
    }
    IEnumerator MovingToPos(Vector3 pos, UnityAction done)
    {
        // 목적지의 방향
        Vector3 dir = pos - transform.position;
        // 목적지까지의 거리
        float dist = dir.magnitude;
        // 정규화
        dir.Normalize();

        StartCoroutine(Rotating(dir));
        myAnim.SetBool("isMoving", true);

        while(dist > 0.0f)
        {
            if (!myAnim.GetBool("isAttacking"))
            {
                float delta = MoveSpeed * Time.deltaTime;
                if (dist - delta < 0.0f)
                {
                    delta = dist;
                }
                dist -= delta;
                transform.Translate(dir * delta, Space.World);
            }
            yield return null;
        }
        myAnim.SetBool("isMoving", false);
        done?.Invoke();
    }

    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;

        if (Vector3.Dot(transform.right, dir) < 0.0f)
        {
            rotDir *= -1.0f;
        }

        while (angle > 0.0f)
        {
            float delta = rotSpeed * Time.deltaTime;
            if(angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up, delta * rotDir);
            yield return null;
        }
    }

    /* 대상을 따라가서 공격할 코루틴
     * Transform을 받아오는 이유 :
     * position 값은 값형이라 코루틴 진행중 값이 변하지 않음
     * Transform은 참조형이라 코루틴 진행 중에도 값이 변하게 됨.
     * 움직이는 대상을 따라다니게 할 목적이면 이게 맞다
     */
    IEnumerator FollowingTarget(Transform target)
    {
        while (target != null)
        {
            // 공격 딜레이를 검사할 playTime
            if(!myAnim.GetBool("isAttacking")) playTime += Time.deltaTime;
            if (!myAnim.GetBool("isAttacking"))
            {
                myAnim.SetBool("isMoving", false);
                // 나 -> 타겟으로의 방향 벡터값
                Vector3 dir = target.position - transform.position;
                // 나 -> 타겟까지의 거리, 공격 범위를 빼줌
                float dist = dir.magnitude - AttackRange;
                // 방향 벡터값 정규화
                dir.Normalize();
                // 프레임당 이동 거리
                float delta = MoveSpeed * Time.deltaTime;
                // 거리가 벌어졌다면
                if (dist > 0.0f)
                {
                    if (dist < delta)
                    {
                        delta = dist;
                    }
                    // 이동이 이루어지니까 move 모션 출력
                    myAnim.SetBool("isMoving", true);
                    // 해당 위치로 이동
                    transform.Translate(dir * delta, Space.World);
                }
                else
                {
                    // isAttacking == false
                    if (!myAnim.GetBool("isAttacking"))
                    {
                        // 딜레이 검사
                        if (playTime > AttackDelay)
                        {
                            // 딜레이 검사 초기화
                            playTime = 0.0f;
                            // 공격
                            myAnim.SetTrigger("Attack");
                        }
                    }
                }
                // Rotation
                // 회전 각
                float angle = Vector3.Angle(transform.forward, dir);
                // 회전 방향
                float rotDir = 1.0f;
                // 회전 방향을 구함, 나의 오른쪽과 목적지를 내적
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                {
                    // 방향 지정
                    rotDir *= -1.0f;
                }
                // 프레임당 회전 값
                delta = rotSpeed * Time.deltaTime;

                if (angle < delta)
                {
                    delta = angle;
                }
                // 방향 회전
                transform.Rotate(transform.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }
}
