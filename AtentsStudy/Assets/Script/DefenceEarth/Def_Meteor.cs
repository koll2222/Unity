using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Def_Meteor : MonoBehaviour
{
    public LayerMask crashMask;
    public Transform myTarget;
    public float Speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        myTarget = Def_Earth.Instance.myEarth;
        StartCoroutine(MovingToTarget());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator MovingToTarget()
    {
        Vector3 dir = myTarget.position - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        
        while(dist > 0.0f)
        {
            float delta = Speed * Time.deltaTime;
            if(dist - delta < 0.0f)
            {
                delta = dist;
            }
            transform.Translate(dir * delta);

            yield return null;
        }

    }
    private void OnTriggerEnter(Collider other)
    {
        if (((1 << other.gameObject.layer) & crashMask) != 0)
        {
            //Destroy(other.gameObject);
            StopAllCoroutines();
            Destroy(gameObject);
        }
    }
}
