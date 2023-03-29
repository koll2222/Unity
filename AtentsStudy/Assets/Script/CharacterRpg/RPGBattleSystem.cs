using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IBattle
{
    void OnDamage(float dmg);
    // property, 얘는 변수가 아니라 함수임
    bool IsLive { get; }
}

public class RPGBattleSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
