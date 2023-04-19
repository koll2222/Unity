using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    bool isFire = false;
    public float Speed_Bomb = 10.0f;
    //public GameObject target = null;
    public GameObject onFireEffect = null;
    //int target_cnt = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            float delta = Speed_Bomb * Time.deltaTime;

            //Raycast는 월드 기준으로 체크하기에 로컬값이 아닌 월드 값을 이용해야 함.
            Ray ray = new Ray();
            ray.origin = transform.position;
            ray.direction = transform.forward;
            // out : 선언과 참조형으로의 변환이 동시에 이루어짐
            if(Physics.Raycast(ray, out RaycastHit hit, delta))
            {   //부딪힘이 있을 때 실행이 됨
                DestroyObject(hit.transform.gameObject);
               
            }

            transform.Translate(Vector3.forward * delta);
        }
    }

    public void OnFire()
    {
        Instantiate(onFireEffect, transform.position, Quaternion.identity);
        isFire = true;
        transform.SetParent(null);
        //transform.parent = null; 둘이 같지만 함수 사용을 권장함.
        GetComponent<Collider>().isTrigger = false;
    }

    //부딪히는 순간
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }
    //부딪히고 있는 동안
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay");
    }
    //부딪혔다가 떨어지는 순간
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Tag 방식은 지양하고 Layer 방식을 지향.
        if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Ground") return;
        { 
        ////other의 로컬 좌표값을 tmp에 저장, 여기서의 other는 파괴시킬 오브젝트
        //Vector3 tmp = other.transform.position;
        ////other를 삭제
        //Destroy(other.gameObject);
        //Debug.Log("OnTriggerEnter");

        //////다운 캐스팅, Object -> GameObject, 다운 캐스팅에 실패하면 org 는 null 임 / 업 캐스팅은 무조건 허용됨
        ////GameObject org = Resources.Load("Target") as GameObject;
        ////if(org != null)
        ////{
        ////    GameObject obj = Instantiate(org);
        ////    tmp.x = Random.Range(-10f, 10f);
        ////    obj.transform.position = tmp;
        ////}
        }
        //StartCoroutine(CreateDelay(other.transform.position));

    }
    void DestroyObject(GameObject obj)
    {
        if (obj.gameObject.tag == "Bomb" || obj.gameObject.tag == "Ground") return;
        Vector3 tmp = obj.transform.position;
        Destroy(obj.gameObject);
        StartCoroutine(CreateDelay(tmp));
    }
    IEnumerator CreateDelay(Vector3 tmp)
    {

        GameObject org = Resources.Load("Target") as GameObject;
        if (org != null)
        {
            yield return new WaitForSeconds(1.0f);  //지연 리턴, 1초 대기
            GameObject obj = Instantiate(org);
            tmp.x = Random.Range(-10f, 10f);
            obj.transform.position = tmp;
        }

    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Bomb") return;
        Debug.Log("OnTriggerStay");
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Bomb") return;
        Debug.Log("OnTriggerExit");
    }
}
