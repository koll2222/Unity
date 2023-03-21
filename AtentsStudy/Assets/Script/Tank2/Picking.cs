using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour
{
    /* 마우스 클릭으로 이동
     */
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(ray, out RaycastHit hit))
            {
                StartCoroutine(MovingToPoint(hit.point));
            }
        }
    }

    /* 시작 지점의 좌표, 도착 지점의 좌표, 좌표간의 거리, 도착 판정, while문 끝내는 방법.
     * Lerp를 사용하면 조금 편해짐
     */
    IEnumerator MovingToPoint(Vector3 pos)
    {
        
        while ()
        {
            //transform.Rotate(pos * 10f * Time.deltaTime);
            Vector3 pos_xz = new Vector3(pos.x, 0, pos.z);
            transform.Translate(pos_xz * Time.deltaTime);
            yield return null;
        }
    }
}
