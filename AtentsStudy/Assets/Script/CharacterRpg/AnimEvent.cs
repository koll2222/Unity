using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimEvent : MonoBehaviour
{

    public UnityEvent AttackFunc;
    public UnityEvent DeadFunc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnAttack()
    {
        AttackFunc?.Invoke();
    }
    public void OnDead()
    {
        DeadFunc?.Invoke();
    }
}
