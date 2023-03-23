using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meteor : MonoBehaviour
{
    public GameObject Target = null;
    public GameObject OnHit = null;
    public GameObject MovingEffect = null;
    public LayerMask targetMask;
    public LayerMask boundaryMask;
    static public float MoveSpeed = 1.0f;
    GameObject moveEff = null;
    bool isOver = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovingToEarth(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        if (isOver)
        {
            Debug.Log("GameOver");
            isOver = false;
        }    
    }

    IEnumerator MovingToEarth(Vector3 pos)
    {
        // 이펙트를 타겟을 바라보게 해주고 싶었는데 실패함

        //float angle = Vector3.Angle(transform.forward, Target.transform.position.normalized);
        if (moveEff == null)
            moveEff = Instantiate(MovingEffect, transform.position, Quaternion.identity);
        moveEff.transform.SetParent(transform);
        //moveEff.transform.Rotate(Vector3.up * angle * 1.0f);
        Vector3 dir = Target.transform.position - pos;      // pos~target 의 방향
        float dist = dir.magnitude;                         // pos~target 까지의 거리
        dir.Normalize();                                    // 정규화



        while (dist > 0.0f) // 가야할 거리가 남았다면
        {
            float delta = MoveSpeed * Time.deltaTime;               // 초당 이동거리
            if(dist - delta < 0.0f)
            {
                delta = dist;
            }
            transform.Translate(dir * delta, Space.World);
            
            dist -= delta;

            yield return null;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (((1 >> other.transform.gameObject.layer) & targetMask) != 0)
        {
            Instantiate(OnHit, other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
        }
        else
            Debug.Log(other.transform.gameObject.layer);
    }

    //Instantiate(OnHit, other.transform.position, Quaternion.identity);
    //Destroy(other.gameObject);
}

