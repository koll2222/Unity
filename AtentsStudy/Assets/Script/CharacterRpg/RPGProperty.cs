using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGProperty : MonoBehaviour
{

    Animator anim = null;
    public float MoveSpeed = 5.0f;
    public float rotSpeed = 360.0f;

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
