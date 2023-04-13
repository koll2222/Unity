using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// navigation ����� ����
using UnityEngine.AI;

public class AIPlayer : AIMovement, IBattle
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            OnDamage(10.0f);
        }
    }

    public void OnMove(Vector3 pos)
    {
        NavMeshPath path = new NavMeshPath();
        if(NavMesh.CalculatePath(transform.position, pos, NavMesh.AllAreas, path))
        {
            switch (path.status)
            {
                // �κ��� �̵�
                case NavMeshPathStatus.PathPartial:
                    break;
                // �������� �Ұ����� ���
                case NavMeshPathStatus.PathInvalid:
                    break;
                // �̵� �Ϸ�
                case NavMeshPathStatus.PathComplete:
                    break;
            }
            MoveByPath(path.corners);
        }
    }
    public void OnDamage(float dmg)
    {
        curHp -= dmg;
    }
    public bool IsLive 
    {
        get;
    }
}
