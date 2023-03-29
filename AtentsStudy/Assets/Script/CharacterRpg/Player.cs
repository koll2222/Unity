using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// RPGBattle�� IBattle �������̽� ���
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
        // myTarget�� IBattle ������Ʈ�� null ���� �ƴ϶�� OnDamage ȣ��
        if(IsLive) myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }
    // �������̽� �Լ� ����
    public void OnDamage(float dmg)
    {
        if (!IsLive) return;
        curHp -= dmg;
        if (Mathf.Approximately(curHp, 0.0f))
        {
            // ����� Collider ��Ȱ��
            // transform.GetComponent<Collider>().enabled = false;
            Collider[] list = transform.GetComponentsInChildren<Collider>();
            foreach (Collider col in list) col.enabled = false;
            DeathAlarm?.Invoke();
            myAnim.SetTrigger("Dead");
        }
        else
            myAnim.SetTrigger("Damage");
    }
    // ��Ʋ ����
    public void BeginBattle(Transform target)
    {
        if(myTarget != null)
        {
            myTarget.GetComponent<RPGProperty>().DeathAlarm -= TargetDead;
        }
        myTarget = target;
        FollowTarget(myTarget);
        // delegate�� �Լ��� ������ �� ����
        myTarget.GetComponent<RPGProperty>().DeathAlarm += TargetDead;
    }
    void TargetDead()
    {
        myTarget = null;
        StopAllCoroutines();
    }
}
