using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RPGProperty : MonoBehaviour
{
    // ����
    public UnityAction DeathAlarm;

    // �̵� �ӵ�
    public float MoveSpeed = 5.0f;
    // ȸ�� �ӵ�
    public float rotSpeed = 360.0f;
    // ���� ����
    public float AttackRange = 1.3f;
    // ���� ������
    public float AttackDelay = 1.0f;
    // �����̸� �˻��� ����
    protected float playTime = 0.0f;
    // �ִ� ü��
    public float MaxHp = 100.0f;
    // ���� ü��
    float _curHp = -100.0f;
    // ���ݷ�
    public float AttackPoint = 35.0f;

    public UnityEvent<float> updateHp;

    // ���� ü�� �����ϴ� property
    protected float curHp
    {
        get
        {
            // _curHp�� �ʱⰪ�� ����, MaxHp ����
            if (_curHp < 0.0f) _curHp = MaxHp;
            return _curHp;
        }
        // _curHp ���� ���� ����
        set
        {
            _curHp = Mathf.Clamp(value, 0.0f, MaxHp);
            updateHp?.Invoke(Mathf.Approximately(MaxHp,0.0f)? 0.0f : _curHp / MaxHp);
        }
    }

    Animator anim = null;
    protected Animator myAnim
    {
        get
        {
            if(anim == null)        // anim�� �������� ������
            {
                anim = GetComponent<Animator>();                // Animator ������Ʈ�� ������
                if(anim == null)                                // �׷��� ������
                {
                    anim = GetComponentInChildren<Animator>();  // �ڽĵ鿡�� ������
                }
            }
            return anim;
        }
    }
}
