using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CharacterMovement2D : CharacterProperty2D
{
    public float defaultForward = 1.0f;
    float dropHeight = 0.0f;
    float dropDist = -1.0f;
    bool isDown = false;
    Coroutine coJump = null;
    public LayerMask crashMask;
    protected void AirCheck()
    {
        Vector2 orgPos = transform.position + Vector3.up * 0.05f;
        Vector2 dir = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(orgPos, dir, 0.1f, crashMask);
        Debug.DrawLine(orgPos, orgPos + dir * 1.0f, Color.green);
        if (hit.collider != null && dropDist <= 0.0f)
        {
            if (isDown)
            {
                if (coJump != null) StopCoroutine(coJump);
                transform.position = hit.point;
            }
            myAnim.SetBool("isAir", false);
        }
        else
        {
            myAnim.SetBool("isAir", true);
            float delta = 9.8f * Time.deltaTime;
            transform.position += Vector3.down * delta;

            if (dropDist > 0.0f)
            {
                float dist = dropHeight - transform.position.y;
                if(dist > dropDist)
                {
                    dropDist = -1.0f;
                }
            }
        }
    }
    protected void Jump(float totalTime, float maxHeight)
    {
        if (coJump != null) StopCoroutine(coJump);
        coJump = StartCoroutine(Jumping(totalTime, maxHeight));
    }
    IEnumerator Jumping(float totalTime, float maxHeight)
    {
        isDown = false;
        myAnim.SetTrigger("Jump");
        float t = 0.0f;
        float orgHeight = transform.position.y;
        while (t <= totalTime)
        {
            if (t >= totalTime * 0.5f) isDown = true;
            t += Time.deltaTime;
            float h = Mathf.Sin((t / totalTime) * Mathf.PI) * maxHeight;

            Vector3 pos = new Vector3(transform.position.x, orgHeight + h, transform.position.z);
            if (isDown)
            {
                RaycastHit2D hit =
                    Physics2D.Raycast(transform.position, Vector2.down, Vector3.Distance(transform.position, pos), crashMask);
                if (hit.collider != null)
                {
                    transform.position = hit.point;
                    yield break;
                }
            }

            transform.position = pos;
            yield return null;
        }
        transform.position = new Vector3(transform.position.x, orgHeight, transform.position.z);
    }
    protected void Drop()
    {
        Jump(0.25f, 0.5f);
        dropHeight = transform.position.y;
        dropDist = 1.5f;
    }
    protected void MoveByDireciton(Vector2 dir, UnityAction done = null)
    {
        StartCoroutine(MovingByDirection(dir,done));
    }

    IEnumerator MovingByDirection(Vector2 dir, UnityAction done)
    {
        bool cliff = false;
        myAnim.SetBool("isMoving", true);
        while(!cliff)
        {
            Vector3 pos = transform.position + (Vector3)dir * MoveSpeed * Time.deltaTime;

            cliff = checkCliff(pos + Vector3.up * 0.5f);
            if (cliff)
            {
                break;
            }
            transform.position = pos;

            yield return null;
        }
        myAnim.SetBool("isMoving", false);
        done?.Invoke();
    }

    bool checkCliff(Vector2 pos)
    {
        RaycastHit2D hit = Physics2D.Raycast(pos, Vector2.down, 1f, crashMask);
        if(hit.collider == null)
        {
            return true;
        }
        return false;
    }

    protected void Turn()
    {
        myRenderer.flipX = !myRenderer.flipX;
    }

    protected Vector3 Forward()
    {
        return myRenderer.flipX ? transform.right * defaultForward : -transform.right * defaultForward;
    }

    IEnumerator Attacking(Transform target)
    {
        while (target != null)
        {
            playTime += Time.deltaTime;
            Vector3 dir = target.position - transform.position;
            if (dir.magnitude <= AttackRange)
            {
                if(playTime >= AttackDelay)
                {
                    myAnim.SetTrigger("Attack");
                    playTime = 0.0f;
                }
            }
            else
            {
                dir.y = 0.0f;
                float dist = dir.magnitude - AttackRange;
                dir.Normalize();
                float delta = MoveSpeed * Time.deltaTime;
                if (delta > dist)
                {
                    delta = dist;
                }
                myRenderer.flipX = dir.x > 0.0f ? true : false;
                transform.Translate(dir * delta, Space.World);
            }
            
            yield return null;
        }
    }
    protected void Attack(Transform target)
    {
        StartCoroutine(Attacking(target));
    }
}
