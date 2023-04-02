using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/* �������̽�
 * �������̽� ���� �Լ��� ��ӹ��� Ŭ�������� �����ǽ� ������ public ������ �ؾ� ��
 * AIPercetion���� �������̽��� ����� ������ Ŀ�ø��� ���ϱ� ������
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
        // �ڽ��� �θ� Ʈ���������� IPerception ������Ʈ�� ã�ƿ�
        myParent = transform.parent.GetComponent<IPerception>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    // Ž�� ������ ������ �� ����Ʈ�� �߰�
    private void OnTriggerEnter(Collider other)
    {
        // ���� ���� ���°� enemyMask ���� Ȯ��
        if((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            // ����Ʈ�� ���� ���� transform �̶��
            if (!myEnemyList.Contains(other.transform))
            {
                // �� ����Ʈ�� �������� ���� transform �߰�
                myEnemyList.Add(other.transform);
            }
            // ó�� �߰��� ���� �־���. null ���� �ƴ϶�� �̹� ���� ���� ���� ����
            if(myTarget == null)
            {
                // myTarget ����
                myTarget = other.transform;
                myParent.Find(myTarget);
            }
        }
    }
    // Ž�� �������� ������ �� ����Ʈ���� ����
    private void OnTriggerExit(Collider other)
    {
        // ���� ����� layer�� enemyMask ���
        if ((enemyMask & 1 << other.gameObject.layer) != 0)
        {
            // ���� ����� �� ����Ʈ�� �ִٸ�
            if (myEnemyList.Contains(other.transform))
            {
                // �� ����Ʈ���� ���� ��� ����
                myEnemyList.Remove(other.transform);
            }
            // ���� ����� ���� ����̶��
            if(myTarget == other.transform)
            {
                // ���� ��� ����
                myTarget = null;
                // �θ��� ���� ��� ���� �Լ� ȣ��
                myParent.LostTarget();
            }
        }
    }
}
