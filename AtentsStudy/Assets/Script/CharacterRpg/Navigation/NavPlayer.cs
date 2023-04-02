using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class NavPlayer : RPGProperty
{

    public NavMeshAgent myNav;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        myAnim.SetFloat("Speed", myNav.velocity.magnitude / myNav.speed );
    }

    public void OnWarp(Vector3 pos)
    {
        myNav.Warp(pos);
    }

    public void OnMove(Vector3 pos)
    {
        //StartCoroutine(Moving(pos));
        //myNav.SetDestination(pos);
        if (myAnim.GetBool("isAir")) return;
        StopAllCoroutines();
        StartCoroutine(JumpableMoving(pos));
    }
    IEnumerator Moving(Vector3 pos)
    {
        // navigation���� ������
        myNav.SetDestination(pos);
        // �����̴� �ִϸ��̼�
        myAnim.SetBool("isMoving", true);
        // myNav.pathPending : SetDestination�� �ڷ�ƾ���� ���ư��⿡ ���������� �ƴ��� �Ǻ��� �ʿ�, ������ true �� false
        // || ���� �Ÿ� > ���� �Ÿ�?
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            yield return null;
        }
        myAnim.SetBool("isMoving", false);
    }

    IEnumerator JumpableMoving(Vector3 pos)
    {
        myNav.SetDestination(pos);
        while (myNav.pathPending || myNav.remainingDistance > myNav.stoppingDistance)
        {
            // OffMeshLink�� Ʈ����ó�� �ش� ������ ���� �νĵ�
            if (myNav.isOnOffMeshLink)
            {
                myAnim.SetBool("isAir", true);
                // ���� ���ߴ°� �ƴ� pause ��
                myNav.isStopped = true;
                // ���� ����
                Vector3 endPos = myNav.currentOffMeshLinkData.endPos;
                Vector3 dir = endPos - transform.position;
                float dist = dir.magnitude;
                dir.Normalize();

                while(dist > 0.0f)
                {
                    float delta = myNav.speed * Time.deltaTime;
                    if(dist - delta < 0.0f)
                    {
                        delta = dist;
                    }
                    dist -= delta;
                    transform.Translate(dir * delta, Space.World);
                    
                    yield return null;
                }
                myAnim.SetBool("isAir", false);
                myNav.CompleteOffMeshLink();
                myNav.isStopped = false;
                //myNav.velocity = dir * myNav.speed;

            }
            yield return null;
        }
    }
    
}
