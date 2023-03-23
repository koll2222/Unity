using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : CharacterProperty
{
    protected void MoveToPos(Vector3 pos)
    {
        StopAllCoroutines();
        StartCoroutine(MovingToPos(pos));
    }
    IEnumerator MovingToPos(Vector3 pos)
    {
        Vector3 dir = pos - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();

        StartCoroutine(Rotating(dir));

        //Animator에 있는 Parameter 값을 설정 가능
        myAnim.SetBool("isMoving", true);

        while(dist > 0.0f)
        {
            float delta = MoveSpeed * Time.deltaTime;
            
            if(dist - delta < 0.0f)
                delta = dist;
            
            dist -= delta;
            transform.Translate(dir * delta, Space.World);

            yield return null;
        }
        myAnim.SetBool("isMoving", false);
    }
    IEnumerator Rotating(Vector3 dir)
    {
        float angle = Vector3.Angle(transform.forward, dir);
        float rotDir = 1.0f;

        if(Vector3.Dot(transform.forward, dir) < 0.0f)
        {
            rotDir *= -1.0f;
        }

        while(angle > 0.0f)
        {
            float delta = RotSpeed * Time.deltaTime;
            if(angle - delta < 0.0f)
            {
                delta = angle;
            }
            angle -= delta;
            transform.Rotate(Vector3.up, rotDir * delta);

            yield return null;
        }

    }
}
