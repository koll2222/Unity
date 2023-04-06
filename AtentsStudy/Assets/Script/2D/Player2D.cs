using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2D : CharacterMovement2D, IBattle
{
    public LayerMask enemyMask;
    public void OnDamage(float dmg)
    {
        myAnim.SetTrigger("Damage");
    }
    public bool IsLive
    {
        get
        {
            return true;
        }
    }
    public void OnAttack()
    {
        Collider2D[] list = Physics2D.OverlapCircleAll(transform.position + Forward() * 0.5f, 0.5f, enemyMask);
        foreach(Collider2D col in list)
        {
            col.transform.GetComponent<IBattle>()?.OnDamage(AttackPoint);
        }
    }

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
        dir.x = Input.GetAxisRaw("Horizontal");

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

        transform.Translate(dir * MoveSpeed * Time.deltaTime);


        if(Input.GetKeyDown(KeyCode.X))
        {
            myAnim.SetTrigger("Attack");
        }

        if(Input.GetKeyDown(KeyCode.C) && !myAnim.GetBool("isAir"))
        {
            //myRigid2D.AddForce(Vector2.up * 300.0f);
            Jump(1.0f, 3.0f);
        }

        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            Drop();
        }
    }
}
