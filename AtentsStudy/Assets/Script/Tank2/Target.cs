using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    //���� ������ �̿��� �¿�� �����̰� �����

    //float Range = 2.0f;         //Ÿ���� �����̴� ����
    //Vector3 startPos, destPos;  //���� ����, ���� ����
    //float t = 0.0f;             //���� �ð��� ���� �� ���� [0~1]
    //float Dir = 1.0f;           //���� ���� ���� ���� 1�̸� ���� -1�̸� ����

    //�ڷ�ƾ�� �����Ͽ� ���߰� �ϱ� ���� Coroutine�� �����ϴ� ���� ����
    Coroutine move = null;

    public GameObject destroyEffect = null;
    
    // Start is called before the first frame update
    void Start()
    {
        //t = 0.5f;   //�߾ӿ� ��ġ
        //destPos = startPos = transform.position;
        //startPos.x -= Range / 2.0f;     //��ü ���� ������ ���� ����????
        //destPos.x += Range / 2.0f;      //

        //�ڷ�ƾ �Լ��� ���� ��� StartCoroutine("�Լ���"())
        //move = StartCoroutine(Moving());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.F1))
        {
            //Moving()�� ���ߴ� �Լ�
            StopCoroutine(move);
        }

        //t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f);
        ////Mathf.Approximately(t,0.0f) t�� 0.0f�� �ٻ��ϴٸ�
        //if (Mathf.Approximately(t,0.0f) || Mathf.Approximately(t, 1.0f))
        //{
        //    Dir *= -1.0f;
        //}
        //// Lerp(a,b,t) t �� �ð��� 0~1
        //transform.position = Vector3.Lerp(startPos, destPos, t);
        ////�� �ڵ带 �ڷ�ƾ?���� �����غ�
    }
    bool isQuitting = false;
    //����� �� ȣ��
    private void OnApplicationQuit()
    {
        isQuitting = true;
    }
    //�ı��� �� ����, ���α׷��� ����� ���� ������ ��
    private void OnDestroy()
    {
        //������ �� ��ġ���� ȸ������ �־��� �� ����, Quaternion.identity : ȸ������ 0��
        if (!isQuitting) Instantiate(destroyEffect, transform.position, Quaternion.identity);
    }
    //�ڷ�ƾ �Լ��� ��� ���� ���� �Լ�, ing�� �ٿ��ָ� ���߿� ���� ����
    IEnumerator Moving()
    {
        /* �Ϲ����� �Լ��� return�� �Լ� ����, �ڷ�ƾ �Լ��� yield return���� ��ȯ�ص� ������� ����, {}��� �������� ������ ����
         * �ڷ�ƾ �Լ��� ȣ�� �� yield return �� �ش� �����ӿ����� ����, ���� �����ӿ��� yield return �������� �ٽ� ���� ��
         * ������� �ʴ� �Լ��̱⿡ �ѹ��� �����ϸ� ��. ��, Update()������ ������ ����ȭ�� ������ �� �� ����.
         * �ý��ۻ� ������ �������� ������ �ڵ��� �������� ������ ����� ���� ��.
         */

        //
        float Range = 2.0f;         //Ÿ���� �����̴� ����
        Vector3 startPos, destPos;  //���� ����, ���� ����
        float t = 0.0f;             //���� �ð��� ���� �� ���� [0~1]
        float Dir = 1.0f;           //���� ���� ���� ���� 1�̸� ���� -1�̸� ����

        t = 0.5f;   //�߾ӿ� ��ġ
        destPos = startPos = transform.position;
        startPos.x -= Range / 2.0f;     //��ü ���� ������ ���� ����????
        destPos.x += Range / 2.0f;      //

        //�Ϲ������� while(true)�� ���� ������ ��������, �ڷ�ƾ������ yield return�� �ֱ⿡ �ش� �����ӿ��� ���ᰡ �Ǿ� ���� ������ ������ ����
        while (true)
        {
            t = Mathf.Clamp(t + Dir * Time.deltaTime, 0.0f, 1.0f);
            //Mathf.Approximately(t,0.0f) t�� 0.0f�� �ٻ��ϴٸ�
            if (Mathf.Approximately(t, 0.0f) || Mathf.Approximately(t, 1.0f))
            {
                Dir *= -1.0f;
            }
            // Lerp(a,b,t) t �� �ð��� 0~1
            transform.position = Vector3.Lerp(startPos, destPos, t);

            yield return null;  // IEnumerator�� ��ȯ ����
        }
    }
}
