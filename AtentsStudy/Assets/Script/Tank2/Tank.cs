using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    /* Unity Inspector Transform�� ���̴� Position, Rotation �� ���� ����
     * Rotation LocalRotation
     * 
     */

    public float Speed = 1.0f;
    public float Speed_Rotation = 180f;
    public float Speed_Rotation_Top = 90f;
    public float Speed_Rotation_Cannon = 90f;
    public Transform myCannon = null;
    public Transform myTop = null;
    public Transform myMuzzle = null;
    public Transform myAura = null;
    public Transform myTopEffect = null;
    public Bomb myBomb = null;
    public GameObject orgBomb = null;   //���� Bomb�� �����ϱ� ���� ������ ����, Prefab Bomb�� �����ص�.
    public GameObject auraEffect = null;
    public GameObject topEffect = null;
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(auraEffect, myAura.position, Quaternion.identity, myAura);
        //moveEf.transform.SetParent(myAura);
        Instantiate(topEffect, myTopEffect.position, myTopEffect.rotation, myTopEffect);
        //topEf.transform.SetParent(myTopEffect);
    }

    // Update is called once per frame
    void Update()
    {
        //��ü
        if (Input.GetKey(KeyCode.W))    //����
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.S))    //����
        {
            transform.Translate(Vector3.back * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.A))    //���� ȸ��
        {
            transform.Rotate(Vector3.down * Speed_Rotation * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))    //������ ȸ��
        {
            transform.Rotate(Vector3.up * Speed_Rotation * Time.deltaTime);
        }

        //��ž
        if (Input.GetKey(KeyCode.LeftArrow))    //��ž ���� ȸ��
        {
            myTop.Rotate(Vector3.down * Speed_Rotation_Top * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))   //��ž ������ ȸ��
        {
            myTop.Rotate(Vector3.up * Speed_Rotation_Top * Time.deltaTime);
        }

        //����
        if (Input.GetKey(KeyCode.UpArrow))  // ���� ���� ȸ��
        {
            myCannon.Rotate(Vector3.left * Speed_Rotation_Cannon * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))    //���� �Ʒ��� ȸ��
        {
            myCannon.Rotate(Vector3.right * Speed_Rotation_Cannon * Time.deltaTime);
        }

        //eulerAngels�� localRotation�� �޾ƿ�
        //Quaternion�� euler�� ��ȯ �� 0~360�� ������ ���� ����, -180~180���� �ƴϴ�.
        Vector3 angle = myCannon.localRotation.eulerAngles;

        //Inspector�� -180~180 ����
        //euler�� ��ȯ�� ���� 180�� �Ѿ�� -180~180 �������� �ٲ��ִ� ��
        if (angle.x > 180.0f)
        {
            angle.x -= 360.0f;
        }

        //if (angle.x > 15.0f)
        //{
        //    angle.x = 15.0f;
        //}
        //if (angle.x < -60.0f)
        //{
        //    angle.x = -60.0f;
        //}
        //���� ���� ����� �ϴ� �Լ� Mathf.Clamp(����,Min,Max)
        angle.x = Mathf.Clamp(angle.x, -60.0f, 15.0f);
        myCannon.localRotation = Quaternion.Euler(angle);   //euler ������ ó�� �� Quaternion���� ��ȯ �� ������ ����� ��

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myBomb?.OnFire();
            myBomb = null;


            ////Instantiate(orgBomb) : orgBomb�� �����ؼ� �������
            //GameObject obj = Instantiate(orgBomb);

            ////���� ����� ������ �θ� ���� ����
            //obj.transform.SetParent(myMuzzle);
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localRotation = myMuzzle.localRotation;

            //GameObject obj = Instantiate(orgBomb, myMuzzle);
            GameObject obj = ObjectPool.Instance.GetObject<Bomb>(orgBomb, myMuzzle.position, myMuzzle.rotation, myMuzzle);

            // .GetComponent<T>() : T�� ����
            myBomb = obj.GetComponent<Bomb>();
            
        }

    }
}
