using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActionRPG : CharacterProperty
{
    Vector2 desireDirection;
    public Transform myWeapon=null;
    public LayerMask enemyMask;
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

        desireDirection = Vector2.Lerp(desireDirection, inputDirection, Time.deltaTime * 5.0f); //보간을 이용한 자연스러움

        myAnim.SetFloat("X", desireDirection.x);
        myAnim.SetFloat("Y", desireDirection.y);

        if (Input.GetMouseButtonDown(0))
        {
            myAnim.SetTrigger("Attack");
        }
    }

    public void OnAttack()
    {
       Collider[] list=Physics.OverlapSphere(myWeapon.position,0.5f,enemyMask);
        foreach(Collider col in list)
        {
            col.GetComponent<IBattle>()?.OnDamage(10.0f);
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
        }
    }

    IEnumerator ComboChecking()
    {
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
