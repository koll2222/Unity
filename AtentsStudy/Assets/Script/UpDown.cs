using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpDown : MonoBehaviour
{
    float a = 0.0f;
    float b = 0.0f;
    float curDist = 0.0f;   //curent distance
    float dist = 2.5f;
    Vector3 dir = new Vector3(0, 1, 0);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //���� 2.5���� �ö� �ڿ� �ٽ� �����ڸ��� ���ƿ��� �� �ݺ��ϰ� ���弼��.
        // #����� �Ÿ��� ������ �ؾ���. ��ġ�� Ǯ� �� ������ ����.
        {/*
            Vector3 dir = new Vector3(0,1,0);
            if (a < 2.5f)
            {
                a += dir.y * Time.deltaTime;
                b = a;
                transform.Translate(dir * Time.deltaTime);
            }
            else
            {
                b -= dir.y * Time.deltaTime;
                if (b <= 0)
                {
                    a = 0 - b;
                    return;
                }
                transform.Translate(-dir * Time.deltaTime);
            }*/
        }
        //����� Ǯ��
        {
            /*// �������� ������. ����ó���� �ؾ� ��.
            float delta = Time.deltaTime;

            //if (curDist + delta >= 2.5f) // �̵��� �Ÿ��� �̸� �̵��� �Ÿ��� ���ؼ� �˻�
            //{
            //    delta = 2.5f - curDist; // 2.5���� �̵��� �Ÿ���ŭ�� ���ָ� 2.5�� �����ϱ� ���� ���� �Ÿ��� ����
            //    dir = -dir;
            //    curDist = 0.0f;
            //    Debug.Log(transform.position.y);
            //}
            //else
            //{
            //    curDist += delta;
            //}
            //transform.Translate(dir * delta);

            // dir = -dir�� �ϰ� �̵��� ��Ű�� ���� �̵��Ÿ���ŭ �ݴ� �������� ���� �Ǵ� �� �ƴѰ�?? -�ش� ����-
            if (dist - delta <= 0.0f)   // ���� �̵��Ÿ��� �̵��� �Ÿ��� ���� �˻�
            {
                delta = dist;           // delta�� dist�� ����, dist�� ���� �̵��Ÿ��� ����Ǿ� ����.
                dist = 2.5f;            // ���� �̵��Ÿ� �ʱ�ȭ
                dir = -dir;
            }
            dist -= delta;
            transform.Translate(dir * delta);
            */
        }
        float delta = Time.deltaTime;
        if (dist - delta <= 0.0f)   // ���� �̵��Ÿ��� �̵��� �Ÿ��� ���� �˻�
        {
            delta = dist;           // delta�� dist�� ����, dist�� ���� �̵��Ÿ��� ����Ǿ� ����.
            dist = 2.5f;            // ���� �̵��Ÿ� �ʱ�ȭ
            transform.Translate(dir * delta,Space.World);
            dir = -dir;
            Debug.Log(transform.position.y);
        }
        else
        {
            dist -= delta;
            transform.Translate(dir * delta,Space.World);
        }

        //Rotate : ���� �������� ȸ���� ��Ŵ ���� dir�� y���� ������ y���� �������� ȸ��
        transform.Rotate(dir * 360.0f * Time.deltaTime,Space.World);

    }
}
