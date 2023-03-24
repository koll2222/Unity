using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : RPGMovement
{
    
    public enum State
    {
        Create, Normal, Battle
    }
    public State myState = State.Create;

    Vector3 orgPos;

    void ChangeState(State s)
    {
        if (myState == s) return;   //���� ���¶� �ٲ� ���¶� ������ �Լ� ����.
        myState = s;
        switch (myState)
        {
            case State.Normal:
                StartCoroutine(Roaming(Random.Range(1.0f,3.0f)));
                break;
            case State.Battle:
                break;
            default:
                Debug.Log("���� ó�� ����");
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
            default:
                Debug.Log("���� ó�� ����");
                break;
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        orgPos = transform.position;
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

}
