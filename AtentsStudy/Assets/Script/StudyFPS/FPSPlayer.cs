using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayer : RPGProperty
{
    public Rifle myRifle;
    public Transform mySpine;
    public Transform mySpringArm;
    Vector2 InputData = Vector2.zero;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        InputData.y = Input.GetAxis("Vertical");
        InputData.x = Input.GetAxis("Horizontal");

        myAnim.SetFloat("x", InputData.x);
        myAnim.SetFloat("y", InputData.y);

        if (Input.GetMouseButton(0))
        {
            myAnim.SetBool("isFiring", true);
        }
        else
        {
            myAnim.SetBool("isFiring", false);
        }
    }
    private void LateUpdate()
    {
        Vector3 angle = mySpringArm.rotation.eulerAngles;
        angle.x = mySpringArm.rotation.eulerAngles.x;
        mySpine.rotation = Quaternion.Euler(angle);
    }

    public void OnFire()
    {
        SoundManager.Instance.PlayEffectSound(myRifle.myAudio, myRifle.myClip);
    }
}
