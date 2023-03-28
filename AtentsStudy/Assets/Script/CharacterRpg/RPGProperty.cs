using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGProperty : MonoBehaviour
{

    Animator anim = null;
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
    // ü��
    public float myHp = 100.0f;
    // ���ݷ�
    public float myPower = 35.0f;

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
