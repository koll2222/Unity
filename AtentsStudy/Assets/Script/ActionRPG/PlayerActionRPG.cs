using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionRPG : RPGProperty
{
    public LayerMask enemyMask;
    public Transform myWeapon;
    Vector2 desireDirection;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector2 inputDirection = Vector2.zero;
        inputDirection.x = Input.GetAxisRaw("Horizontal");
        inputDirection.y = Input.GetAxisRaw("Vertical");

        desireDirection = Vector2.Lerp(desireDirection, inputDirection, Time.deltaTime * 5.0f);

        myAnim.SetFloat("x", desireDirection.x);
        myAnim.SetFloat("y", desireDirection.y);

        if (Input.GetMouseButtonDown(0))
        {
            myAnim.SetTrigger("Attack");
        }
    }
    public void OnAttack()
    {
        Collider[] colList = Physics.OverlapSphere(myWeapon.position, 0.5f, enemyMask);
        foreach(Collider col in colList)
        {
            col.GetComponent<IBattle>().OnDamage(AttackPoint);
        }
    }
    int clickCount = 0;
    Coroutine coCheck = null;
    public void ComboCheckStart()
    {
        coCheck = StartCoroutine(ComboChecking());
    }
    public void ComboCheckEnd()
    {
        StopCoroutine(coCheck);
        if (clickCount == 0)
        {
            myAnim.SetTrigger("FailedCombo");
            myAnim.SetBool("isFailedCombo", true);
        }
    }
    IEnumerator ComboChecking()
    {
        myAnim.SetBool("isFailedCombo", false);
        clickCount = 0;
        while (true)
        {
            if (Input.GetMouseButtonDown(0))
            {
                clickCount++;
            }
            yield return null;
        }
    }
}
