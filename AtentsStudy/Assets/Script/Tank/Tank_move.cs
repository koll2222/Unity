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
        //�ǽ� - S�� ������ �ڷ� �̵�, A�� ������ �������� ȸ��, D�� ���������� ȸ���ϰ� ������
        if (Input.GetKey(KeyCode.W))    //������ �̵�
        {
            //transform.forward - ������ ���� ���Ͱ��̱⿡ ���� �������� �̵��ؾ� ��
            //transform.Translate(transform.forward * speed * Time.deltaTime, Space.World);

            //Vector3.forward == new Vector3(0, 0, 1) ������ ���� ���Ͱ���
            transform.Translate(Vector3.forward * move_forward_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))    //�ڷ� �̵�
        {
            transform.Translate(-transform.forward * move_forward_speed * Time.deltaTime, Space.World);
        }
        if (Input.GetKey(KeyCode.A))    //���� ȸ��
        {
            transform.Rotate(-transform.up * 360f * turn_speed * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))    //������ ȸ��
        {
            transform.Rotate(transform.up * 360f * turn_speed * Time.deltaTime);
        }
    }
}
