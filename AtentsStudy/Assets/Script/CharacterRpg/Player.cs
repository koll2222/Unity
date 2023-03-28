using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RPGBattle에 IBattle 인터페이스 상속
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
    // 인터페이스 함수 정의
    public void OnDamage(float dmg)
    {
        myAnim.SetTrigger("Damage");
    }
    // 배틀 시작
    public void BeginBattle(Transform target)
    {
        myTarget = target;
        FollowTarget(myTarget);
    }
}
