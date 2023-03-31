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
        // �������� ����
        Vector3 dir = pos - transform.position;
        // ������������ �Ÿ�
        float dist = dir.magnitude;
        // ����ȭ
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

    /* ����� ���󰡼� ������ �ڷ�ƾ
     * Transform�� �޾ƿ��� ���� :
     * position ���� �����̶� �ڷ�ƾ ������ ���� ������ ����
     * Transform�� �������̶� �ڷ�ƾ ���� �߿��� ���� ���ϰ� ��.
     * �����̴� ����� ����ٴϰ� �� �����̸� �̰� �´�
     */
    IEnumerator FollowingTarget(Transform target)
    {
        while (target != null)
        {
            // ���� �����̸� �˻��� playTime
            if(!myAnim.GetBool("isAttacking")) playTime += Time.deltaTime;
            if (!myAnim.GetBool("isAttacking"))
            {
                myAnim.SetBool("isMoving", false);
                // �� -> Ÿ�������� ���� ���Ͱ�
                Vector3 dir = target.position - transform.position;
                // �� -> Ÿ�ٱ����� �Ÿ�, ���� ������ ����
                float dist = dir.magnitude - AttackRange;
                // ���� ���Ͱ� ����ȭ
                dir.Normalize();
                // �����Ӵ� �̵� �Ÿ�
                float delta = MoveSpeed * Time.deltaTime;
                // �Ÿ��� �������ٸ�
                if (dist > 0.0f)
                {
                    if (dist < delta)
                    {
                        delta = dist;
                    }
                    // �̵��� �̷�����ϱ� move ��� ���
                    myAnim.SetBool("isMoving", true);
                    // �ش� ��ġ�� �̵�
                    transform.Translate(dir * delta, Space.World);
                }
                else
                {
                    // isAttacking == false
                    if (!myAnim.GetBool("isAttacking"))
                    {
                        // ������ �˻�
                        if (playTime > AttackDelay)
                        {
                            // ������ �˻� �ʱ�ȭ
                            playTime = 0.0f;
                            // ����
                            myAnim.SetTrigger("Attack");
                        }
                    }
                }
                // Rotation
                // ȸ�� ��
                float angle = Vector3.Angle(transform.forward, dir);
                // ȸ�� ����
                float rotDir = 1.0f;
                // ȸ�� ������ ����, ���� �����ʰ� �������� ����
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                {
                    // ���� ����
                    rotDir *= -1.0f;
                }
                // �����Ӵ� ȸ�� ��
                delta = rotSpeed * Time.deltaTime;

                if (angle < delta)
                {
                    delta = angle;
                }
                // ���� ȸ��
                transform.Rotate(transform.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }
}
