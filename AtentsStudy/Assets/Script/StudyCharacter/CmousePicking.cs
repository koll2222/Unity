using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CmousePicking : MonoBehaviour
{
    public LayerMask pickingMask;
    public UnityEvent<Vector3> clickMask = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit, 1000.0f, pickingMask))
            {
                clickMask?.Invoke(hit.point);
            }
        }
    }
}
