using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RPGBattle�� IBattle �������̽� ���
public class Player : RPGMovement, IBattle
{
    Transform myTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myAnim.SetTrigger("Attack");
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            myAnim.SetTrigger("Skill_W");
        }
    }

    public void OnMove(Vector3 pos)
    {
        MoveToPos(pos);
    }
    public void OnAttack()
    {
        myTarget.GetComponent<IBattle>()?.OnDamage(myPower);
    }
    // �������̽� �Լ� ����
    public void OnDamage(float dmg)
    {
        myAnim.SetTrigger("Damage");
    }
    // ��Ʋ ����
    public void BeginBattle(Transform target)
    {
        myTarget = target;
        FollowTarget(myTarget);
    }
}
