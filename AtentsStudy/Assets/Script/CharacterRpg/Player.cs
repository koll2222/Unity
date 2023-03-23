using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : RPGMovement
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMove(Vector3 pos)
    {
        MoveToPos(pos);
    }
}
