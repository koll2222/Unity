using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGProperty : MonoBehaviour
{

    Animator anim = null;
    // 이동 속도
    public float MoveSpeed = 5.0f;
    // 회전 속도
    public float rotSpeed = 360.0f;
    // 공격 범위
    public float AttackRange = 1.3f;
    // 공격 딜레이
    public float AttackDelay = 1.0f;
    // 딜레이를 검사할 변수
    protected float playTime = 0.0f;
    // 체력
    public float myHp = 100.0f;
    // 공격력
    public float myPower = 35.0f;

    protected Animator myAnim
    {
        get
        {
            if(anim == null)        // anim에 참조값이 없으면
            {
                anim = GetComponent<Animator>();                // Animator 컴포넌트를 가져옴
                if(anim == null)                                // 그래도 없으면
                {
                    anim = GetComponentInChildren<Animator>();  // 자식들에서 가져옴
                }
            }
            return anim;
        }
    }
}
