using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self : MonoBehaviour
{
    public Vector3 dir = new Vector3(0, 1, 0);
    public float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(360f * dir * Time.deltaTime / speed, Space.World);
    }
}
