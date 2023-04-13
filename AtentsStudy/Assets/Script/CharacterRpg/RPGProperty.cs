using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RPGProperty : MonoBehaviour
{
    // 죽음
    public UnityAction DeathAlarm;

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
    // 최대 체력
    public float MaxHp = 100.0f;
    // 현재 체력
    float _curHp = -100.0f;
    // 공격력
    public float AttackPoint = 35.0f;

    public UnityEvent<float> updateHp;

    // 현재 체력 설정하는 property
    protected float curHp
    {
        get
        {
            // _curHp의 초기값이 음수, MaxHp 대입
            if (_curHp < 0.0f) _curHp = MaxHp;
            return _curHp;
        }
        // _curHp 값의 범위 지정
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
