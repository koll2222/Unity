using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CProperty : MonoBehaviour
{
    public Animator anim;
    public float Speed = 1.0f;
    public float rotSpeed = 1.0f;

    protected Animator myAnim
    {
        get
        {
            if(anim == null)
            {
                anim = GetComponent<Animator>();
                if(anim == null)
                {
                    anim = GetComponentInChildren<Animator>();
                }
            }
            return anim;
        }
    }
}
