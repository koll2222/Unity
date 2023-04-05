using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void FixedUpdate()
    {
        AirCheck();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 dir = Vector2.zero;
        dir.x = Input.GetAxis("Horizontal");
        if (!Mathf.Approximately(dir.x, 0.0f))
        {
            myAnim.SetBool("isMoving", true);
            if (dir.x < 0.0f)
            {
                myRenderer.flipX = true;
            }
            else
            {
                myRenderer.flipX = false;
            }
        }
        else
        {
            myAnim.SetBool("isMoving", false);
        }
        if (!myAnim.GetBool("isAttacking"))
            transform.Translate(dir * MoveSpeed * Time.deltaTime,Space.World);

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(!myAnim.GetBool("isAttacking"))
                myAnim.SetTrigger("Attack");
        }
        // Á¡ÇÁ
        if (Input.GetKeyDown(KeyCode.C) && !myAnim.GetBool("isAir"))
        {
            //myRigid2D.AddForce(Vector2.up * 600f);
            if (coJump != null) StopCoroutine(coJump);
            coJump = StartCoroutine(Jumping(1.0f, 5.0f));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StartCoroutine(Droping());
        }

    }

    Coroutine coJump = null;
    bool isDown = false;
    IEnumerator Jumping(float totalTime, float maxHeight)
    {
        isDown = false;
        myAnim.SetTrigger("Jump");
        float t = 0.0f;
        float orgHeight = transform.position.y;

        while(t <= totalTime)
        {
            if (t >= totalTime * 0.5f) isDown = true;
            t += Time.deltaTime;
            // »ï°¢ÇÔ¼ö ÁøÂ¥ ½È´Ù. t : totalTime = y : Pi
            float h = Mathf.Sin((t/totalTime) * Mathf.PI) * maxHeight;

            Vector3 pos = new Vector3(transform.position.x, orgHeight + h, transform.position.z);
            if (isDown)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, Vector3.Distance(transform.position,pos), crashMask);
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

    float dropDist = 0.0f;
    IEnumerator Droping()
    {
        dropDist = 1.0f;


        yield return null;
    }

    public LayerMask crashMask;
    void AirCheck()
    {
        Vector2 orgPos = transform.position + Vector3.up * 0.05f;
        Vector2 dir = Vector2.down;

        RaycastHit2D hit = Physics2D.Raycast(orgPos, dir, 0.05f, crashMask);
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

            if(dropDist > 0.0f)
            {
                dropDist -= delta;
            }
        }
    }
}
