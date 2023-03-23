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
        // ����Ʈ�� Ÿ���� �ٶ󺸰� ���ְ� �;��µ� ������

        //float angle = Vector3.Angle(transform.forward, Target.transform.position.normalized);
        if (moveEff == null)
            moveEff = Instantiate(MovingEffect, transform.position, Quaternion.identity);
        moveEff.transform.SetParent(transform);
        //moveEff.transform.Rotate(Vector3.up * angle * 1.0f);
        Vector3 dir = Target.transform.position - pos;      // pos~target �� ����
        float dist = dir.magnitude;                         // pos~target ������ �Ÿ�
        dir.Normalize();                                    // ����ȭ



        while (dist > 0.0f) // ������ �Ÿ��� ���Ҵٸ�
        {
            float delta = MoveSpeed * Time.deltaTime;               // �ʴ� �̵��Ÿ�
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

