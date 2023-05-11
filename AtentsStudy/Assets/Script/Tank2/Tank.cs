using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tank : MonoBehaviour
{
    /* Unity Inspector Transform에 보이는 Position, Rotation 은 로컬 값임
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
    public GameObject orgBomb = null;   //원본 Bomb을 참조하기 위한 참조형 변수, Prefab Bomb을 참조해둠.
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
        //몸체
        if (Input.GetKey(KeyCode.W))    //전진
        {
            transform.Translate(Vector3.forward * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.S))    //후진
        {
            transform.Translate(Vector3.back * Time.deltaTime * Speed);
        }
        if (Input.GetKey(KeyCode.A))    //왼쪽 회전
        {
            transform.Rotate(Vector3.down * Speed_Rotation * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))    //오른쪽 회전
        {
            transform.Rotate(Vector3.up * Speed_Rotation * Time.deltaTime);
        }

        //포탑
        if (Input.GetKey(KeyCode.LeftArrow))    //포탑 왼쪽 회전
        {
            myTop.Rotate(Vector3.down * Speed_Rotation_Top * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.RightArrow))   //포탑 오른쪽 회전
        {
            myTop.Rotate(Vector3.up * Speed_Rotation_Top * Time.deltaTime);
        }

        //포신
        if (Input.GetKey(KeyCode.UpArrow))  // 포신 위쪽 회전
        {
            myCannon.Rotate(Vector3.left * Speed_Rotation_Cannon * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.DownArrow))    //포신 아래쪽 회전
        {
            myCannon.Rotate(Vector3.right * Speed_Rotation_Cannon * Time.deltaTime);
        }

        //eulerAngels로 localRotation을 받아옴
        //Quaternion을 euler로 변환 시 0~360도 사이의 값이 나옴, -180~180도가 아니다.
        Vector3 angle = myCannon.localRotation.eulerAngles;

        //Inspector는 -180~180 기준
        //euler로 변환한 값이 180을 넘어가면 -180~180 기준으로 바꿔주는 식
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
        //위와 같은 기능을 하는 함수 Mathf.Clamp(변수,Min,Max)
        angle.x = Mathf.Clamp(angle.x, -60.0f, 15.0f);
        myCannon.localRotation = Quaternion.Euler(angle);   //euler 값으로 처리 후 Quaternion으로 변환 후 대입을 해줘야 함

        if (Input.GetKeyDown(KeyCode.Space))
        {
            myBomb?.OnFire();
            myBomb = null;


            ////Instantiate(orgBomb) : orgBomb을 복제해서 만들어줌
            //GameObject obj = Instantiate(orgBomb);

            ////새로 만들어 졌으니 부모를 새로 지정
            //obj.transform.SetParent(myMuzzle);
            //obj.transform.localPosition = Vector3.zero;
            //obj.transform.localRotation = myMuzzle.localRotation;

            //GameObject obj = Instantiate(orgBomb, myMuzzle);
            GameObject obj = ObjectPool.Instance.GetObject<Bomb>(orgBomb, myMuzzle.position, myMuzzle.rotation, myMuzzle);

            // .GetComponent<T>() : T를 참조
            myBomb = obj.GetComponent<Bomb>();
            
        }

    }
}
