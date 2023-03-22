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
}
