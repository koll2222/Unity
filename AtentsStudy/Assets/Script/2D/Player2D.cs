using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterProperty2D
{
    // Start is called before the first frame update
    void Start()
    {
        
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
            transform.Translate(dir * MoveSpeed * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.X))
        {
            if(!myAnim.GetBool("isAttacking"))
                myAnim.SetTrigger("Attack");
        }

        if (Input.GetKeyDown(KeyCode.C))
        {

        }

    }
}
