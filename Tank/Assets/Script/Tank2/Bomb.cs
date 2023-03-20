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
