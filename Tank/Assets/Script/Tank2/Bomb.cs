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
        if (other.gameObject.tag == "Bomb") return;
        Destroy(other.gameObject);
        Debug.Log("OnTriggerEnter");
        GameObject obj = Instantiate(target);
        target.transform.position = new Vector3(Random.Range(1f, 10f), 0, 0);
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
