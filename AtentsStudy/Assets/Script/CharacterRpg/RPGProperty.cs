using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RPGProperty : MonoBehaviour
{

    Animator anim = null;
    public float MoveSpeed = 5.0f;
    public float rotSpeed = 360.0f;

    protected Animator myAnim
    {
        get
        {
            if(anim == null)        // anim에 참조값이 없으면
            {
                anim = GetComponent<Animator>();                // Animator 컴포넌트를 가져옴
                if(anim == null)                                // 그래도 없으면
                {
                    anim = GetComponentInChildren<Animator>();  // 자식들에서 가져옴
                }
            }
            return anim;
        }
    }
}
