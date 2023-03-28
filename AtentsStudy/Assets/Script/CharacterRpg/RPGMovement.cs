using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RPGMovement : RPGProperty
{

    protected void MoveToPos(Vector3 pos, UnityAction done = null)
    {
        StopAllCoroutines();
        StartCoroutine(MovingToPos(pos, done));
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

    /* ����� ����ٴϰ� �� �ڷ�ƾ
     * Transform�� �޾ƿ��� ���� :
     * position ���� �����̶� �ڷ�ƾ ������ ���� ������ ����
     * Transform�� �������̶� �ڷ�ƾ ���� �߿��� ���� ���ϰ� ��.
     * �����̴� ����� ����ٴϰ� �� �����̸� �̰� �´�
     */
    IEnumerator FollowingTarget(Transform target)
    {
        while (target != null)
        {
            if (!myAnim.GetBool("isAttacking"))
            {
                myAnim.SetBool("isMoving", true);
                // �� -> Ÿ�������� ���� ���Ͱ�
                Vector3 dir = target.position - transform.position;
                // �� -> Ÿ�ٱ����� �Ÿ�
                float dist = dir.magnitude;
                // ���� ���Ͱ� ����ȭ
                dir.Normalize();
                // �����Ӵ� �̵� �Ÿ�
                float delta = MoveSpeed * Time.deltaTime;

                if (dist < delta)
                {
                    delta = dist;
                }
                transform.Translate(dir * delta, Space.World);

                // Rotation
                // ȸ�� ��
                float angle = Vector3.Angle(transform.forward, dir);
                // ȸ�� ����
                float rotDir = 1.0f;
                // ȸ�� ������ ����, ���� �����ʰ� �������� ����
                if (Vector3.Dot(transform.right, dir) < 0.0f)
                {
                    rotDir *= -1.0f;
                }
                delta = rotSpeed * Time.deltaTime;

                if (angle < delta)
                {
                    delta = angle;
                }
                transform.Rotate(transform.up * rotDir * delta, Space.World);
            }
            yield return null;
        }
    }
}
