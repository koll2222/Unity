using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    bool isFire = false;
    public float Speed_Bomb = 10.0f;
    public GameObject target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isFire)
        {
            transform.Translate(Vector3.forward * Speed_Bomb * Time.deltaTime);
        }
    }

    public void OnFire()
    {
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
        if (other.gameObject.tag == "Bomb") return;
        //other의 로컬 좌표값을 tmp에 저장, 여기서의 other는 파괴시킬 오브젝트
        Vector3 tmp = other.transform.position;
        //other를 삭제
        Destroy(other.gameObject);
        Debug.Log("OnTriggerEnter");
        //GameObject obj = Instantiate(target);
        //target.transform.position = new Vector3(Random.Range(1f, 10f), 0, 0);
        
        //다운 캐스팅, Object -> GameObject, 다운 캐스팅에 실패하면 org 는 null 임 / 업 캐스팅은 무조건 허용됨
        GameObject org = Resources.Load("Target") as GameObject;
        if(org != null)
        {
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
