using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Transform location_target = null;
    public Target target = null;
    public GameObject org_Target = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        //Tag 방식은 지양하고 Layer 방식을 지향.
        if (other.gameObject.tag != "Bomb") return;
        GameObject obj = Instantiate(org_Target, location_target);
        target = obj.GetComponent<Target>();
    }
}
