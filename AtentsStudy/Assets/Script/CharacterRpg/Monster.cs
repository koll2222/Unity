using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : RPGMovement, IPerception, IBattle
{
    public enum State
    {
        Create, Normal, Battle, Death
    }
    public State myState = State.Create;

    Vector3 orgPos;

    public Transform myTarget = null;

    void ChangeState(State s)
    {
        if (myState == s) return;   //현재 상태랑 바뀔 상태랑 같으면 함수 종료.
        myState = s;
        switch (myState)
        {
            case State.Normal:
                // Move 애니메이션 중지
                myAnim.SetBool("isMoving", false);
                StopAllCoroutines();
                StartCoroutine(Roaming(Random.Range(1.0f,3.0f)));
                break;
            case State.Battle:
                StopAllCoroutines();
                FollowTarget(myTarget);
                break;
            case State.Death:
                StopAllCoroutines();
                myAnim.SetBool("isLive", false);
                myAnim.SetTrigger("Dying");
                break;
            default:
                Debug.Log("상태 처리 에러");
                break;
        }
    }
    void StateProcess()
    {
        switch (myState)
        {
            case State.Normal:
                break;
            case State.Battle:
                break;
            case State.Death:
                break;
            default:
                Debug.Log("상태 처리 에러");
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
        myAnim.SetBool("isLive", true);
        ChangeState(State.Normal);
    }

    // Update is called once per frame
    void Update()
    {
        StateProcess();
    }

    IEnumerator Roaming(float delay)
    {
        yield return new WaitForSeconds(delay);
        Vector3 pos = orgPos;
        pos.x += Random.Range(-4.0f, 4.0f);
        pos.z += Random.Range(-4.0f, 4.0f);
        MoveToPos(pos, ()=> StartCoroutine(Roaming(Random.Range(1.0f,3.0f))));
    }

    // 따라다닐 타겟 탐색
    public void Find(Transform target)
    {
        if (myAnim.GetBool("isLive"))
        {
            // 호출될 때 들어온 적의 transform 값 매개변수를 myTarget에 대입
            myTarget = target;
            ChangeState(State.Battle);
        }
    }

    // 타겟이 없음
    public void LostTarget()
    {
        if (myAnim.GetBool("isLive"))
        {
            myTarget = null;
            ChangeState(State.Normal);
        }
    }

    public void OnAttack()
    {
        // myTarget이 IBattle 컴포넌트가 null이 아니라면
        myTarget.GetComponent<IBattle>()?.OnDamage(0.0f);
    }
    public void OnDamage(float dmg)
    {
        myHp -= dmg;
        if (myHp <= 0.0f)
        {
            ChangeState(State.Death);
            transform.GetComponent<Collider>().enabled = false;
            return;
        }
        myAnim.SetTrigger("Damage");
    }
}
