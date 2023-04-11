using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgePlayer : CharacterProperty2D
{
    bool isActive = true;
    public void Set_Active(bool live)
    {
        isActive = live;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            float x = Input.GetAxisRaw("Horizontal");
            myAnim.SetFloat("Direction", x);
            transform.Translate(transform.right * x * MoveSpeed * Time.deltaTime, Space.World);
        }
    }
}
