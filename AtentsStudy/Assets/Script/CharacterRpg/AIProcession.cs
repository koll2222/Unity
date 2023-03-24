using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIProcession : MonoBehaviour
{
    public LayerMask enemyMask;
    public List<Transform> myEnemyList = new List<Transform>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            myEnemyList.Add(other.transform);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            if (myEnemyList.Contains(other.transform))
            {
                myEnemyList.Remove(other.transform);
            }
        }
    }
}
