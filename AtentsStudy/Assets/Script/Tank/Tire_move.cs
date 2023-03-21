using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tire_move : MonoBehaviour
{
    public float speed = 0.25f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
            transform.Rotate(-Vector3.up * (360.0f / speed) * Time.deltaTime);
        if (Input.GetKey(KeyCode.S))
            transform.Rotate(Vector3.up * (360.0f / speed) * Time.deltaTime);

        // 바퀴가 이상하게 돌아감..
        //if (Input.GetKey(KeyCode.A))
        //    transform.Rotate(-Vector3.right * (10.0f / speed) * Time.deltaTime);
        //if (Input.GetKey(KeyCode.D))
        //    transform.Rotate(Vector3.right * (10.0f / speed) * Time.deltaTime);

    }
}
