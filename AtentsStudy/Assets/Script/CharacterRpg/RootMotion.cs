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

    // Unity의 가상 함수를 재정의
    // 
    private void OnAnimatorMove()
    {
        // myAnim.deltaPosition; 이전 위치에서 애니메이션이 동작하고 난 후의 position 차이값
        // myAnim.deltaRotation; 이전 위치에서 애니메이션이 동작하고 난 후의 rotation 차이값
        Root.position += myAnim.deltaPosition;
        Root.rotation *= myAnim.deltaRotation;  // 쿼터니언 값이라 곱하기 연산
    }
}
