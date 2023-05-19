using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInstancing : MonoBehaviour
{
    public GameObject orgObj;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < 100; i++)
        {
            Instantiate(orgObj, new Vector3(Random.Range(-5f,5f),Random.Range(-5f,5f), Random.Range(-5f,5f)), Quaternion.identity );
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
