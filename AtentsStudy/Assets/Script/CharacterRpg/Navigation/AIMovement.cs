using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIMovement : RPGMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected void MoveByPath(Vector3[] pathList)
    {
        StopAllCoroutines();
        StartCoroutine(MovingByPath(pathList));
    }

    IEnumerator MovingByPath(Vector3[] pathList)
    {
        int i = 1;
        myAnim.SetFloat("Speed", 1f);
        while(i < pathList.Length)
        {
            bool done = false;
            MoveToPos(pathList[i], () => done = true);
            while (!done)
            {
                for(int n = i; n < pathList.Length; n++)
                {
                    Debug.DrawLine( n == i ? transform.position : pathList[n-1] , pathList[n], Color.green);
                }
                yield return null;
            }
            i++;
        }
        myAnim.SetFloat("Speed", 0f);
    }
}
