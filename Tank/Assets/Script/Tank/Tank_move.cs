using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank_move : MonoBehaviour
{
    public float move_forward_speed = 1.0f;
    public float turn_speed = 0.15f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //실습 - S를 누르면 뒤로 이동, A를 누르면 왼쪽으로 회전, D는 오른쪽으로 회전하게 만들어라
        if (Input.GetKey(KeyCode.W))    //앞으로 이동
        {
            //transform.forward - 월드의 방향 벡터값이기에 월드 기준으로 이동해야 함
            //transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

            //Vector3.forward == new Vector3(0, 0, 1) 로컬의 방향 벡터값임
            transform.Translate(Vector3.forward * move_forward_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))    //뒤로 이동
        {
            transform.Translate(-transform.forward * move_forward_speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))    //왼쪽 회전
        {
            transform.Rotate(-transform.up * 360f * turn_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))    //오른쪽 회전
        {
            transform.Rotate(transform.up * 360f * turn_speed * Time.deltaTime);
        }
    }
}
