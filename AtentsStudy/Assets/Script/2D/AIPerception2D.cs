using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AIPerception2D : MonoBehaviour
{
    public LayerMask enemyMask;
    public UnityEvent<Transform> findEnemy;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Searching());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // 물리 처리는 여기서
    private void FixedUpdate()
    {
        
    }

    IEnumerator Searching()
    {
        Collider2D col = null;
        while(!col)
        {
            // 오버랩 활용 지향
            col = Physics2D.OverlapCircle(transform.position, 5.0f, enemyMask);
            if (col != null)
            {
                findEnemy?.Invoke(col.transform);
            }
            yield return new WaitForFixedUpdate();
        }
    }
}
