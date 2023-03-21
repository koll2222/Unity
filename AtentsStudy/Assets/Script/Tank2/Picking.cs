using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Picking : MonoBehaviour
{
    /* ���콺 Ŭ������ �̵�
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

    /* ���� ������ ��ǥ, ���� ������ ��ǥ, ��ǥ���� �Ÿ�, ���� ����, while�� ������ ���.
     * Lerp�� ����ϸ� ���� ������
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
