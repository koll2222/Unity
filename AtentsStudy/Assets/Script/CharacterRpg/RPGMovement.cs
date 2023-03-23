using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGMovement : RPGProperty
{

    protected void MoveToPos(Vector3 pos)
    {
        StopAllCoroutines();
        StartCoroutine(MovingToPos(pos));
    }
    IEnumerator MovingToPos(Vector3 pos)
    {
        // �������� ����
        Vector3 dir = pos - transform.position;
        // ������������ �Ÿ�
        float dist = dir.magnitude;
        // ����ȭ
        dir.Normalize();

        StartCoroutine(Rotating(dir));
        myAnim.SetBool("isRunning", true);

        while(dist > 0.0f)
        {
            float delta = MoveSpeed * Time.deltaTime;
            if(dist - delta < 0.0f)
            {
                delta = dist;
            }
            dist -= delta;
            transform.Translate(dir * delta, Space.World);
            yield return null;
        }
        myAnim.SetBool("isRunning", false);
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
}
