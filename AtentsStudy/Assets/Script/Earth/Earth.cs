using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Earth : MonoBehaviour
{
    public LayerMask pickMask;
    public GameObject myEnemy = null;
    public Transform Orbit = null;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreatingEnemy());   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {

        }
    }
    Vector3 newEnemyPos;
    IEnumerator CreatingEnemy()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);

            float rndPos = Random.Range(10.0f, 50.0f);
            float rndPos2 = Random.Range(-50.0f, -10.0f);

            GameObject obj = Instantiate(myEnemy,Earth_orbit);
            newEnemyPos = transform.position;
            newEnemyPos.x = newEnemyPos.y = rndPos;
            obj.transform.position.Set(10, 10, 0);


        }
    }
   
}
