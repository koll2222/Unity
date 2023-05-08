using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Monster : RPGMovement, IPerception, IBattle
{
    public Transform myHeadPoint;
    public static int TotalCount = 3;
    public bool IsLive
    {
        get => myState != State.Death;
    }

    // 
    public enum State
    {
        Create, Normal, Battle, Death
    }
    public State myState = State.Create;

    Vector3 orgPos;

    public Transform myTarget = null;

    UnityAction deadAction = null;

    void ChangeState(State s)
    {
        if (myState == s) return;   //���� ���¶� �ٲ� ���¶� ������ �Լ� ����.
        myState = s;
        switch (myState)
        {
            case State.Normal:
                // Move �ִϸ��̼� ����
                myAnim.SetBool("isMoving", false);
                StopAllCoroutines();
                StartCoroutine(Roaming(Random.Range(1.0f, 3.0f)));
                break;
            case State.Battle:
                StopAllCoroutines();
                FollowTarget(myTarget);
                break;
            case State.Death:
                //transform.GetComponent<Collider>().enabled = false;
                Collider[] list = transform.GetComponentsInChildren<Collider>();
                foreach (Collider col in list) col.enabled = false;
                DeathAlarm?.Invoke();
                StopAllCoroutines();
                myAnim.SetTrigger("Dead");
                myAnim.SetBool("isDead", true);
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
            case State.Death:
                break;
            default:
                Debug.Log("���� ó�� ����");
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        TotalCount++;
        orgPos = transform.position;
        ChangeState(State.Normal);
        if (SceneData.Inst != null)
        {
            HpBarUI hpUi = (Instantiate(Resources.Load("RPG/HpBar"), SceneData.Inst.hpBars) as GameObject).GetComponent<HpBarUI>();
            //Canvas canvas = FindObjectOfType<Canvas>();
            //GameObject obj = GameObject.Find("Canvas");
            hpUi.myRoot = myHeadPoint;
            updateHp.AddListener(hpUi.updateHp);
            deadAction += () => Destroy(hpUi.gameObject);

            MiniMapIcon icon = (Instantiate(Resources.Load("RPG/MiniMapIcon"), SceneData.Inst.miniMap) as GameObject).GetComponent<MiniMapIcon>();

            icon.Initailize(transform, Color.red);
            deadAction += () => Destroy(icon.gameObject);
        }
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
        MoveToPos(pos, () => StartCoroutine(Roaming(Random.Range(1.0f, 3.0f))));
    }

    // ����ٴ� Ÿ�� Ž��
    public void Find(Transform target)
    {
        // ȣ��� �� ���� ���� transform �� �Ű������� myTarget�� ����
        myTarget = target;
        myTarget.GetComponent<RPGProperty>().DeathAlarm += () => { if (IsLive) ChangeState(State.Normal); };
        ChangeState(State.Battle);
    }

    // Ÿ���� ����
    public void LostTarget()
    {
        myTarget = null;
        ChangeState(State.Normal);
    }

    public void OnAttack()
    {
        // myTarget�� IBattle ������Ʈ�� null�� �ƴ϶��
        myTarget.GetComponent<IBattle>()?.OnDamage(AttackPoint);
    }
    public void OnDamage(float dmg)
    {
        curHp -= dmg;

        if (Mathf.Approximately(curHp, 0.0f))
            ChangeState(State.Death);
        else
            myAnim.SetTrigger("Damage");
    }

    public void OnDisappear()
    {
        StartCoroutine(Disappearing());
    }
    // ����� ����
    IEnumerator Disappearing()
    {
        yield return new WaitForSeconds(3.0f);
        float dist = 0.0f;
        while (dist < 1.0f)
        {
            dist += Time.deltaTime;
            transform.Translate(Vector3.down * Time.deltaTime);
            yield return null;
        }
        deadAction?.Invoke();
        Destroy(gameObject);
        TotalCount--;
    }
}
