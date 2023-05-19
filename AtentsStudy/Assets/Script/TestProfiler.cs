using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestProfiler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 150; i++)
        {
            for (int j = 0; j < 150; j++)
            {
                Vector3 dir1 = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                Vector3 dir2 = new Vector3(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
                float d = Vector3.Dot(dir1, dir2);
            }
        }
    }
}
