using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootMotion : MonoBehaviour
{
    // 
    public Animator myAnim = null;
    public Transform Root;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Unity�� ���� �Լ��� ������
    // 
    private void OnAnimatorMove()
    {
        // myAnim.deltaPosition; ���� ��ġ���� �ִϸ��̼��� �����ϰ� �� ���� position ���̰�
        // myAnim.deltaRotation; ���� ��ġ���� �ִϸ��̼��� �����ϰ� �� ���� rotation ���̰�
        Root.position += myAnim.deltaPosition;
        Root.rotation *= myAnim.deltaRotation;  // ���ʹϾ� ���̶� ���ϱ� ����
    }
}
