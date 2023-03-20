using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon_move : MonoBehaviour
{
    public float speed = 0.03f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.UpArrow))
            transform.Rotate(-Vector3.right * 360f * speed * Time.deltaTime);
        if(Input.GetKey(KeyCode.DownArrow))
            transform.Rotate(Vector3.right * 360f * speed * Time.deltaTime);
    }
}
