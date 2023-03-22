using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public GameObject Target = null;
    public float MoveSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(MovingToEarth(transform.position));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MovingToEarth(Vector3 pos)
    {
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
}
