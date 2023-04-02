using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RPGBattle에 IBattle 인터페이스 상속
public class Player : RPGMovement, IBattle
{
    Transform myTarget = null;

    public bool IsLive
    {
        get => !Mathf.Approximately(curHp, 0.0f);
    }

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
        if(IsLive) MoveToPos(pos);
    }
    public void OnAttack()
    {
        // myTarget의 IBattle 컴포넌트가 null 값이 아니라면 OnDamage 호출
        if(IsLive) myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }
    // 인터페이스 함수 정의
    public void OnDamage(float dmg)
    {
        if (!IsLive) return;
        curHp -= dmg;
        if (Mathf.Approximately(curHp, 0.0f))
        {
            // 사망시 Collider 비활성
            // transform.GetComponent<Collider>().enabled = false;
            Collider[] list = transform.GetComponentsInChildren<Collider>();
            foreach (Collider col in list) col.enabled = false;
            DeathAlarm?.Invoke();
            myAnim.SetTrigger("Dead");
        }
        else
            myAnim.SetTrigger("Damage");
    }
    // 배틀 시작
    public void BeginBattle(Transform target)
    {
        if(myTarget != null)
        {
            myTarget.GetComponent<RPGProperty>().DeathAlarm -= TargetDead;
        }
        myTarget = target;
        FollowTarget(myTarget);
        // delegate는 함수를 누적할 수 있음
        myTarget.GetComponent<RPGProperty>().DeathAlarm += TargetDead;
    }
    void TargetDead()
    {
        myTarget = null;
        StopAllCoroutines();
    }
}
