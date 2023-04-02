using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* 인터페이스
 * 인터페이스 내의 함수를 상속받은 클래스에서 재정의시 무조건 public 선언을 해야 함
 * AIPercetion에서 인터페이스를 사용한 이유는 커플링을 피하기 위함임
 */
public interface IPerception
{
    void Find(Transform target);
    void LostTarget();
}

public class AIPerception : MonoBehaviour
{
    public LayerMask enemyMask;
    public List<Transform> myEnemyList = new List<Transform>();
    IPerception myParent = null;
    Transform myTarget = null;

    // Start is called before the first frame update
    void Start()
    {
        // 자신의 부모 트랜스폼에서 IPerception 컴포넌트를 찾아옴
        myParent = transform.parent.GetComponent<IPerception>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // 탐색 범위에 들어왔을 때 리스트에 추가
    private void OnTriggerEnter(Collider other)
    {
        // 범위 내에 들어온게 enemyMask 인지 확인
        if((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            // 리스트에 있지 않은 transform 이라면
            if (!myEnemyList.Contains(other.transform))
            {
                // 적 리스트에 범위내에 들어온 transform 추가
                myEnemyList.Add(other.transform);
            }
            // 처음 발견한 적을 넣어줌. null 값이 아니라면 이미 범위 내에 적이 있음
            if(myTarget == null)
            {
                // myTarget 지정
                myTarget = other.transform;
                myParent.Find(myTarget);
            }
        }
    }
    // 탐색 범위에서 나갔을 때 리스트에서 삭제
    private void OnTriggerExit(Collider other)
    {
        // 나간 대상의 layer가 enemyMask 라면
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            // 나간 대상이 적 리스트에 있다면
            if (myEnemyList.Contains(other.transform))
            {
                // 적 리스트에서 나간 대상 삭제
                myEnemyList.Remove(other.transform);
            }
            // 추적 대상이 나간 대상이라면
            if(myTarget == other.transform)
            {
                // 추적 대상 삭제
                myTarget = null;
                // 부모의 추적 대상 삭제 함수 호출
                myParent.LostTarget();
            }
        }
    }
}
