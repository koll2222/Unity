using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PickingMouse : MonoBehaviour
{
    public LayerMask pickMask;
    public LayerMask enemyMask;
    public UnityEvent<Vector3> clickAction = null;
    public UnityEvent<Transform> attackAction = null;

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
            // 찍은 대상이 pickMask or enemyMask
            if( Physics.Raycast(ray, out RaycastHit hit, 1000f, pickMask | enemyMask) )
            {
                // 찍은 대상이 enemyMask인지
                if ((1 << hit.transform.gameObject.layer & enemyMask) != 0)
                {
                    // 공격
                    attackAction?.Invoke(hit.transform);
                }
                else
                {
                    // 이동
                    clickAction?.Invoke(hit.point);
                }
            }
        }
    }
}
