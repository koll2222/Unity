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

            //Raycast�� ���� �������� üũ�ϱ⿡ ���ð��� �ƴ� ���� ���� �̿��ؾ� ��.
            Ray ray = new Ray();
            ray.origin = transform.position;
            ray.direction = transform.forward;
            // out : ����� ������������ ��ȯ�� ���ÿ� �̷����
            if(Physics.Raycast(ray, out RaycastHit hit, delta))
            {   //�ε����� ���� �� ������ ��
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
        //transform.parent = null; ���� ������ �Լ� ����� ������.
        GetComponent<Collider>().isTrigger = false;
    }

    //�ε����� ����
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("OnCollisionEnter");
    }
    //�ε����� �ִ� ����
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log("OnCollisionStay");
    }
    //�ε����ٰ� �������� ����
    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("OnCollisionExit");
    }

    private void OnTriggerEnter(Collider other)
    {
        //Tag ����� �����ϰ� Layer ����� ����.
        if (other.gameObject.tag == "Bomb" || other.gameObject.tag == "Ground") return;
        { 
        ////other�� ���� ��ǥ���� tmp�� ����, ���⼭�� other�� �ı���ų ������Ʈ
        //Vector3 tmp = other.transform.position;
        ////other�� ����
        //Destroy(other.gameObject);
        //Debug.Log("OnTriggerEnter");

        //////�ٿ� ĳ����, Object -> GameObject, �ٿ� ĳ���ÿ� �����ϸ� org �� null �� / �� ĳ������ ������ ����
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
            yield return new WaitForSeconds(1.0f);  //���� ����, 1�� ���
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
